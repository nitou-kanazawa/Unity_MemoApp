using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Domain.Memos.Model;
using Project.Domain.Memos.Repository;

namespace Project.Application.Memos.UseCase {

    /// <summary>
    /// ��������Ɋւ���UseCase�D
    /// </summary>
    public sealed class MemoUseCase {

        private readonly IMemoRepository _repository;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^�D
        /// </summary>
        public MemoUseCase(IMemoRepository repository) {
            _repository = repository;
        }

        /// <summary>
        /// �������쐬����D
        /// </summary>
        public async UniTask<Memo> CreateMemoAsync(string title, string content) {

            var memo = Memo.CreateNew(title, new Content(content));
            await _repository.SaveAsync(memo);
            return memo;
        }

        /// <summary>
        /// �����ꗗ���擾����D
        /// </summary>
        public async UniTask<IEnumerable<Memo>> GetAllMemosAsync() {
            return await _repository.GetAllAsync();
        }

        /// <summary>
        /// �������X�V����D
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
        /// �������폜����D
        /// </summary>
        public async UniTask DeleteMemoAsync(Guid id) {
            await _repository.DeleteAsync(id);
        }
    }

}