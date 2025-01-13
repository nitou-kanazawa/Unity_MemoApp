using System;
using System.Text.RegularExpressions;
using Project.Domain.Shared;

namespace Project.Domain.Common.Model {

    /// <summary>
    /// �d�b�ԍ���\��ValueObject�D
    /// </summary>
    public class PhoneNumber : ValueObject<PhoneNumber> {

        private static readonly Regex PhoneNumberRegex = new(@"^\+?[1-9]\d{1,14}$");

        /// <summary>
        /// �d�b�ԍ��D
        /// </summary>
        public string Value { get; }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^�D
        /// </summary>
        public PhoneNumber(string phoneNumber) {
            if (string.IsNullOrWhiteSpace(phoneNumber)) {
                throw new ArgumentException("�d�b�ԍ��͋�ł����Ă͂Ȃ�܂���B", nameof(phoneNumber));
            }

            if (!PhoneNumberRegex.IsMatch(phoneNumber)) {
                throw new ArgumentException("�����ȓd�b�ԍ��`���ł��B", nameof(phoneNumber));
            }

            Value = phoneNumber;
        }

        /// <summary>
        /// �n�b�V���l�̎擾�D
        /// </summary>
        public override int GetHashCode() => Value.GetHashCode();

        /// <summary>
        /// ������ւ̕ϊ��D
        /// </summary>
        public override string ToString() => Value;


        /// ----------------------------------------------------------------------------
        // Protected Method

        /// <summary>
        /// �l�̔�r���W�b�N�D
        /// </summary>
        protected override bool EqualsCore(PhoneNumber other) {
            return Value == other.Value;
        }
    }
}