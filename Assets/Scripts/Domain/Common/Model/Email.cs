using System;
using System.Text.RegularExpressions;
using Project.Domain.Shared;

namespace Project.Domain.Common.Model {

    /// <summary>
    /// Emailを表すValueObject．
    /// </summary>
    public class Email : ValueObject<Email> {

        private static readonly Regex EmailRegex = new(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");

        /// <summary>
        /// Emailアドレス．
        /// </summary>
        public string Value { get; }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public Email(string emailAddress) {
            if (string.IsNullOrWhiteSpace(emailAddress)) {
                throw new ArgumentException("Emailアドレスは空であってはなりません。", nameof(emailAddress));
            }

            if (!EmailRegex.IsMatch(emailAddress)) {
                throw new ArgumentException("無効なEmailアドレス形式です。", nameof(emailAddress));
            }

            Value = emailAddress;
        }

        public override string ToString() => Value;

        public override int GetHashCode() => Value.GetHashCode();


        /// ----------------------------------------------------------------------------
        // Protected Method

        /// <summary>
        /// 値の比較ロジック．
        /// </summary>
        protected override bool EqualsCore(Email other) {
            return Value == other.Value;
        }
    }
}

