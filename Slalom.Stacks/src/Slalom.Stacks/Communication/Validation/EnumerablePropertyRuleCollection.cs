using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Slalom.Stacks.Runtime;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Communication.Validation
{
    internal class EnumerablePropertyRuleCollection<TValue, TProperty> : IValidationRule<TValue, ExecutionContext> where TValue : ICommand
    {
        private readonly Action<PropertyRule<TProperty>> _action;
        private readonly Expression<Func<TValue, IEnumerable<TProperty>>> _property;

        internal PropertyRule<TProperty> StarterRule = new PropertyRule<TProperty>("This is a starter rule that can be ignored.", (a, b) => true);

        public EnumerablePropertyRuleCollection(Expression<Func<TValue, IEnumerable<TProperty>>> property, Action<PropertyRule<TProperty>> action)
        {
            _property = property;
            _action = action;
        }

        public async Task<IEnumerable<ValidationError>> Validate(TValue instance, ExecutionContext context)
        {
            var value = _property.Compile()(instance);

            _action.Invoke(StarterRule);

            foreach (var item in value)
            {
                var target = await StarterRule.Validate(item, context);
                if (target.Any())
                {
                    return target;
                }
            }
            return Enumerable.Empty<ValidationError>();
        }
    }
}