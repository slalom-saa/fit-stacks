using System;
using Autofac;
using System.Linq;

namespace Slalom.Stacks.Runtime
{
    /// <summary>
    /// An Autofac module that wires up runtime dependencies for the stack.
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    public class RuntimeModule : Module
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        /// <remarks>Note that the ContainerBuilder parameter is unique to this module.</remarks>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            //builder.Use(c => new ExecutionContextManager(c.Resolve<IExecutionContextResolver>()))
            //       .SingleInstance()
            //       .OnActivated(e =>
            //       {
            //           ExecutionContextManager.Instance = e.Instance;
            //       }).AutoActivate();
        }
    }
}