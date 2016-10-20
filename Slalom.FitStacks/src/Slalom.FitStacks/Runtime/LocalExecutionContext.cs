using System;
using System.Linq;
using System.Security.Claims;

namespace Slalom.FitStacks.Runtime
{
    public class LocalExecutionContext : ExecutionContext
    {
        public LocalExecutionContext()
        {
        }

        public LocalExecutionContext(string userName, params Claim[] claims)
            : base(userName, claims)
        {
        }
    }
}