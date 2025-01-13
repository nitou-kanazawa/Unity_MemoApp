using System;
using Project.Domain.Shared;

namespace Project.Domain.Memos.Model {

    /// <summary>
    /// タグを表すValueObject．
    /// </summary>
    public class Tag : ValueObject<Tag> {

        public string Name { get; }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public Tag(string name) {
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


        /// ----------------------------------------------------------------------------
        // Protected Method

        /// <summary>
        /// 値の比較ロジック．
        /// </summary>
        protected override bool EqualsCore(Tag other) {
            return this.Name == other.Name;
        }
        
    }
}
