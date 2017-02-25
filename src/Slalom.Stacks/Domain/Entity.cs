using System.Diagnostics.CodeAnalysis;
using Slalom.Stacks.Utilities.NewId;

namespace Slalom.Stacks.Domain
{
    /// <summary>
    ///     The base class for an <see href="http://bit.ly/2dViCg3">Entity</see>,
    ///     an object that represents a thread of continuity and identity, going through a lifecycle.
    /// </summary>
    /// <seealso href="http://bit.ly/2dVQsXu">Domain-Driven Design: Tackling Complexity in the Heart of Software</seealso>
    public abstract class Entity : IEntity
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Entity" /> class.
        /// </summary>
        protected Entity()
            : this(NewId.NextId())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Entity" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        protected Entity(string id)
        {
            Id = id;
        }

        /// <summary>
        ///     Gets or sets the entity identifier.
        /// </summary>
        /// <value>The entity identifier.</value>
        public string Id { get; }

        /// <summary>
        ///     Gets the keys for this instance.  It may be just the identifier or a combination of identifier and human readable
        ///     keys.
        /// </summary>
        /// <returns>Returns the keys for this instance.</returns>
        public virtual object GetKeys()
        {
            return new
            {
                Id
            };
        }

        #region Equality Members

        /// <summary>
        /// Implements the ==.
        /// </summary>
        /// <param name="a">The first item to compare.</param>
        /// <param name="b">The second item to compare.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;
            return a.Equals(b);
        }

        /// <summary>
        /// Implements the !=.
        /// </summary>
        /// <param name="a">The first item to compare.</param>
        /// <param name="b">The second item to compare.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }


        /// <summary>
        ///     Determines whether the specified <see cref="Entity" /> is equal to this instance.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        protected bool Equals(Entity other)
        {
            return other != null && Id.Equals(other.Id);
        }

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Entity) obj);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            return (GetType() + Id).GetHashCode();
        }

        #endregion
    }
}