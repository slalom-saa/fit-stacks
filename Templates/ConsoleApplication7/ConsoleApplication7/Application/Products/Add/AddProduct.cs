using System.Threading.Tasks;
using ConsoleApplication7.Domain;
using Slalom.Stacks.Messaging;

namespace ConsoleApplication7.Application.Actors.Products.Add
{
    public class AddProduct : UseCaseActor<AddProductCommand, AddProductEvent>
    {
        public override async Task<AddProductEvent> ExecuteAsync(AddProductCommand command)
        {
            var product = new Product(command.Name, command.Description);

            await this.Domain.AddAsync(product);

            return new AddProductEvent(product);
        }
    }
}