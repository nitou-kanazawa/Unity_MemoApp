using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Project.Domain.Memos.Model;
using Project.Domain.Memos.Repository;

namespace Project.Infrastructure.BuiltIn.Memos {

    public sealed class InMemoryTagRepository : ITagRepository {

        private readonly ConcurrentDictionary<TagId, Tag> _tags = new();

        public InMemoryTagRepository() {

        }
        
        public void Dispose() {
        }
        
        UniTask ITagRepository.AddAsync(Tag tag) {
            if (_tags.Values.Any(t => t.Name.Equals(tag.Name, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("A tag with the same name already exists.");

            if (!_tags.TryAdd(tag.Id, tag))
                throw new InvalidOperationException("Tag already exists.");

            return UniTask.CompletedTask;
        }

        UniTask<IEnumerable<Tag>> ITagRepository.GetAllAsync() {
            return UniTask.FromResult(_tags.Values.AsEnumerable());
        }

        UniTask<Tag> ITagRepository.FindByIdAsync(TagId id) {
            _tags.TryGetValue(id, out var tag);
            return UniTask.FromResult(tag);
        }

        UniTask<Tag> ITagRepository.FindByNameAsync(string name) {
            var tag = _tags.Values.FirstOrDefault(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            return UniTask.FromResult(tag);
        }

        UniTask ITagRepository.RemoveAsync(TagId id) {
            _tags.TryRemove(id, out _);
            return UniTask.CompletedTask;
        }
    }
}
