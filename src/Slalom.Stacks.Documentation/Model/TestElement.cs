using System.Reflection;
using Slalom.Stacks.Text;

namespace Slalom.Stacks.Documentation.Model
{
    public class TestElement
    {
        public TestElement(MethodInfo test)
        {
            this.Name = (test.DeclaringType.Name + "_it_" + test.Name).ToSentence();
        }

        public string Name { get; set; }
    }
}