using System;
using Project.Domain.Shared;

namespace Project.Domain.Memos.Model {

    /// <summary>
    /// タグのIDを表すValueObject．
    /// </summary>
    public sealed class TagId : ValueObject<TagId>{

        public Guid Value { get; }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public TagId(Guid value) {
            if (value == Guid.Empty)
                throw new ArgumentException("TagId cannot be empty.", nameof(value));

            Value = value;
        }

        /// <summary>
        /// ハッシュ値の取得．
        /// </summary>
        public override int GetHashCode() {
            return Value.GetHashCode();
        }

        /// <summary>
        /// 文字列への変換．
        /// </summary>
        public override string ToString() {
            return Value.ToString();
        }


        /// ----------------------------------------------------------------------------
        // Protected Method

        /// <summary>
        /// 値の比較ロジック．
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
