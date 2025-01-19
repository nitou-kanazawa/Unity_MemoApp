using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Domain.Memos.Model;
using Project.Domain.Memos.Repository;

namespace Project.Infrastructure.BuiltIn.Memos {

    /// <summary>
    /// <see cref="ITagRepository"/>�Ƀ��O�o�͂�ǉ����郉�b�p�[�i�v���L�V�j�D
    /// </summary>
    public sealed class LoggingTagRepository : ITagRepository {
        
        private readonly ITagRepository _innerRepository;


        /// ----------------------------------------------------------------------------
        // Public Method

        public LoggingTagRepository(ITagRepository innerRepository) {
            _innerRepository = innerRepository;
        }

        public void Dispose() {
            _innerRepository.Dispose();
        }


        /// ----------------------------------------------------------------------------
        // Public Method (Interface)

        public async UniTask AddAsync(Tag tag) {
            Console.WriteLine($"[INFO] Adding tag: {tag.Name}");
            await _innerRepository.AddAsync(tag);
        }

        public async UniTask<IEnumerable<Tag>> GetAllAsync() {
            Console.WriteLine("[INFO] Fetching all tags");
            return await _innerRepository.GetAllAsync();
        }

        public async UniTask<Tag> FindByIdAsync(TagId id) {
            Console.WriteLine($"[INFO] Finding tag by ID: {id}");
            return await _innerRepository.FindByIdAsync(id);
        }

        public async UniTask<Tag> FindByNameAsync(string name) {
            Console.WriteLine($"[INFO] Finding tag by Name: {name}");
            return await _innerRepository.FindByNameAsync(name);
        }

        public async UniTask RemoveAsync(TagId id) {
            Console.WriteLine($"[INFO] Removing tag with ID: {id}");
            await _innerRepository.RemoveAsync(id);
        }
    }

}
