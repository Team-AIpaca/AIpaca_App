using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIpaca_App.Models
{
    public class Log
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Message { get; set; } = string.Empty;

        [NotNull]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public string? Success { get; set; }
    }
}
