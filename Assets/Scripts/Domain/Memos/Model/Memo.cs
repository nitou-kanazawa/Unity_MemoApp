using System;
using System.Collections.Generic;
using Project.Domain.Shared;

namespace Project.Domain.Memos.Model {

    /// <summary>
    /// ������\��Entity�D
    /// </summary>
    public class Memo : EntityBase<MemoId> {

        // ����
        private readonly HashSet<TagId> _tags = new();

        /// <summary>
        /// �^�C�g���D
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// �{���D
        /// </summary>
        public Content Content { get; private set; }

        /// <summary>
        /// �t�^���ꂽ�^�O�ꗗ�D
        /// </summary>
        public IReadOnlyCollection<TagId> Tags => _tags;

        /// <summary>
        /// �쐬�����D
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// �X�V�����D
        /// </summary>
        public DateTime UpdatedAt { get; private set; }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^�D
        /// </summary>
        public Memo(MemoId id, string title, Content content, DateTime createdAt, DateTime updatedAt) 
            : base(id){
            
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Content = content ?? throw new ArgumentNullException(nameof(content));
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        /// <summary>
        /// ������ւ̕ϊ��D
        /// </summary>
        public override string ToString() {
            return $"{Title}";
        }

        /// <summary>
        /// �^�C�g�����X�V����D
        /// </summary>
        public void UpdateTitle(string title) {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title));

            Title = title;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// �{�����X�V����D
        /// </summary>
        public void UpdateContent(Content content) {
            Content = content ?? throw new ArgumentNullException(nameof(content));
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// �^�O��ǉ�����D
        /// </summary>
        public void AddTag(TagId tagId) {
            if (tagId == null) 
                throw new ArgumentNullException(nameof(tagId));
            if (_tags.Contains(tagId))
                throw new InvalidOperationException($"Tag with ID '{tagId.Value}' is already associated with this memo.");

            // �ǉ�
            _tags.Add(tagId);
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// �^�O���폜����D
        /// </summary>
        public void RemoveTag(TagId tagId) {
            if (tagId == null) 
                throw new ArgumentNullException(nameof(tagId));
            if (!_tags.Contains(tagId))
                throw new InvalidOperationException($"Tag with ID '{tagId.Value}' is not associated with this memo.");

            // �폜
            _tags.Remove(tagId);
            UpdatedAt = DateTime.UtcNow;
        }


        /// ----------------------------------------------------------------------------
        #region Static

        public static Memo CreateNew(string title, Content content) {
            var now = DateTime.UtcNow;

            return new Memo(new MemoId(), title, content, now, now);
        }

        #endregion
    }
}
