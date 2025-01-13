using System;
using Project.Domain.Shared;

namespace Project.Domain.Memos.Model {

    /// <summary>
    /// タグを表すEntity．
    /// </summary>
    public class Tag : EntityBase<TagId> {

        public string Name { get; }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public Tag(TagId id, string name) : base(id) {

            if (string.IsNullOrEmpty(name)) {
                throw new InvalidOperationException();
            }
            Name = name;
        }
        
        /// <summary>
        /// ハッシュ値の取得．
        /// </summary>
        public override int GetHashCode() {
            return Name.GetHashCode();
        }

        /// <summary>
        /// 文字列への変換．
        /// </summary>
        public override string ToString() {
            return Name;
        }
    }
}
