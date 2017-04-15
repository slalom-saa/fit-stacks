using System;

namespace Slalom.Stacks.Services.Messaging
{
    public class EndPointNotFoundException : InvalidOperationException
    {
        public EndPointNotFoundException(Request request) : base("An endpoint could not be found for the path " + request.Path)
        {
        }
    }
}
