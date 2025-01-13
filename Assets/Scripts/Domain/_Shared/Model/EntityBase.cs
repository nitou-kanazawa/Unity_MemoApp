using System;

namespace Project.Domain.Shared {

    /// <summary>
    /// 識別可能なEntityの基底クラス．
    /// </summary>
    public abstract class EntityBase<T> {
        
        public T Id { get; }

        public EntityBase(T id) {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }
    }
}
