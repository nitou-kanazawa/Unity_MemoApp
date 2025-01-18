using System;
using Project.Domain.Shared;

namespace Project.Domain.Memos.Model {

    public sealed class MemoId : IdentifierBase<MemoId> {


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public MemoId(Guid value) : base(value) { }

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public MemoId() {}

    }
}
