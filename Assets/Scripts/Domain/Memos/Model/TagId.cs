using System;
using Project.Domain.Shared;

namespace Project.Domain.Memos.Model {

    /// <summary>
    /// �^�O��ID��\��ValueObject�D
    /// </summary>
    public sealed class TagId : ValueObject<TagId>{

        public Guid Value { get; }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^�D
        /// </summary>
        public TagId(Guid value) {
            if (value == Guid.Empty)
                throw new ArgumentException("TagId cannot be empty.", nameof(value));

            Value = value;
        }

        /// <summary>
        /// �n�b�V���l�̎擾�D
        /// </summary>
        public override int GetHashCode() {
            return Value.GetHashCode();
        }

        /// <summary>
        /// ������ւ̕ϊ��D
        /// </summary>
        public override string ToString() {
            return Value.ToString();
        }


        /// ----------------------------------------------------------------------------
        // Protected Method

        /// <summary>
        /// �l�̔�r���W�b�N�D
        /// </summary>
        protected override bool EqualsCore(TagId other) {
            return this.Value == other.Value;
        }


        /// ----------------------------------------------------------------------------
        #region Static

        public static TagId New() {
            return new TagId(Guid.NewGuid());
        }

        #endregion
    }
}
