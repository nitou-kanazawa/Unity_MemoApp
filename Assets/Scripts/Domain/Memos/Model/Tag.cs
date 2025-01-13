using System;
using Project.Domain.Shared;

namespace Project.Domain.Memos.Model {

    /// <summary>
    /// �^�O��\��Entity�D
    /// </summary>
    public class Tag : EntityBase<Guid> {

        public string Name { get; }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^�D
        /// </summary>
        public Tag(Guid id, string name) : base(id) {

            if (string.IsNullOrEmpty(name)) {
                throw new InvalidOperationException();
            }
            Name = name;
        }
        
        /// <summary>
        /// �n�b�V���l�̎擾�D
        /// </summary>
        public override int GetHashCode() {
            return Name.GetHashCode();
        }

        /// <summary>
        /// ������ւ̕ϊ��D
        /// </summary>
        public override string ToString() {
            return Name;
        }


        /// ----------------------------------------------------------------------------
        #region Static

        public static Tag CreateNew(string name) {
            var id = Guid.NewGuid();    // [NOTE] Id�������W�b�N�͂����ɉB��

            return new Tag(id, name);
        }

        #endregion
    }
}
