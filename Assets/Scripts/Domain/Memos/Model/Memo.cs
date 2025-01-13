using System;
using System.Collections.Generic;
using Project.Domain.Shared;

namespace Project.Domain.Memos.Model {

    /// <summary>
    /// メモを表すEntity．
    /// </summary>
    public class Memo : EntityBase<Guid> {

        private readonly List<Tag> _tags;

        /// <summary>
        /// タイトル．
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// 本文．
        /// </summary>
        public Content Content { get; private set; }

        /// <summary>
        /// 付与されたタグ一覧．
        /// </summary>
        public IReadOnlyList<Tag> Tags => _tags;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public Memo(Guid id, string title, Content content) : base(id){
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Content = content ?? throw new ArgumentNullException(nameof(content));
            _tags = new List<Tag>();
        }

        /// <summary>
        /// タイトルを更新する．
        /// </summary>
        public void UpdateTitle(string title) {
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }

        /// <summary>
        /// 本文を更新する．
        /// </summary>
        public void UpdateContent(Content content) {
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        /// <summary>
        /// タグを追加する．
        /// </summary>
        public void AddTag(Tag tag) {
            if (!_tags.Contains(tag)) {
                _tags.Add(tag);
            }
        }

        /// <summary>
        /// タグを削除する．
        /// </summary>
        public void RemoveTag(Tag tag) {
            _tags.Remove(tag);
        }


        /// ----------------------------------------------------------------------------
        #region Static

        public static Memo CreateNew(string title, Content content) {
            var id = Guid.NewGuid();    // [NOTE] Id生成ロジックはここに隠蔽
            var now = DateTime.UtcNow;

            return new Memo(id, title, content);
        }

        #endregion
    }
}
