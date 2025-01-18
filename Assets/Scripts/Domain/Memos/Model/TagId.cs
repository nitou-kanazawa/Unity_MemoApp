using System;
using Project.Domain.Shared;

namespace Project.Domain.Memos.Model {

    /// <summary>
    /// �^�O��ID��\��ValueObject�D
    /// </summary>
    public sealed class TagId : IdentifierBase<TagId> {

        /// <summary>
        /// �R���X�g���N�^�D
        /// </summary>
        public TagId(Guid value) : base(value) { }

        /// <summary>
        /// �R���X�g���N�^�D
        /// </summary>
        public TagId() { }

        /// <summary>
        /// �����D
        /// </summary>
        public TagId Clone() {
            return new TagId(Value);
        }
    }
}
