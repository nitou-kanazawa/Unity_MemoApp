using System;
using System.Collections.Generic;
using Project.Domain.Shared;

namespace Project.Domain.Memos.Model {

    /// <summary>
    /// メモを表すEntity．
    /// </summary>
    public class Memo : EntityBase<Guid> {

        // 実装
        private readonly List<TagId> _tags = new();

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
        public IReadOnlyList<TagId> Tags => _tags;

        /// <summary>
        /// 作成日時．
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// 更新日時．
        /// </summary>
        public DateTime UpdatedAt { get; private set; }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public Memo(Guid id, string title, Content content, DateTime createdAt, DateTime updatedAt) 
            : base(id){
            
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Content = content ?? throw new ArgumentNullException(nameof(content));
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        /// <summary>
        /// 文字列への変換．
        /// </summary>
        public override string ToString() {
            return $"{Title}";
        }

        /// <summary>
        /// タイトルを更新する．
        /// </summary>
        public void UpdateTitle(string title) {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// 本文を更新する．
        /// </summary>
        public void UpdateContent(Content content) {
            Content = content ?? throw new ArgumentNullException(nameof(content));
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// タグを追加する．
        /// </summary>
        public void AddTag(TagId tag) {
            if (tag == null) throw new ArgumentNullException(nameof(tag));
            if (_tags.Contains(tag)) return;

            // 追加
            _tags.Add(tag);
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// タグを削除する．
        /// </summary>
        public void RemoveTag(TagId tag) {
            if (tag == null) throw new ArgumentNullException(nameof(tag));
            if (!_tags.Contains(tag)) return;

            // 削除
            _tags.Remove(tag);
            UpdatedAt = DateTime.UtcNow;
        }


        /// ----------------------------------------------------------------------------
        #region Static

        public static Memo CreateNew(string title, Content content) {
            var id = Guid.NewGuid();    // [NOTE] Id生成ロジックはここに隠蔽
            var now = DateTime.UtcNow;

            return new Memo(id, title, content, now, now);
        }

        #endregion
    }
}
