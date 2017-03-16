using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Products.Application.Catalog.Interfaces;
using Slalom.Products.Domain.Catalog.Products;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Exceptions;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;
using Slalom.Stacks.Validation;

namespace Slalom.Products.Application.Catalog.Products.Add
{
    /// <summary>
    /// Yada yada yada.
    /// </summary>
    [EndPoint("catalog/products/add", Timeout = 1000)]
    public class AddProduct : UseCase<AddProductCommand, string>
    {
        private int? _count;

        private async Task<int> GetCount()
        {
            if (_count == null)
            {

                await Task.Delay(500);
                _count = 0;

            }
            _count = _count + 1;

            return _count.Value;
        }

        public override async Task<string> ExecuteAsync(AddProductCommand command)
        {
            var count = await GetCount();

            if (count > 3)
            {
                throw new Exception("Asdfasdf");
            }

            var target = new Product(command.Name);

            await this.Domain.Add(target);

            var stock = await this.Send("shipping/products/stock", new StockProductCommand());
            if (!stock.IsSuccessful)
            {
                // rollback 

                throw new ChainFailedException(this.Request, stock);
            }

            this.AddRaisedEvent(new AddProductEvent(target.Id, target.Name));

            return "Added " + count;
        }
    }
}
