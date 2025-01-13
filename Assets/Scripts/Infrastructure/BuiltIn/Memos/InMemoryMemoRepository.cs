using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Project.Domain.Memos.Model;
using Project.Domain.Memos.Repository;

namespace Project.Infrastructure.Default.Memos {

    public sealed class InMemoryMemoRepository : IMemoRepository {

        private readonly Dictionary<Guid, Memo> _store = new();


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public InMemoryMemoRepository() {

        }

        /// <summary>
        /// 終了処理．
        /// </summary>
        public void Dispose() {

        }


        /// ----------------------------------------------------------------------------
        // Public Method

        UniTask IMemoRepository.SaveAsync(Memo memo) {
            _store[memo.Id] = memo;
            return UniTask.CompletedTask;
        }

        UniTask<IEnumerable<Memo>> IMemoRepository.GetAllAsync() {
            return UniTask.FromResult(_store.Values.AsEnumerable());
        }

        UniTask<Memo> IMemoRepository.FindByIdAsync(Guid id) {
            _store.TryGetValue(id, out var memo);
            return UniTask.FromResult(memo);
        }

        UniTask IMemoRepository.DeleteAsync(Guid id) {
            _store.Remove(id);
            return UniTask.CompletedTask;
        }
    }
}
