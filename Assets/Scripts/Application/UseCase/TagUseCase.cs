using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Project.Domain.Memos.Model;
using Project.Domain.Memos.Repository;

namespace Project.Application.UseCase {

    /// <summary>
    /// タグ操作に関するUseCase．
    /// </summary>
    public sealed class TagUseCase {

        private readonly IMemoRepository _memoRepository;
        private readonly ITagRepository _tagRepository;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public TagUseCase(IMemoRepository memoRepository, ITagRepository tagRepository) {
            _memoRepository = memoRepository ?? throw new ArgumentNullException(nameof(memoRepository));
            _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
        }

        /// <summary>
        /// タグを削除する．
        /// </summary>
        public async UniTask DeleteTagAsync(TagId tagId) {
            if (tagId == null) throw new ArgumentNullException(nameof(tagId));

            // 1. タグを削除
            await _tagRepository.DeleteAsync(tagId);

            // 2. タグを参照しているメモを取得
            var memos = await _memoRepository.GetAllAsync();
            foreach (var memo in memos) {
                // 3. メモから該当タグを削除
                if (memo.Tags.Contains(tagId)) {
                    memo.RemoveTag(tagId);
                    await _memoRepository.SaveAsync(memo);
                }
            }
        }
    }
}
