using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Domain.Shared;
using Project.Domain.Memos.Model;

// [REF]
//  Zenn: https://zenn.dev/kohii/articles/e4f325ed011db8

namespace Project.Domain.Memos.Repository {

    public interface IMemoRepository : IRepository {
        UniTask SaveAsync(Memo memo);
        UniTask<IEnumerable<Memo>> GetAllAsync();
        UniTask<Memo> FindByIdAsync(Guid id);
        UniTask DeleteAsync(Guid id);
    }
}
