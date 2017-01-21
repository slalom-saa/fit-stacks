using Slalom.Stacks.Messaging;

namespace WebApplication1.Actors.Products.Get
{
    public class GetProductCommand : Command
    {
        public string Id { get; }

        public GetProductCommand(string id)
        {
            this.Id = id;
        }
    }
}