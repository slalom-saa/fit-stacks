using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Slalom.Stacks.Communication;
using Slalom.Stacks.Runtime;

namespace Slalom.Stacks.EntityFramework
{
    public class EntityFrameworkAuditStore : IAuditStore
    {
        private readonly DbContext _context;

        public EntityFrameworkAuditStore(DbContext context)
        {
            _context = context;
        }

        public Task AppendAsync(ICommand command, ICommandResult result, ExecutionContext context)
        {
            _context.Add(new Audit(command, result, context));
            return _context.SaveChangesAsync();
        }
    }
}
