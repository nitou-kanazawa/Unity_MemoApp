using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Project.View.Memos {

    public sealed class MemoCellView : MonoBehaviour {

        [SerializeField] Button _button;
        [SerializeField] TextMeshProUGUI _titleText;

        /// <summary>
        /// インデックス．
        /// </summary>
        public int Index { get; internal set; } = -1;

        /// <summary>
        /// 
        /// </summary>
        public IObservable<int> OnCliced => _button.OnClickAsObservable().Select(_ => Index);


        /// ----------------------------------------------------------------------------
        // Public Method

        public void SetTitleText(string text) {
            _titleText.text = text;
        }
    }
}
