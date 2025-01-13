using System;
using Project.Domain.Shared;

namespace Project.Domain.Common.Model {

    // [NOTE]
    // - 数量が負になることはあり得ない．
    // - 数量同士の加算，減算が行える．

    /// <summary>
    /// 数量を表すValueObject．
    /// </summary>
    public sealed class Quantity : ValueObject<Quantity> {       

        public int Value { get; }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public Quantity(int value) {
            if (!Quantity.IsValid(value)) {
                throw new ArgumentOutOfRangeException(nameof(value), $"Quantity must be between {MIN} and {MAX}.");
            }

            Value = value;
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
            return Value.ToString();
        }

        /// <summary>
        /// 指定された数量を加算した新しい<see cref="Quantity"/>を返す．
        /// </summary>
        public Quantity Add(Quantity other) {

            var addedValue = this.Value + other.Value;
            if (!IsValid(addedValue)) {
                throw new InvalidOperationException("Addition result is out of range.");
            }

            return new Quantity(addedValue);
        }

        /// <summary>
        /// 指定された数量を減算した新しい<see cref="Quantity"/>を返す．
        /// </summary>
        public Quantity Subtract(Quantity other) {

            var subtractedValue = this.Value - other.Value;
            if (!IsValid(subtractedValue)) {
                throw new InvalidOperationException("Subtraction result is out of range.");
            }

            return new Quantity(subtractedValue);
        }


        /// ----------------------------------------------------------------------------
        // Protected Method

        /// <summary>
        /// 値の比較ロジック．
        /// </summary>
        protected override bool EqualsCore(Quantity other) {
            return Value == other.Value;
        }


        /// ----------------------------------------------------------------------------
        #region Static

        public static readonly int MIN = 0;
        public static readonly int MAX = 999;

        private static bool IsValid(int value) {
            return MIN <= value && value <= MAX;
        }
        #endregion
    }
}
