using System.Threading.Tasks;
using Slalom.Stacks.Messaging;
using WebApplication1.Domain;

namespace WebApplication1.Actors.Products.Expire
{
    [Path("products/expire")]
    public class ExpireProduct : UseCaseActor<ExpireProductCommand, ExpireProductEvent>
    {
        public override async Task<ExpireProductEvent> ExecuteAsync(ExpireProductCommand command)
        {
            var target = await this.Domain.FindAsync<Product>(command.Id);

            target.Expire();

            await this.Domain.UpdateAsync(target);

            return new ExpireProductEvent(target.Id);
        }
    }
}