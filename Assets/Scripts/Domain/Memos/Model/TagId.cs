using System;
using Project.Domain.Shared;

namespace Project.Domain.Memos.Model {

    /// <summary>
    /// タグのIDを表すValueObject．
    /// </summary>
    public sealed class TagId : IdentifierBase<TagId> {

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public TagId(Guid value) : base(value) { }

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public TagId() { }

        /// <summary>
        /// 複製．
        /// </summary>
        public TagId Clone() {
            return new TagId(Value);
        }
    }
}
