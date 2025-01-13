using System;
using Project.Domain.Shared;

namespace Project.Domain.Common.Model {

    public enum AgeCategory {
        /// <summary> 
        /// �q��
        /// </summary>
        Child,

        /// <summary> 
        /// ��l
        /// </summary>
        Adult,

        /// <summary> 
        /// �V�l
        /// </summary>
        Senior,
    }

    /// <summary>
    /// �N���\��ValueObject�D
    /// </summary>
    public class Age : ValueObject<Age> {

        /// <summary>
        /// ���N�����D
        /// </summary>
        public DateTime BirthDate { get; }

        /// <summary>
        /// �N��D
        /// </summary>
        public int Value => CalculateAge();

        /// <summary>
        /// �N��敪�D
        /// </summary>
        public AgeCategory Category => GetCategory();


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^�D
        /// </summary>
        public Age(DateTime birthDate) {
            if (birthDate > DateTime.Now) {
                throw new ArgumentOutOfRangeException(nameof(birthDate), "���N�����͌��݂̓��t�ȑO�ł���K�v������܂��B");
            }

            BirthDate = birthDate.Date; // ���Ԃ𖳎����邽��Date���g�p
        }
        
        /// <summary>
        /// �n�b�V���l�̎擾�D
        /// </summary>
        public override int GetHashCode() {
            return Value.GetHashCode();
        }

        /// <summary>
        /// ������ւ̕ϊ��D
        /// </summary>
        public override string ToString() {
            return $"BirthDate: {BirthDate:yyyy-MM-dd}, Age: {Value}, Category: {Category}";
        }



        /// ----------------------------------------------------------------------------
        // Protected Method

        /// <summary>
        /// �l�̔�r���W�b�N�D
        /// </summary>
        protected override bool EqualsCore(Age other) {
            // [NOTE] ���l��r�͐��N�����ł͂Ȃ��A�N��l�ōs��.
            return this.Value == other.Value;
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// ���N��������ɂ����N����v�Z
        /// </summary>
        private int CalculateAge() {
            var today = DateTime.Today;
            var age = today.Year - BirthDate.Year;

            // �a�������܂����Ă��Ȃ��ꍇ�A�N���1���炷
            if (BirthDate > today.AddYears(-age)) {
                age--;
            }

            return age;
        }

        /// <summary>
        /// �N�����ɂ����敪���擾
        /// </summary>
        private AgeCategory GetCategory() =>
            Value switch {
                < 18 => AgeCategory.Child,
                < 65 => AgeCategory.Adult,
                _ => AgeCategory.Senior
            };
    }
}
