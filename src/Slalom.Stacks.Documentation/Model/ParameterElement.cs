using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Registry;

namespace Slalom.Stacks.Documentation.Model
{
    public class ParameterElement
    {
        public ParameterElement(EndPointProperty property)
        {
            this.Name = property.Name;
            this.TypeName = property.Type;
            this.Comments = property.Comments;
        }

        public Comments Comments { get; set; }

        public string TypeName { get; set; }

        public string Name { get; set; }
    }
}