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
    [EndPoint("catalog/products/add", Timeout = 5000)]
    public class AddProduct : UseCase<AddProductCommand, string>
    {
        private int _count;

        public override async Task<string> ExecuteAsync(AddProductCommand command)
        {
            _count++;

            if (_count > 2)
            {
                throw new Exception("Asdfasdf");
            }

            var target = new Product(command.Name);

            await this.Domain.Add(target);

            var stock = await this.Send(new StockProductCommand());
            if (!stock.IsSuccessful)
            {
                // rollback 

                throw new ChainFailedException(this.Request, stock);
            }

            this.AddRaisedEvent(new AddProductEvent(target.Id, target.Name));

            return "Added " + _count;
        }
    }
}
