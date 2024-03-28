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
        // 날짜를 문자열로 설정하는 메서드
        public void SetAchieveDateFromString(string dateString)
        {
            DateTime parsedDate;
            if (DateTime.TryParse(dateString, out parsedDate))
            {
                AchieveDate = parsedDate;
            }
            else
            {
                // 날짜 파싱에 실패한 경우 처리
            }
        }

        public DateTime? AchieveFixDate { get; set; }

        public bool IsAvailable { get; set; }
    }
}
