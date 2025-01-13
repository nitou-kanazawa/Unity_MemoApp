using System;
using SQLite;
using Project.Domain.Memos.Model;

namespace Project.Infrastructure.SQLiteNet {

    /// <summary>
    /// <see cref="Memo"/>の永続化用データオブジェクト．
    /// </summary>
    internal class MemoRecord {
        [PrimaryKey]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }


    internal static class MemoMapper {

        public static MemoRecord ToRecord(this Memo memo) {
            return new MemoRecord {
                Id = memo.Id,
                Title = memo.Title,
                Content = memo.Content.Text,
                //CreatedAt = memo.CreatedAt,
                //UpdatedAt = memo.UpdatedAt
            };
        }

        public static Memo ToDomain(this MemoRecord record) {
            return new Memo(
                record.Id,
                record.Title,
                new Content(record.Content)
            //record.CreatedAt,
            //record.UpdatedAt
            );
        }
    }
}
