using System;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApplication7.Domain;
using Slalom.Stacks.Messaging;

namespace ConsoleApplication7.Application.Actors.Products.Get
{
    public class GetProduct : UseCaseActor<GetProductCommand, Product>
    {
        public override async Task<Product> ExecuteAsync(GetProductCommand command)
        {
            return await this.Domain.FindAsync<Product>(command.ProductId);
        }
    }
}