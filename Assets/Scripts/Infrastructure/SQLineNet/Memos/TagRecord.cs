using System;
using SQLite;
using Project.Domain.Memos.Model;

namespace Project.Infrastructure.SQLiteNet.Memos {

    /// <summary>
    /// <see cref="Tag"/>の永続化用データオブジェクト．
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
