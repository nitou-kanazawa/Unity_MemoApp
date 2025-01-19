using System;
using SQLite;
using Project.Domain.Memos.Model;

namespace Project.Infrastructure.SQLiteNet.Memos {

    /// <summary>
    /// <see cref="Tag"/>�̉i�����p�f�[�^�I�u�W�F�N�g�D
    /// </summary>
    internal class TagRecord {
        [PrimaryKey]
        public Guid Id { get; set; }
        [Unique]
        public string Name { get; set; }
    }

    internal static class TagMapper {

        public static TagRecord ToRecord(this Tag tag) {
            return new TagRecord {
                Id = tag.Id.Value,
                Name = tag.Name,
            };
        }

        public static Tag ToDomain(this TagRecord record) {
            return new Tag(
                new TagId(record.Id),
                record.Name);
        }
    }
}
