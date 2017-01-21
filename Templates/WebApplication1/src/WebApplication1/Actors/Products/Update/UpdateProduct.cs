using System.Threading.Tasks;
using Slalom.Stacks.Messaging;
using WebApplication1.Domain;

namespace WebApplication1.Actors.Products.Update
{
    [Path("products/update")]
    public class UpdateProduct : UseCaseActor<UpdateProductCommand, UpdateProductEvent>
    {
        public override async Task<UpdateProductEvent> ExecuteAsync(UpdateProductCommand command)
        {
            var target = await this.Domain.FindAsync<Product>(command.Id);

            target.Update(command.Name, command.Description);

            await this.Domain.UpdateAsync(target);

            return new UpdateProductEvent(target);
        }
    }
}