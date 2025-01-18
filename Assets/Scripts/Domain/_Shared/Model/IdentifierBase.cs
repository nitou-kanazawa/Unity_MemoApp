using System;

namespace Project.Domain.Shared {

    /// <summary>
    /// 識別子を表すValueObject．
    /// </summary>
    public abstract class IdentifierBase<T> : IEquatable<T> 
        where T : IdentifierBase<T> , new(){
        
        public Guid Value { get; }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public IdentifierBase(Guid value) {
            if(value == Guid.Empty)
                throw new ArgumentException("Identifier value cannot be Guid.Empty.", nameof(value));

            Value = value;
        }

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public IdentifierBase(): this(Guid.NewGuid()) {}

        /// <summary>
        /// 
        /// </summary>
        public override bool Equals(object obj) {
            return obj is IdentifierBase<T> other && Equals(other);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Equals(T other) {
            return other != null && Value.Equals(other.Value);
        }

        /// <summary>
        /// ハッシュ値の取得．
        /// </summary>
        public override int GetHashCode() {
            return Value.GetHashCode();
        }

        /// <summary>
        /// 文字列への変換．
        /// </summary>
        public override string ToString() {
            return $"{GetType().Name}({Value.ToString()})";
        }


        /// ----------------------------------------------------------------------------
        #region Static
        public static bool operator ==(IdentifierBase<T> left, IdentifierBase<T> right) => Equals(left, right);
        public static bool operator !=(IdentifierBase<T> left, IdentifierBase<T> right) => !Equals(left, right);
        #endregion
    }
}
