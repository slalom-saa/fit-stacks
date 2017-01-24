using System.Threading.Tasks;
using ConsoleApplication7.Domain;
using Slalom.Stacks.Messaging;

namespace ConsoleApplication7.Application.Actors.Products.Activate
{
    public class ActivateProduct : UseCaseActor<ActivateProductCommand, ActivateProductEvent>
    {
        public override async Task<ActivateProductEvent> ExecuteAsync(ActivateProductCommand command)
        {
            var target = await this.Domain.FindAsync<Product>(command.ProductId);

            target.Activate();

            return new ActivateProductEvent(target.Id);
        }
    }
}