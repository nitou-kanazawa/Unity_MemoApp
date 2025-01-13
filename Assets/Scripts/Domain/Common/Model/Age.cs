using System;
using Project.Domain.Shared;

namespace Project.Domain.Common.Model {

    public enum AgeCategory {
        /// <summary> 
        /// 子供
        /// </summary>
        Child,

        /// <summary> 
        /// 大人
        /// </summary>
        Adult,

        /// <summary> 
        /// 老人
        /// </summary>
        Senior,
    }

    /// <summary>
    /// 年齢を表すValueObject．
    /// </summary>
    public class Age : ValueObject<Age> {

        /// <summary>
        /// 生年月日．
        /// </summary>
        public DateTime BirthDate { get; }

        /// <summary>
        /// 年齢．
        /// </summary>
        public int Value => CalculateAge();

        /// <summary>
        /// 年齢区分．
        /// </summary>
        public AgeCategory Category => GetCategory();


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public Age(DateTime birthDate) {
            if (birthDate > DateTime.Now) {
                throw new ArgumentOutOfRangeException(nameof(birthDate), "生年月日は現在の日付以前である必要があります。");
            }

            BirthDate = birthDate.Date; // 時間を無視するためDateを使用
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
            return $"BirthDate: {BirthDate:yyyy-MM-dd}, Age: {Value}, Category: {Category}";
        }



        /// ----------------------------------------------------------------------------
        // Protected Method

        /// <summary>
        /// 値の比較ロジック．
        /// </summary>
        protected override bool EqualsCore(Age other) {
            // [NOTE] 等値比較は生年月日ではなく、年齢値で行う.
            return this.Value == other.Value;
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// 生年月日を基にした年齢を計算
        /// </summary>
        private int CalculateAge() {
            var today = DateTime.Today;
            var age = today.Year - BirthDate.Year;

            // 誕生日がまだ来ていない場合、年齢を1減らす
            if (BirthDate > today.AddYears(-age)) {
                age--;
            }

            return age;
        }

        /// <summary>
        /// 年齢を基にした区分を取得
        /// </summary>
        private AgeCategory GetCategory() =>
            Value switch {
                < 18 => AgeCategory.Child,
                < 65 => AgeCategory.Adult,
                _ => AgeCategory.Senior
            };
    }
}
