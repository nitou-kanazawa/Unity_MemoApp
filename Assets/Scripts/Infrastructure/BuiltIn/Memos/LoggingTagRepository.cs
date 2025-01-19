using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Domain.Memos.Model;
using Project.Domain.Memos.Repository;
using Debug = UnityEngine.Debug;

namespace Project.Infrastructure.BuiltIn.Memos {

    /// <summary>
    /// <see cref="ITagRepository"/>にログ出力を追加するラッパー（プロキシ）．
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
            Debug.Log($"[INFO] Adding tag: {tag.Name}");
            await _innerRepository.AddAsync(tag);
        }

        public async UniTask<IEnumerable<Tag>> GetAllAsync() {
            Debug.Log("[INFO] Fetching all tags");
            return await _innerRepository.GetAllAsync();
        }

        public async UniTask<Tag> FindByIdAsync(TagId id) {
            Debug.Log($"[INFO] Finding tag by ID: {id}");
            return await _innerRepository.FindByIdAsync(id);
        }

        public async UniTask<Tag> FindByNameAsync(string name) {
            Debug.Log($"[INFO] Finding tag by Name: {name}");
            return await _innerRepository.FindByNameAsync(name);
        }

        public async UniTask RemoveAsync(TagId id) {
            Debug.Log($"[INFO] Removing tag with ID: {id}");
            await _innerRepository.RemoveAsync(id);
        }
    }

}
