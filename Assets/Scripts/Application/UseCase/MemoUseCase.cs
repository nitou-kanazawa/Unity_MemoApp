using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Domain.Memos.Model;
using Project.Domain.Memos.Repository;

namespace Project.Application.Memos.UseCase {

    /// <summary>
    /// メモ操作に関するUseCase．
    /// </summary>
    public sealed class MemoUseCase {

        private readonly IMemoRepository _repository;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public MemoUseCase(IMemoRepository repository) {
            _repository = repository;
        }

        /// <summary>
        /// メモを作成する．
        /// </summary>
        public async UniTask<Memo> CreateMemoAsync(string title, string content) {

            var memo = Memo.CreateNew(title, new Content(content));
            await _repository.SaveAsync(memo);
            return memo;
        }

        /// <summary>
        /// メモ一覧を取得する．
        /// </summary>
        public async UniTask<IEnumerable<Memo>> GetAllMemosAsync() {
            return await _repository.GetAllAsync();
        }

        /// <summary>
        /// メモを更新する．
        /// </summary>
        public async UniTask UpdateMemoAsync(Guid id, string title, string content) {
            var memo = await _repository.FindByIdAsync(id);
            if (memo == null) {
                throw new ArgumentException("Memo not found");
            }

            memo.UpdateTitle(title);
            memo.UpdateContent(new Content(content));
            await _repository.SaveAsync(memo);
        }

        /// <summary>
        /// メモを削除する．
        /// </summary>
        public async UniTask DeleteMemoAsync(Guid id) {
            await _repository.DeleteAsync(id);
        }
    }

}