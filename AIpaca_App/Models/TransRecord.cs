using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIpaca_App.Models
{
    public class TransRecord
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string OriginalText { get; set; } = string.Empty;

        [NotNull]
        public string OriginalLang { get; set; } = string.Empty;

        [NotNull]
        public string TranslatedLang { get; set; } = string.Empty;

        [NotNull]
        public string TranslatedText { get; set; } = string.Empty;  
    }
}
