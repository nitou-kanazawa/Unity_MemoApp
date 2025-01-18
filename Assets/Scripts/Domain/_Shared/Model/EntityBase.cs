using System;

namespace Project.Domain.Shared {

    public abstract class EntityBase<T>
        where T : IdentifierBase<T>, new() {

        public T Id { get; }


        public EntityBase(T id) {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        public override bool Equals(object obj) {
            var other = obj as EntityBase<T>;
            if (other == null) {
                return false;
            }
            return Id.Equals(other.Id);
        }

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() {
            return $"{GetType().Name} [Id={Id}]";
        }


        /// ----------------------------------------------------------------------------
        #region Static

        public static bool operator ==(EntityBase<T> left, EntityBase<T> right) => Equals(left, right);
        public static bool operator !=(EntityBase<T> left, EntityBase<T> right) => !Equals(left, right);
        #endregion
    }
}
