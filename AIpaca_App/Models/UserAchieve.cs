using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIpaca_App.Models
{
    public class UserAchieve
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int AchieveId { get; set; }

        public DateTime CompletedDate { get; set; }
    }
}
