using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Slalom.Stacks.Search
{
    internal static class SearchExtensions
    {
        internal static Expression<Func<T, bool>> Search<T>(string searchText)
        {
            var t = Expression.Parameter(typeof(T));
            Expression body = Expression.Constant(false);

            var containsMethod = typeof(string).GetMethod("Contains"
                , new[] { typeof(string) });

            var toLowerMethod = typeof(string).GetMethod("ToLower", new Type[0]);

            var toStringMethod = typeof(object).GetMethod("ToString");

            var stringProperties = typeof(T).GetProperties()
                .Where(property => property.PropertyType == typeof(string));

            foreach (var property in stringProperties)
            {
                var stringValue = Expression.Call(Expression.Property(t, property.Name),
                    toStringMethod);

                var updated = Expression.Call(stringValue, toLowerMethod);

                var nextExpression = Expression.Call(updated,
                    containsMethod,
                    Expression.Constant(searchText.ToLower()));

                body = Expression.OrElse(body, nextExpression);
            }

            return Expression.Lambda<Func<T, bool>>(body, t);
        }
    }
}
