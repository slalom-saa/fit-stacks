using System;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Services.Registry;

namespace Slalom.Stacks.Services
{
    public abstract class ServiceEndPoint : Service, IEndPoint<object>
    {
        public void Respond(object response)
        {
            ((IService)this).Context.Response = response;
        }

        public virtual void Execute()
        {
            throw new NotImplementedException();
        }

        public virtual Task ExecuteAsync()
        {
            this.Execute();
            return Task.FromResult(0);
        }

        public Task Receive(object instance)
        {
            return this.ExecuteAsync();
        }
    }

    public abstract class ServiceEndPoint<T, R> : Service, IEndPoint<T>
    {
        public virtual R Execute(T instance)
        {
            throw new NotImplementedException();
        }

        public virtual Task<R> ExecuteAsync(T instance)
        {
            return Task.FromResult(this.Execute(instance));
        }

        public async Task Receive(T instance)
        {
            var result = await this.ExecuteAsync(instance);

            ((IService)this).Context.Response = result;
        }
    }

    public abstract class SystemEndPoint<T> : Service, IEndPoint<T>
    {
        public virtual void Execute(T instance)
        {
            throw new NotImplementedException();
        }

        public virtual Task ExecuteAsync(T instance)
        {
            this.Execute(instance);
            return Task.FromResult(0);
        }

        public Task Receive(T instance)
        {
            return this.ExecuteAsync(instance);
        }
    }
}