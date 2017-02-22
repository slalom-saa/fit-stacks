using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Slalom.Stacks.Messaging.Pipeline;
using Slalom.Stacks.Messaging.Routing;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.Reflection;
using Module = Autofac.Module;

namespace Slalom.Stacks.Messaging.Modules
{
    /// <summary>
    /// An Autofac module to configure the communication dependencies.
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    internal class MessagingModule : Module
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessagingModule"/> class.
        /// </summary>
        /// <param name="assemblies">The assemblies used to probe.</param>
        public MessagingModule(Assembly[] assemblies)
        {
            this._assemblies = assemblies;
        }

        private Assembly[] _assemblies;

        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        /// <remarks>Note that the ContainerBuilder parameter is unique to this module.</remarks>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(c => new MessageStream(c.Resolve<IComponentContext>()))
                   .As<IMessageStream>()
                   .SingleInstance();

            builder.RegisterAssemblyTypes(_assemblies.Union(new[] { typeof(IMessageExecutionStep).GetTypeInfo().Assembly }).ToArray())
                .Where(e => e.GetInterfaces().Any(x => x == typeof(IMessageExecutionStep)))
                .AsSelf();

            builder.RegisterType<MessageExecutionPipeline>()
                .AsImplementedInterfaces();

            builder.RegisterType<RequestRouting>()
                .AsImplementedInterfaces();

            

            builder.RegisterGeneric(typeof(CommandValidator<>));

            builder.RegisterModule(new MessagingTypesModule(this._assemblies));
        }
    }
}