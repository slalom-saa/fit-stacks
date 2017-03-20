using System;
using Slalom.Products.Application.Shipping.Products.Integration;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;

namespace Slalom.Products.Application.Shipping.Products.Stock
{
    /// <summary>
    /// Requests that stock be added for a given product.
    /// </summary>
    [EndPoint("shipping/products/stock")]
    public class StockProduct : UseCase<StockProductCommand>
    {
        /// <inheritdoc />
        public override void Execute(StockProductCommand command)
        {
            Console.WriteLine("Stocking product " + command.ProductId);

            this.Send(new SendNotificationCommand("Product Stocked", command.ProductId));

            this.AddRaisedEvent(new StockProductEvent(command.ProductId, command.Quantity));
        }
    }
}