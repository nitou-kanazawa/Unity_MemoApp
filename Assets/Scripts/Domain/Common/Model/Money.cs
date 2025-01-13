using UnityEngine;
using Project.Domain.Shared;
using System;

namespace Project.Domain.Common.Model {

    /// <summary>
    /// 金額を表すValueObject．
    /// </summary>
    public sealed class Money : ValueObject<Money> {

        /// <summary>
        /// 金額．
        /// </summary>
        public int Amount { get; private set; }

        /// <summary>
        /// 通貨単位．
        /// </summary>
        public static readonly string Currency = "$";


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        /// <param name="amount">金額．</param>
        /// <param name="currency">通貨単位．</param>
        public Money(int amount) {
            if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be negative.");

            Amount = amount;
        }

        /// <summary>
        /// ハッシュ値の取得．
        /// </summary>
        public override int GetHashCode() {
            return HashCode.Combine(Amount, Currency);
        }

        /// <summary>
        /// 文字列への変換．
        /// </summary>
        public override string ToString() {
            return $"{Amount:F2} {Currency}";
        }

        /// <summary>
        /// 値の比較ロジック．
        /// </summary>
        protected override bool EqualsCore(Money other) {
            return Amount == other.Amount;
        }

    }
}
