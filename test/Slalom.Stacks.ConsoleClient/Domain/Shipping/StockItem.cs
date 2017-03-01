using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.ConsoleClient.Domain.Shipping
{
    public class StockItem : AggregateRoot
    {
        public string ProductId { get; }

        public int Quantity { get; private set; }

        public StockItem(string productId)
        {
            this.ProductId = productId;
        }

        public void Add(int quantity)
        {
            if (quantity < 1)
            {
                throw new ValidationException("The product quantity must be greater than 0.");
            }

            this.Quantity += quantity;
        }

        public void Remove(int quantity)
        {
            if (quantity < 1)
            {
                throw new ValidationException("Quantity cannot be less than 1.");
            }
            if (this.Quantity - quantity < 0)
            {
                throw new ValidationException("There is not enough quantity to remove x.");
            }

            this.Quantity -= quantity;
        }
    }
}
