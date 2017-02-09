using System.Threading.Tasks;
using Slalom.Stacks.Messaging;
using WebApplication1.Domain;

namespace WebApplication1.Actors.Products.Activate
{
    [Path("products/expire")]
    public class ActivateProduct : UseCaseActor<ActivateProductCommand, ActivateProductEvent>
    {
        public override async Task<ActivateProductEvent> ExecuteAsync(ActivateProductCommand command)
        {
            var target = await this.Domain.FindAsync<Product>(command.Id);

            target.Activate();

            await this.Domain.UpdateAsync(target);

            return new ActivateProductEvent(target.Id);
        }
    }
}