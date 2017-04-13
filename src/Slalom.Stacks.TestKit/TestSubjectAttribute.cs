using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slalom.Stacks.TestKit
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TestSubjectAttribute : Attribute
    {
        public Type Type { get; }

        public TestSubjectAttribute(Type type)
        {
            this.Type = type;
        }
    }
}
