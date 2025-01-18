using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Project.Domain.Memos.Model;
using Project.Domain.Memos.Repository;

namespace Project.Application.UseCase {

    /// <summary>
    /// �^�O����Ɋւ���UseCase�D
    /// </summary>
    /// <remarks>
    /// - �^�O�̒ǉ�
    /// - �����^�O�̓K�p
    /// - �^�O�̍폜
    /// - �^�O�̌���
    /// </remarks>
    public sealed class TagUseCase {

        //private readonly IMemoRepository _memoRepository;
        private readonly ITagRepository _tagRepository;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^�D
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

            // ���ɑ��݂��Ă��邩�̊m�F
            var existingTag = await _tagRepository.FindByNameAsync(name);
            return existingTag == null;
        }

        /// <summary>
        /// �V�K�^�O���쐬����D
        /// </summary>
        public async UniTask CreateTagAsync(string name) {
            var tag = await _tagRepository.FindByNameAsync(name);
            if (tag == null)
                throw new InvalidOperationException("A tag with the same name already exists.");

            // �ǉ�����
            var newTag = new Tag(name);
            await _tagRepository.AddAsync(newTag);
        }

        /// <summary>
        /// �����^�O���폜����D
        /// </summary>
        public async UniTask DeleteTagAsync(TagId id) {
            var tag = await _tagRepository.FindByIdAsync(id);
            if (tag == null)
                throw new InvalidOperationException("Tag not found.");

            await _tagRepository.RemoveAsync(id);
        }
    }
}
