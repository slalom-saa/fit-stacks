using System;
using Akka.Actor;
using Akka.DI.Core;
using Autofac;

namespace Slalom.Stacks.Messaging
{
    public class UseCaseSupervisionActor : ReceiveActor
    {
        public ActorNetwork ActorNetwork { get; set; }
        public IComponentContext ComponentContext { get; set; }

        public string Path
        {
            get
            {
                var path = Context.Self.Path.ToString();
                return path.Substring(path.IndexOf("user/", StringComparison.OrdinalIgnoreCase) + 5);
            }
        }

        protected override void PreStart()
        {
            base.PreStart();

            var node = ActorNetwork.RootNode.Find(this.Path);
            foreach (var child in node.Nodes)
            {
                var name = child.Path.Substring(child.Path.LastIndexOf('/') + 1);
                if (Context.Child(name).Equals(ActorRefs.Nobody))
                {
                    if (child.Type == null)
                    {
                        Context.ActorOf(Context.DI().Props<UseCaseSupervisionActor>(), name);
                    }
                    else
                    {
                        Context.ActorOf(Props.Create(() => new AkkaUseCaseActor((IHandle)this.ComponentContext.Resolve(child.Type), this.ComponentContext)), name);
                    }
                }
            }
        }
    }


    //[Path("items")]
    //public class ItemsActor : UseCaseSupervisionActor
    //{
    //    public ItemsActor()
    //    {
    //    }

    //    protected override void PreStart()
    //    {
    //       // Context.ActorOf(Context.DI().Props>(), "add-item");

    //        base.PreStart();
    //    }

    //    protected override bool AroundReceive(Receive receive, object message)
    //    {
    //        Console.WriteLine("...");
    //        return base.AroundReceive(receive, message);
    //    }

    //}
}