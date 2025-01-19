using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Project.View.Memos;

namespace Project.Develop.View.Memos {

    public sealed class Develop_MemoListView : MonoBehaviour {

        [SerializeField] MemoListView _listView;

        private List<ViewData> _source;


        async void Start() {


            _source = Enumerable.Range(0, 30)
                .Select(i => new ViewData($"Memo_{i}"))
                .ToList();


            _listView.BindToDataSetter(_source.Count, SetData);
        }


        private void SetData(MemoCellView cell, int index) {

            var data = _source[index];

            cell.SetTitleText(data.title);
        }


        public struct ViewData {
            public string title;

            public override string ToString() => $"Title : {title}";

            public ViewData(string title) {
                this.title = title;
            }
        }
    }
}
