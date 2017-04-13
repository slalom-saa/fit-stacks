using System.Xml.Linq;
using Microsoft.CodeAnalysis;
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

        public ParameterElement(IPropertySymbol property)
        {
            this.Name = property.Name;
            this.TypeName = property.Type.Name;
            this.Comments = new Comments(property.GetDocumentationCommentXml());
        }

        public Comments Comments { get; set; }

        public string TypeName { get; set; }

        public string Name { get; set; }
    }
}