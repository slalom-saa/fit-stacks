using System.Reflection;
using Microsoft.CodeAnalysis;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Text;

namespace Slalom.Stacks.Documentation.Model
{
    public class TestElement
    {
        public TestElement(MethodInfo test)
        {
            this.Name = (test.DeclaringType.Name + "_it_" + test.Name).ToSentence();
        }

        public TestElement(IMethodSymbol method)
        {
            this.Name = (method.ContainingType.Name + "_it_" + method.Name).ToSentence();
        }

        public string Name { get; set; }
    }
}