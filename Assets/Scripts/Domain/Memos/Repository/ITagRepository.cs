using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Domain.Shared;
using Project.Domain.Memos.Model;

namespace Project.Domain.Memos.Repository {

    public interface ITagRepository : IRepository {
        UniTask SaveAsync(Tag tag);
        UniTask<IEnumerable<Tag>> GetAllAsync();
        UniTask<Tag> FindByIdAsync(TagId id);
        UniTask<Tag> FindByNameAsync(string name);
        UniTask DeleteAsync(TagId id);
    }
}
