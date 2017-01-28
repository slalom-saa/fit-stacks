using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Slalom.Stacks.Domain
{
    public abstract class ValueObject<T> : IEquatable<T> where T : ValueObject<T>
    {
        static IList<FieldInfo> _fields = new List<FieldInfo>();

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var other = obj as T;

            return Equals(other);
        }

        public override int GetHashCode()
        {
            var fields = GetFields().Select(field => field.GetValue(this)).Where(value => value != null).ToList();
            fields.Add(GetType());
            return GetHashCode(fields.ToArray());
        }

        public static int GetHashCode(params object[] objects)
        {
            unchecked
            {
                int hash = 17;
                foreach (var item in objects)
                {
                    hash = hash * 31 + item.GetHashCode();
                }
                return hash;
            }
        }

        public virtual bool Equals(T other)
        {
            if (other == null)
                return false;

            var t = GetType();
            var otherType = other.GetType();

            if (t != otherType)
                return false;

            var fields = GetFields();

            foreach (var field in fields)
            {
                var value1 = field.GetValue(other);
                var value2 = field.GetValue(this);

                if (value1 == null)
                {
                    if (value2 != null)
                        return false;
                }
                else if (!value1.Equals(value2))
                    return false;
            }

            return true;
        }

        public static bool operator ==(ValueObject<T> x, ValueObject<T> y)
        {
            return ReferenceEquals(x, y) || x.Equals(y);
        }

        public static bool operator !=(ValueObject<T> x, ValueObject<T> y)
        {
            return !(x == y);
        }

        IEnumerable<FieldInfo> GetFields()
        {
            if (!_fields.Any())
                _fields = new List<FieldInfo>(BuildFieldCollection());
            return _fields;
        }

        IEnumerable<FieldInfo> BuildFieldCollection()
        {
            var t = typeof(T);
            var fields = new List<FieldInfo>();

            while (t != typeof(object))
            {
                var typeInfo = t.GetTypeInfo();

                fields.AddRange(typeInfo.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance));
                var fieldInfoCache = typeInfo.GetField("_fields");
                fields.Remove(fieldInfoCache);
                t = typeInfo.BaseType;
            }
            return fields;
        }
    }
}
