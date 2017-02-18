using System.Collections.Generic;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.TestStack.Examples.Domain
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

    public class ItemAddedEvent : Event
    {
        public string Name { get; }

        public ItemAddedEvent(string name)
        {
            this.Name = name;
        }
    }

    public class Item : AggregateRoot
    {
        protected Item()
        {
        }

        public static Item Create(string text)
        {
            var target = new Item
            {
                Name = text
            };
            target.AddEvent(new ItemAddedEvent(text));
            return target;
        }

        public ItemName Name { get; private set; }
    }
}