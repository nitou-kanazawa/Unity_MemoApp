using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Project.Domain.Memos.Model;
using Project.Domain.Memos.Repository;

namespace Project.Application.UseCase {

    /// <summary>
    /// タグ操作に関するUseCase．
    /// </summary>
    /// <remarks>
    /// - タグの追加
    /// - 既存タグの適用
    /// - タグの削除
    /// - タグの検索
    /// </remarks>
    public sealed class TagUseCase {

        //private readonly IMemoRepository _memoRepository;
        private readonly ITagRepository _tagRepository;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public TagUseCase(ITagRepository tagRepository) {
            _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
        }
        //public TagUseCase(IMemoRepository memoRepository, ITagRepository tagRepository) {
        //    _memoRepository = memoRepository ?? throw new ArgumentNullException(nameof(memoRepository));
        //    _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
        //}

        /// <summary>
        /// 
        /// </summary>
        public async UniTask<bool> CanAddTageAsync(string name) {

            if (string.IsNullOrWhiteSpace(name))
                return false;

            // 既に存在しているかの確認
            var existingTag = await _tagRepository.FindByNameAsync(name);
            return existingTag == null;
        }

        /// <summary>
        /// 新規タグを作成する．
        /// </summary>
        public async UniTask CreateTagAsync(string name) {
            var tag = await _tagRepository.FindByNameAsync(name);
            if (tag == null)
                throw new InvalidOperationException("A tag with the same name already exists.");

            // 追加処理
            var newTag = new Tag(name);
            await _tagRepository.AddAsync(newTag);
        }

        /// <summary>
        /// 既存タグを削除する．
        /// </summary>
        public async UniTask DeleteTagAsync(TagId id) {
            var tag = await _tagRepository.FindByIdAsync(id);
            if (tag == null)
                throw new InvalidOperationException("Tag not found.");

            await _tagRepository.RemoveAsync(id);
        }
    }
}
