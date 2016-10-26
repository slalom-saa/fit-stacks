using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Messaging.Validation
{
    internal class PropertyRuleCollection<TValue, TProperty> : IValidationRule<TValue, ExecutionContext> where TValue : ICommand
    {
        private readonly Expression<Func<TValue, TProperty>> _property;
        internal PropertyRule<TProperty> StarterRule = new PropertyRule<TProperty>("This is a starter rule that can be ignored.", (a, b) => true);

        public PropertyRuleCollection(Expression<Func<TValue, TProperty>> property)
        {
            _property = property;
        }

        public IEnumerable<ValidationError> Validate(TValue instance, ExecutionContext context)
        {
            var value = _property.Compile()(instance);

            return StarterRule.Validate(value, context);
        }
    }
}