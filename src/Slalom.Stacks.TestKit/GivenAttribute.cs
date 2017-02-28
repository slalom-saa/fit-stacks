using System;

namespace Slalom.Stacks.TestKit
{
    public class GivenAttribute : Attribute
    {
        public Type Name { get; }

        public GivenAttribute(Type name)
        {
            this.Name = name;
        }
    }
}