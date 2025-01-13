using System;
using SQLite;

namespace Project.Infrastructure.SQLiteNet.Memos {

    internal class TagRecord {
        [PrimaryKey]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
