using System;
using UniRx;

// [REF]
//  github: 
//  _: 無限スクロールビューライブラリ 二つの設計思想比較 https://speakerdeck.com/harumak/wu-xian-sukurorubiyuraiburari-er-tufalseshe-ji-si-xiang-bi-jiao?slide=4
//  qiita: LoopScrollRect をスナップさせる試み https://qiita.com/Yamara/items/f426562b5530e08ee67a

namespace UnityEngine.UI {

    public interface IIndexable {
        int Index { get; set; }
    }

    /// <summary>
    /// <see cref="LoopScrollRect"/>に関する拡張メソッド集．
    /// </summary>
    public static class LoopScrollRectExtensions {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scrollRect"></param>
        public static IObservable<Vector2> OnValueChangeAsObservable(this LoopScrollRect scrollRect) {
            return scrollRect.onValueChanged.AsObservable();
        }

        /// <summary>
        /// 
        /// </summary>
        public static IDisposable RegisterDataSource<T>(this LoopScrollRect scrollRect,
            int count, Action<T, int> provideData)
            where T : Component {

            // Data source
            var dataSource = new DataSource<T>(provideData);
            scrollRect.dataSource = dataSource;
            scrollRect.totalCount = count;
            scrollRect.RefillCells();

            // Disposable
            return Disposable.Create(() => {
                if (scrollRect.dataSource == dataSource) {
                    scrollRect.dataSource = null;
                    scrollRect.totalCount = 0;
                    scrollRect.ClearCells();
                }
                dataSource.Dispose();
            });
        }

        private class DataSource<T> : LoopScrollDataSource
            where T : Component {

            public Action<T, int> provideData;
            public DataSource(Action<T, int> provideData) {
                this.provideData = provideData ?? throw new ArgumentNullException(nameof(provideData));
            }
            public void Dispose() {
                provideData = null;
            }
            public void ProvideData(Transform transform, int index) {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Snap(this LoopScrollRectBase scrollRect, 
            float width, int defaultViewCount = int.MaxValue, float duration = 0.2f) {
            
            scrollRect.StopMovement();
            if (scrollRect.content.childCount <= defaultViewCount) return;

            var firstIndex = scrollRect.GetFirstItem(out var offset);
            scrollRect.ScrollToCellWithinTime(firstIndex + (offset >= -width * 0.5f ? 1 : 0), duration);
        }

    }

}