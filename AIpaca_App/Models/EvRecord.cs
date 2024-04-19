using SQLite;

namespace AIpaca_App.Models
{
    public class EvRecord
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string OriginalText { get; set; } = string.Empty;

        [NotNull]
        public string OriginalLang { get; set; } = string.Empty;

        [NotNull]
        public string TranslatedText { get; set; } = string.Empty;

        [NotNull]
        public string TranslatedLang { get; set; } = string.Empty;

        [NotNull]
        public string Message { get; set; } = string.Empty;

        [NotNull]
        public string RequestTime { get; set; } = string.Empty;

        [NotNull]
        public int Score { get; set; }

        [NotNull]
        public string RecommendedTrans { get; set; } = string.Empty;

        [NotNull]
        public string Rating { get; set; } = string.Empty;
    }
}
