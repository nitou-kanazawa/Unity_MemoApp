using System;
using Project.Domain.Shared;

namespace Project.Domain.Common.Model {
    
    public class Name : ValueObject<Name> {
        
        /// <summary>
        /// ���D
        /// </summary>
        public string FirstName { get; }
        
        /// <summary>
        /// ���D
        /// </summary>
        public string LastName { get; }

        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^�D
        /// </summary>
        public Name(string firstName, string lastName) {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("���O�i���j�͋�ł����Ă͂����܂���B");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("���O�i���j�͋�ł����Ă͂����܂���B");

            FirstName = firstName;
            LastName = lastName;
        }

        /// <summary>
        /// �n�b�V���l�̎擾�D
        /// </summary>
        public override string ToString() => $"{LastName} {FirstName}";

        /// <summary>
        /// ������ւ̕ϊ��D
        /// </summary>
        public override int GetHashCode() => HashCode.Combine(FirstName, LastName);
        
        
        /// ----------------------------------------------------------------------------
        // Protected Method

        /// <summary>
        /// �l�̔�r���W�b�N�D
        /// </summary>
        protected override bool EqualsCore(Name other) {
            return FirstName == other.FirstName 
                && LastName == other.LastName;
        }

    }
}
