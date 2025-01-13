using System;
using System.Collections.Generic;
using Project.Domain.Shared;

namespace Project.Domain.Memos.Model {

    /// <summary>
    /// ������\��Entity�D
    /// </summary>
    public class Memo : EntityBase<Guid> {

        // ����
        private readonly List<TagId> _tags = new();

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
        public IReadOnlyList<TagId> Tags => _tags;

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
        public Memo(Guid id, string title, Content content, DateTime createdAt, DateTime updatedAt) 
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
            Title = title ?? throw new ArgumentNullException(nameof(title));
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
        public void AddTag(TagId tag) {
            if (tag == null) throw new ArgumentNullException(nameof(tag));
            if (_tags.Contains(tag)) return;

            // �ǉ�
            _tags.Add(tag);
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// �^�O���폜����D
        /// </summary>
        public void RemoveTag(TagId tag) {
            if (tag == null) throw new ArgumentNullException(nameof(tag));
            if (!_tags.Contains(tag)) return;

            // �폜
            _tags.Remove(tag);
            UpdatedAt = DateTime.UtcNow;
        }


        /// ----------------------------------------------------------------------------
        #region Static

        public static Memo CreateNew(string title, Content content) {
            var id = Guid.NewGuid();    // [NOTE] Id�������W�b�N�͂����ɉB��
            var now = DateTime.UtcNow;

            return new Memo(id, title, content, now, now);
        }

        #endregion
    }
}
