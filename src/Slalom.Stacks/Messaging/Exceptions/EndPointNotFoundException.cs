using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging.Exceptions
{
    public class EndPointNotFoundException : InvalidOperationException
    {
        public EndPointNotFoundException(Request request) : base("An endpoint could not be found for the path " + request.Path)
        {
        }
    }
}
