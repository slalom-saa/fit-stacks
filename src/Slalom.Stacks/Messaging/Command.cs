using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Messaging
{
    public class CommandAttribute : Attribute
    {
        public string Path { get; }

        public CommandAttribute(string path)
        {
            this.Path = path;
        }
    }

    public abstract class Command
    {
    }
}
