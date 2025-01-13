using System;
using Project.Domain.Shared;

namespace Project.Domain.Memos.Model {

    /// <summary>
    /// タグを表すEntity．
    /// </summary>
    public class Tag : EntityBase<Guid> {

        public string Name { get; }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public Tag(Guid id, string name) : base(id) {

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
        #region Static

        public static Tag CreateNew(string name) {
            var id = Guid.NewGuid();    // [NOTE] Id生成ロジックはここに隠蔽

            return new Tag(id, name);
        }

        #endregion
    }
}
