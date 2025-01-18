using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Domain.Shared;
using Project.Domain.Memos.Model;

namespace Project.Domain.Memos.Repository {

    /// <summary>
    /// <see cref="Tag"/>�̉i������S�����|�W�g���D
    /// </summary>
    public interface ITagRepository : IRepository {
        
        // Create
        UniTask AddAsync(Tag tag);

        // Read
        UniTask<IEnumerable<Tag>> GetAllAsync();
        UniTask<Tag> FindByIdAsync(TagId id);
        UniTask<Tag> FindByNameAsync(string name);
        
        // Delete
        UniTask RemoveAsync(TagId id);
    }
}
