using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Slalom.Stacks.Domain;
using Slalom.Stacks.Messaging;
using Slalom.Stacks.Messaging.Validation;
using Slalom.Stacks.TestStack;
using Slalom.Stacks.Validation;
using Xunit;

namespace Slalom.Stacks.UnitTests
{
    public class ProductName : ConceptAs<string>
    {
        public static implicit operator ProductName(string value)
        {
            var target = new ProductName { Value = value };
            target.EnsureValid();
            return target;
        }

        public override IEnumerable<ValidationError> Validate()
        {
            if (this.Value?.Length != 4)
            {
                yield return "A product name must be 3 characters.";
            }
        }
    }

    public class name_must_be_unique : BusinessRule<AddProductCommand>
    {
        public override IEnumerable<ValidationError> Validate(AddProductCommand instance)
        {
            if (instance.Name != "code")
            {
                yield return "no code";
            }
        }
    }

    public class AddProductCommand : Command
    {
        [NotNull("Code")]
        public string Name { get; }

        public AddProductCommand(string name)
        {
            this.Name = name;
        }
    }

    public class AddProduct : UseCase<AddProductCommand>
    {
        public override void Execute(AddProductCommand command)
        {
            this.Domain.Add(new Product(command.Name));
        }
    }


    public class Product : AggregateRoot
    {
        public Product(string name)
        {
            this.Name = name;
        }

        public ProductName Name { get; set; }
    }

    public class StateZero : Scenario
    {
        public StateZero()
        {
            this.WithData(new Product("aaaa"), new Product("bbbb"));
        }
    }


    public class FirstTest
    {
        [Fact(DisplayName = "Add product"), Given(typeof(StateZero))]
        public void A()
        {
            using (var context = new TestStack.TestStack(typeof(AddProduct)))
            {
                context.Send(new AddProductCommand("code"));

                context.LastResult.ShouldBeSuccessful();

                context.Domain.Find<Product>().Result.Count().Should().Be(3);
            }
        }

        [Fact(DisplayName = "Add product with null name"), Given(typeof(StateZero))]
        public void B()
        {
            using (var context = new TestStack.TestStack(typeof(AddProduct)))
            {
                context.Send(new AddProductCommand(null));

                context.LastResult.ShouldNotBeSuccessful("a null name was sent");

                context.LastResult.ShouldContainMessage(ValidationType.Input, "Code");
            }
        }

        [Fact(DisplayName = "Add product with invalid name"), Given(typeof(StateZero))]
        public void C()
        {
            using (var context = new TestStack.TestStack(typeof(AddProduct)))
            {
                context.Send(new AddProductCommand("some"));

                context.LastResult.ShouldNotBeSuccessful("a name of some was sent");

                context.LastResult.ShouldContainMessage(ValidationType.Business, "no code");
            }
        }
    }
}
