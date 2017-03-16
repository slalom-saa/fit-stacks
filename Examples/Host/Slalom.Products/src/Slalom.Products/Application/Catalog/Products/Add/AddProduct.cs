using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Products.Application.Catalog.Integration;
using Slalom.Products.Domain.Catalog.Products;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Exceptions;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;
using Slalom.Stacks.Validation;

namespace Slalom.Products.Application.Catalog.Products.Add
{
    /// <summary>
    /// Adds a product to the product catalog so that a user can search for it and it can be added to a cart, purchased and/or shipped.
    /// </summary>
    [EndPoint("catalog/products/add", Timeout = 1000)]
    public class AddProduct : UseCase<AddProductCommand, string>
    {
        /// <inheritdoc />
        public override async Task<string> ExecuteAsync(AddProductCommand command)
        {
            var target = new Product(command.Name);

            await this.Domain.Add(target);

            var stock = await this.Send(new StockProductCommand(target.Id, 5));
            if (!stock.IsSuccessful)
            {
                // rollback 

                throw new ChainFailedException(this.Request, stock);
            }

            this.AddRaisedEvent(new AddProductEvent(target.Id, target.Name));

            return target.Id;
        }
    }
}
