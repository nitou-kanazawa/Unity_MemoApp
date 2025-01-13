using UnityEngine;
using Project.Domain.Shared;
using System;

namespace Project.Domain.Common.Model {

    /// <summary>
    /// ���z��\��ValueObject�D
    /// </summary>
    public sealed class Money : ValueObject<Money> {

        /// <summary>
        /// ���z�D
        /// </summary>
        public int Amount { get; private set; }

        /// <summary>
        /// �ʉݒP�ʁD
        /// </summary>
        public static readonly string Currency = "$";


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^�D
        /// </summary>
        /// <param name="amount">���z�D</param>
        /// <param name="currency">�ʉݒP�ʁD</param>
        public Money(int amount) {
            if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be negative.");

            Amount = amount;
        }

        /// <summary>
        /// �n�b�V���l�̎擾�D
        /// </summary>
        public override int GetHashCode() {
            return HashCode.Combine(Amount, Currency);
        }

        /// <summary>
        /// ������ւ̕ϊ��D
        /// </summary>
        public override string ToString() {
            return $"{Amount:F2} {Currency}";
        }

        /// <summary>
        /// �l�̔�r���W�b�N�D
        /// </summary>
        protected override bool EqualsCore(Money other) {
            return Amount == other.Amount;
        }

    }
}
