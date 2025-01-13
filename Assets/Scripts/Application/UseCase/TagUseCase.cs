using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Project.Domain.Memos.Model;
using Project.Domain.Memos.Repository;

namespace Project.Application.UseCase {

    /// <summary>
    /// �^�O����Ɋւ���UseCase�D
    /// </summary>
    public sealed class TagUseCase {

        private readonly IMemoRepository _memoRepository;
        private readonly ITagRepository _tagRepository;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^�D
        /// </summary>
        public TagUseCase(IMemoRepository memoRepository, ITagRepository tagRepository) {
            _memoRepository = memoRepository ?? throw new ArgumentNullException(nameof(memoRepository));
            _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
        }

        /// <summary>
        /// �^�O���폜����D
        /// </summary>
        public async UniTask DeleteTagAsync(TagId tagId) {
            if (tagId == null) throw new ArgumentNullException(nameof(tagId));

            // 1. �^�O���폜
            await _tagRepository.DeleteAsync(tagId);

            // 2. �^�O���Q�Ƃ��Ă��郁�����擾
            var memos = await _memoRepository.GetAllAsync();
            foreach (var memo in memos) {
                // 3. ��������Y���^�O���폜
                if (memo.Tags.Contains(tagId)) {
                    memo.RemoveTag(tagId);
                    await _memoRepository.SaveAsync(memo);
                }
            }
        }
    }
}
