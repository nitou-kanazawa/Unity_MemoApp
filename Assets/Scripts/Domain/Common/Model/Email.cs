using System;
using System.Text.RegularExpressions;
using Project.Domain.Shared;

namespace Project.Domain.Common.Model {

    /// <summary>
    /// Email��\��ValueObject�D
    /// </summary>
    public class Email : ValueObject<Email> {

        private static readonly Regex EmailRegex = new(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");

        /// <summary>
        /// Email�A�h���X�D
        /// </summary>
        public string Value { get; }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^�D
        /// </summary>
        public Email(string emailAddress) {
            if (string.IsNullOrWhiteSpace(emailAddress)) {
                throw new ArgumentException("Email�A�h���X�͋�ł����Ă͂Ȃ�܂���B", nameof(emailAddress));
            }

            if (!EmailRegex.IsMatch(emailAddress)) {
                throw new ArgumentException("������Email�A�h���X�`���ł��B", nameof(emailAddress));
            }

            Value = emailAddress;
        }

        public override string ToString() => Value;

        public override int GetHashCode() => Value.GetHashCode();


        /// ----------------------------------------------------------------------------
        // Protected Method

        /// <summary>
        /// �l�̔�r���W�b�N�D
        /// </summary>
        protected override bool EqualsCore(Email other) {
            return Value == other.Value;
        }
    }
}

