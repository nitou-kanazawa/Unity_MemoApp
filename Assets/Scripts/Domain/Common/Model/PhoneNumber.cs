using System;
using System.Text.RegularExpressions;
using Project.Domain.Shared;

namespace Project.Domain.Common.Model {

    /// <summary>
    /// 電話番号を表すValueObject．
    /// </summary>
    public class PhoneNumber : ValueObject<PhoneNumber> {

        private static readonly Regex PhoneNumberRegex = new(@"^\+?[1-9]\d{1,14}$");

        /// <summary>
        /// 電話番号．
        /// </summary>
        public string Value { get; }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public PhoneNumber(string phoneNumber) {
            if (string.IsNullOrWhiteSpace(phoneNumber)) {
                throw new ArgumentException("電話番号は空であってはなりません。", nameof(phoneNumber));
            }

            if (!PhoneNumberRegex.IsMatch(phoneNumber)) {
                throw new ArgumentException("無効な電話番号形式です。", nameof(phoneNumber));
            }

            Value = phoneNumber;
        }

        /// <summary>
        /// ハッシュ値の取得．
        /// </summary>
        public override int GetHashCode() => Value.GetHashCode();

        /// <summary>
        /// 文字列への変換．
        /// </summary>
        public override string ToString() => Value;


        /// ----------------------------------------------------------------------------
        // Protected Method

        /// <summary>
        /// 値の比較ロジック．
        /// </summary>
        protected override bool EqualsCore(PhoneNumber other) {
            return Value == other.Value;
        }
    }
}