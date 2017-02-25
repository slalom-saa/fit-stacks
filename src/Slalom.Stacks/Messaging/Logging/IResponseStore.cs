using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging.Logging
{
    public interface IResponseStore
    {
        Task Append(ResponseEntry entry);
    }
}
