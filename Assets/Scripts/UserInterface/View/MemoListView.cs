using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;

// [REF]
//  LIGHT11: 効率的なスクロールビューを実装するためのOSS『Loop Scroll Rect』使い方まとめ https://light11.hatenadiary.com/entry/2022/05/16/201949

namespace Project.View.Memos {

    public class MemoListView : MonoBehaviour, LoopScrollPrefabSource {

        [SerializeField] LoopScrollRect _scrollRect;

        // Prefab source
        [SerializeField] MemoCellView _cellPrefab;
        [SerializeField] Transform _poolParent;
        private ObjectPool<GameObject> _pool;

        // Data source
        private DataProvider _dataProvider = null;

        private Subject<int> _onCellClickedSubject = new();


        /// <summary>
        /// リスト要素がクリックされた時に通知するObservable．
        /// </summary>
        public IObservable<int> OnCellClicked => _onCellClickedSubject.AsObservable();


        /// ----------------------------------------------------------------------------
        // Lifecycle Events

        private void Awake() {
            _pool = new ObjectPool<GameObject>(
                createFunc: OnCreate,
                actionOnGet: OnGet,
                actionOnRelease: OnRelease);

            // 
            _scrollRect.prefabSource = this;
            _scrollRect.totalCount = 0;
        }

        private void OnDestroy() {
            _onCellClickedSubject.Dispose();
        }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// データバインド．
        /// </summary>
        public void BindToDataSetter(int count, Action<MemoCellView, int> dataSetter) {

            _dataProvider = new DataProvider(dataSetter);

            // View setup
            _scrollRect.dataSource = _dataProvider;
            _scrollRect.totalCount = count;
            _scrollRect.RefillCells();

        }

        /// <summary>
        /// 
        /// </summary>
        public void RefreshCells() {
            _scrollRect.RefreshCells();
        }


        GameObject LoopScrollPrefabSource.GetObject(int index) => _pool.Get();
        void LoopScrollPrefabSource.ReturnObject(Transform trans) => _pool.Release(trans.gameObject);


        /// ----------------------------------------------------------------------------
        // Private Method

        private GameObject OnCreate() {
            var cell = Instantiate(_cellPrefab, _poolParent);
            cell.OnCliced
                .TakeUntilDisable(cell)
                .Subscribe(index => {
                    _onCellClickedSubject.OnNext(index);
                    Debug.Log($"On click [{index}]");
                })
                .AddTo(this);


            return cell.gameObject;
        }

        private void OnGet(GameObject item) {
            item.transform.SetParent(_scrollRect.content);
            item.SetActive(true);
        }

        private void OnRelease(GameObject item) {
            item.SetActive(false);
            item.transform.SetParent(_poolParent);
        }

        private void PublishClickEvent(int index) {
            _onCellClickedSubject.OnNext(index);

            Debug.Log($"[{index}] clicked !!");
        }


        /// ----------------------------------------------------------------------------
        #region Inner Definition

        private class DataProvider : LoopScrollDataSource, IDisposable {

            private Action<MemoCellView, int> _dataSetter;

            public DataProvider(Action<MemoCellView, int> dataSetter) {
                _dataSetter = dataSetter;
            }

            public void Dispose() {
                _dataSetter = null;
            }

            void LoopScrollDataSource.ProvideData(Transform transform, int index) {
                if (!transform.TryGetComponent<MemoCellView>(out var cell)) {
                    Debug.LogWarning("");
                    return;
                }

                cell.Index = index;
                _dataSetter.Invoke(cell, index);
            }
        }
        #endregion


        /// ----------------------------------------------------------------------------
#if UNITY_EDITOR
        private void OnValidate() {
            if (_scrollRect == null)
                _scrollRect = GetComponent<LoopScrollRect>();
        }
#endif
    }
}
