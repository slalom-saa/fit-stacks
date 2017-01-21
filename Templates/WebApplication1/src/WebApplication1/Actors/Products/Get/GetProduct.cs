using System.Threading.Tasks;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;
using WebApplication1.Controllers;
using WebApplication1.Domain;

namespace WebApplication1.Actors.Products.Get
{
    [Path("products/get")]
    public class GetProduct : UseCaseActor<GetProductCommand, Product>
    {
        public override async Task<Product> ExecuteAsync(GetProductCommand command)
        {
            var target = await this.Domain.FindAsync<Product>(command.Id);

            if (target == null)
            {
                throw new ValidationException(new ValidationError("The specified product could not be found.", ValidationErrorType.Business));
            }

            return target;
        }
    }
}