using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Communication;

namespace Slalom.Stacks.Commands
{
    public class ProcedureAdded : Event
    {
        public string Name { get; }

        public ProcedureAdded(string name)
        {
            this.Name = name;
        }
    }
}
