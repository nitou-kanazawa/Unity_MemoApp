using System;
using System.IO;
using System.Linq;
using SQLite;
using Project.Domain.Memos.Model;
using Project.Domain.Memos.Repository;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Project.Infrastructure.SQLiteNet.Memos {

    public sealed class SQLiteTagRepository : ITagRepository {

        private readonly ISQLiteConnection _connection;

        private readonly static string FileName = "momos.db";
        private readonly static string FilePath = Path.Combine(UnityEngine.Application.persistentDataPath, FileName);


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^�D
        /// </summary>
        public SQLiteTagRepository() {
            _connection = new SQLiteConnection(FilePath);
            _connection.CreateTable<TagRecord>();
        }

        /// <summary>
        /// �I�������D
        /// </summary>
        public void Dispose() {
            _connection.Dispose();
        }


        /// ----------------------------------------------------------------------------
        // Public Method (Interface)

        /// <summary>
        /// �w�肵���^�O��ۑ�����D
        /// </summary>
        UniTask ITagRepository.AddAsync(Tag tag) {
            var record = new TagRecord {
                Id = tag.Id.Value,
                Name = tag.Name
            };

            // Apply to database
            if (_connection.Find<TagRecord>(record.Id) != null) {
                _connection.Update(record);
            } else {
                _connection.Insert(record);
            }

            return UniTask.CompletedTask;
        }

        UniTask<Tag> ITagRepository.FindByIdAsync(TagId id) {
            var record = _connection.Find<TagRecord>(id.Value);
            var tag = record != null ? new Tag(new TagId(record.Id), record.Name) : null;

            return UniTask.FromResult(tag);
        }

        UniTask<Tag> ITagRepository.FindByNameAsync(string name) {
            var record = _connection.Table<TagRecord>().FirstOrDefault(r => r.Name == name);
            var tag = record != null ? new Tag(new TagId(record.Id), record.Name) : null;

            return UniTask.FromResult(tag);
        }

        /// <summary>
        /// �S�Ẵ^�O���擾����D
        /// </summary>
        UniTask<IEnumerable<Tag>> ITagRepository.GetAllAsync() {
            var records = _connection.Table<TagRecord>().ToList();
            var tags = records.Select(r => new Tag(new TagId(r.Id), r.Name));
            return UniTask.FromResult(tags);
        }

        /// <summary>
        /// �w�肵��ID�̃������폜����D
        /// </summary>
        UniTask ITagRepository.RemoveAsync(TagId id) {
            _connection.Delete<TagRecord>(id);

            return UniTask.CompletedTask;
        }
    }
}
