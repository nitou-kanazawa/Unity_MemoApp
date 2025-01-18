using System;
using Project.Domain.Shared;

namespace Project.Domain.Memos.Model {

    public sealed class MemoId : IdentifierBase<MemoId> {


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^�D
        /// </summary>
        public MemoId(Guid value) : base(value) { }

        /// <summary>
        /// �R���X�g���N�^�D
        /// </summary>
        public MemoId() {}

    }
}
