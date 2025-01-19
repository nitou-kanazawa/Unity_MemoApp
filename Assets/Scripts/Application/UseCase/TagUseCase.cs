using System;
using System.Collections.Generic;
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

        private readonly ITagRepository _tagRepository;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public TagUseCase(ITagRepository tagRepository) {
            _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
        }

        /// <summary>
        /// タグを追加できるか確認する．
        /// </summary>
        public async UniTask<bool> CanAddTagAsync(string name) {

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
            var existingTag = await _tagRepository.FindByNameAsync(name);
            if (existingTag != null)
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

        /// <summary>
        /// 部分一致でタグを検索する．
        /// </summary>
        /// <param name="keyword">検索キーワード</param>
        /// <returns>一致するタグのリスト</returns>
        public async UniTask<IEnumerable<Tag>> SearchTagsAsync(string keyword) {

            if (string.IsNullOrWhiteSpace(keyword))
                return Enumerable.Empty<Tag>();

            var allTags = await _tagRepository.GetAllAsync();
            return allTags.Where(tag => tag.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }

    }
}
