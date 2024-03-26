using SQLite;

namespace AIpaca_App.Models
{
    public class AchieveList
    { 
        public AchieveList()
        {
            AchieveId = string.Empty;
            AchieveName = string.Empty;

            // 기타 필요한 기본값 설정
        }

        public string AchieveId { get; set; }

        public string AchieveName { get; set; }

        public DateTime AchieveDate { get; set; }

        public DateTime? AchieveFixDate { get; set; }

        public bool IsAvailable { get; set; }
    }
}
