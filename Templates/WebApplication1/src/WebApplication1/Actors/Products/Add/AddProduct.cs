using System.Threading.Tasks;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Runtime;
using WebApplication1.Controllers;
using WebApplication1.Domain;

namespace WebApplication1.Actors.Products.Add
{
    [Path("products/add")]
    public class AddProduct : UseCaseActor<AddProductCommand, AddProductEvent>
    {
        public override async Task<AddProductEvent> ExecuteAsync(AddProductCommand command)
        {
            var target = Product.Create(command.Name, command.Description);

            await this.Domain.AddAsync(target);

            return new AddProductEvent(target);
        }
    }
}