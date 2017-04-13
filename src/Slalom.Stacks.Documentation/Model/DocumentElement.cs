using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Slalom.Stacks.Messaging.Registry;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.TestKit;

namespace Slalom.Stacks.Documentation.Model
{
    public class DocumentElement
    {
        public List<EndPointElement> EndPoints { get; set; } = new List<EndPointElement>();
    }
}
