using AIpaca_App.Models;
using CommunityToolkit.Maui.Alerts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Reflection;

namespace AIpaca_App.ViewModels
{
    public class ChallengeViewModel
    {
        public ObservableCollection<AchieveList> Achievements { get; set; }

        public ChallengeViewModel()
        {
            Achievements = new ObservableCollection<AchieveList>();
            LoadAchievementsFromXml();
        }

        private void LoadAchievementsFromXml()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ChallengeViewModel)).Assembly;
            var stream = assembly.GetManifestResourceStream("AIpaca_App.Resources.data.AchieveList.xml");

            if (stream != null)
            {
                using (var reader = new StreamReader(stream))
                {
                    var document = XDocument.Load(reader);
                    var achievements = document.Descendants("Achieve")
                        .Select(xAchieve =>
                        {
                            var achievement = new AchieveList
                            {
                                AchieveId = xAchieve.Element("AchieveId")?.Value ?? string.Empty,
                                AchieveName = xAchieve.Element("AchieveName")?.Value ?? string.Empty,
                                IsAvailable = bool.Parse(xAchieve.Element("IsAvailable")?.Value ?? "false")
                            };

                            DateTime achieveDate;
                            if (DateTime.TryParse(xAchieve.Element("AchieveDate")?.Value, out achieveDate))
                            {
                                achievement.AchieveDate = achieveDate;
                            }

                            DateTime achieveFixDate;
                            if (DateTime.TryParse(xAchieve.Element("AchieveFixDate")?.Value, out achieveFixDate))
                            {
                                achievement.AchieveFixDate = achieveFixDate;
                            }

                            return achievement;
                        });

                    foreach (var achieve in achievements)
                    {
                        Achievements.Add(achieve);
                    }
                }
            }
            else
            {
                Toast.Make("업적 목록을 로드하는 데 실패했습니다.").Show();
            }
        }


    }
}
