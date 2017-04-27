using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.Services.Messaging
{
    public class Document
    {
        public Document(string name, byte[] content)
        {
            this.Name = name;
            this.Content = content;
        }

        public string Name { get;  }

        public byte[] Content { get;  }
    }
}
