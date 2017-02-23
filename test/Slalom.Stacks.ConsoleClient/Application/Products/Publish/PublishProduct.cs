using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging;

namespace Slalom.Stacks.ConsoleClient.Application.Products.Publish
{
    [Path("products/publish")]
    public class PublishProduct : UseCase<PublishProductCommand>
    {
        public override void Execute(PublishProductCommand message)
        {
        }
    }
}
