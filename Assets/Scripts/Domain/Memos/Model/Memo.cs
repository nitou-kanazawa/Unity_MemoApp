using System;
using System.Collections.Generic;
using Project.Domain.Shared;

namespace Project.Domain.Memos.Model {

    /// <summary>
    /// ������\��Entity�D
    /// </summary>
    public class Memo : EntityBase<Guid> {

        private readonly List<Tag> _tags;

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
        public IReadOnlyList<Tag> Tags => _tags;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^�D
        /// </summary>
        public Memo(Guid id, string title, Content content) : base(id){
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Content = content ?? throw new ArgumentNullException(nameof(content));
            _tags = new List<Tag>();
        }

        /// <summary>
        /// �^�C�g�����X�V����D
        /// </summary>
        public void UpdateTitle(string title) {
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }

        /// <summary>
        /// �{�����X�V����D
        /// </summary>
        public void UpdateContent(Content content) {
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        /// <summary>
        /// �^�O��ǉ�����D
        /// </summary>
        public void AddTag(Tag tag) {
            if (!_tags.Contains(tag)) {
                _tags.Add(tag);
            }
        }

        /// <summary>
        /// �^�O���폜����D
        /// </summary>
        public void RemoveTag(Tag tag) {
            _tags.Remove(tag);
        }


        /// ----------------------------------------------------------------------------
        #region Static

        public static Memo CreateNew(string title, Content content) {
            var id = Guid.NewGuid();    // [NOTE] Id�������W�b�N�͂����ɉB��
            var now = DateTime.UtcNow;

            return new Memo(id, title, content);
        }

        #endregion
    }
}
