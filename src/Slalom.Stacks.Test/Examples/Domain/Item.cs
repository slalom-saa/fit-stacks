using System.Collections.Generic;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Test.Examples.Domain
{
    public class ItemName : ConceptAs<string>
    {
        public static implicit operator ItemName(string value)
        {
            var target = new ItemName {Value = value};
            target.EnsureValid();
            return target;
        }

        public override IEnumerable<ValidationError> Validate()
        {
            if (string.IsNullOrWhiteSpace(this.Value) || this.Value.Length < 3)
            {
                yield return "An item must have a name that is more than 3 characters.";
            }
        }
    }

    public class Item : AggregateRoot
    {
        protected Item()
        {
        }

        public static Item Create(string text)
        {
            return new Item
            {
                Name = text
            };
        }

        public ItemName Name { get; private set; }
    }
}