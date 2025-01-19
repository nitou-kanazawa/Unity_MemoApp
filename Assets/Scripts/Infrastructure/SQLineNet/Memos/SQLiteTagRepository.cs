using System;
using System.IO;
using System.Linq;
using SQLite;
using Project.Domain.Memos.Model;
using Project.Domain.Memos.Repository;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Project.Infrastructure.SQLiteNet.Memos {

    public sealed class SQLiteTagRepository : ITagRepository {

        private readonly ISQLiteConnection _connection;

        private readonly static string FileName = "memos.db";
        private readonly static string FilePath = Path.Combine(UnityEngine.Application.persistentDataPath, FileName);


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public SQLiteTagRepository() {
            _connection = new SQLiteConnection(FilePath);
            _connection.CreateTable<TagRecord>();
        }

        /// <summary>
        /// 終了処理．
        /// </summary>
        public void Dispose() {
            _connection.Dispose();
        }


        /// ----------------------------------------------------------------------------
        // Public Method (Interface)

        /// <summary>
        /// 指定したタグを追加する．
        /// </summary>
        UniTask ITagRepository.AddAsync(Tag tag) {

            if (tag == null)
                throw new ArgumentNullException(nameof(tag));
            // Name の重複を確認
            if (Find(tag.Name) != null) {
                throw new InvalidOperationException($"Tag with name '{tag.Name}' already exists.");
            }
            // Id の重複を確認
            if (Find(tag.Id) != null) {
                throw new InvalidOperationException($"Tag with ID '{tag.Id.Value}' already exists.");
            }

            // データベースへの適用
            var record = TagMapper.ToRecord(tag);
            _connection.Insert(record); // 新規データを挿入

            return UniTask.CompletedTask;
        }

        /// <summary>
        /// <see cref="TagId">タグID</see>で検索する．
        /// </summary>
        UniTask<Tag> ITagRepository.FindByIdAsync(TagId id) {
            var tag = Find(id);
            return UniTask.FromResult(tag);
        }

        /// <summary>
        /// タグ名で検索する．
        /// </summary>
        UniTask<Tag> ITagRepository.FindByNameAsync(string name) {
            var tag = Find(name);
            return UniTask.FromResult(tag);
        }

        /// <summary>
        /// 全てのタグを取得する．
        /// </summary>
        UniTask<IEnumerable<Tag>> ITagRepository.GetAllAsync() {
            var records = _connection.Table<TagRecord>().ToList();
            var tags = records.Select(record => TagMapper.ToDomain(record));
            return UniTask.FromResult(tags);
        }

        /// <summary>
        /// 指定したIDのメモを削除する．
        /// </summary>
        UniTask ITagRepository.RemoveAsync(TagId id) {
            _connection.Delete<TagRecord>(id.Value);

            return UniTask.CompletedTask;
        }



        /// ----------------------------------------------------------------------------
        // Private Method

        private Tag Find(TagId id) {
            var record = _connection
                .Find<TagRecord>(id.Value);

            return record != null ? TagMapper.ToDomain(record) : null;
        }

        private Tag Find(string name) {
            var record = _connection
                .Table<TagRecord>()
                .FirstOrDefault(r => r.Name == name);

            return record != null ? TagMapper.ToDomain(record) : null;
        }
    }
}
