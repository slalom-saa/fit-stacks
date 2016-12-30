using System;
using Autofac;
using Slalom.Stacks.Caching;

namespace Slalom.Stacks.Logging
{
    /// <summary>
    /// An Autofac module to configure null logging dependencies.
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    internal class NullLoggingModule : Module
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

            builder.Register(c => new NullLogger())
                   .AsImplementedInterfaces()
                   .SingleInstance();
        }
    }
}