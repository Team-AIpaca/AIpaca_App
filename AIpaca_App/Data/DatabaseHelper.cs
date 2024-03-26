using AIpaca_App.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIpaca_App.Data
{
    public class DatabaseHelper
    {
        private SQLiteAsyncConnection _database;

        public DatabaseHelper(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<AchieveList>().Wait();
            _database.CreateTableAsync<UserAchieve>().Wait();
        }

        // AchieveList CRUD operations
        public Task<List<AchieveList>> GetAchieveListsAsync()
        {
            return _database.Table<AchieveList>().ToListAsync();
        }

        public Task<AchieveList> GetAchieveListAsync(string id)
        {
            return _database.Table<AchieveList>().Where(i => i.AchieveId == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveAchieveListAsync(AchieveList achieveList)
        {
            if (!string.IsNullOrEmpty(achieveList.AchieveId))
            {
                return _database.UpdateAsync(achieveList);
            }
            else
            {
                return _database.InsertAsync(achieveList);
            }
        }

        public Task<int> DeleteAchieveListAsync(AchieveList achieveList)
        {
            return _database.DeleteAsync(achieveList);
        }

        // UserAchieve CRUD operations
        public Task<List<UserAchieve>> GetUserAchievesAsync()
        {
            return _database.Table<UserAchieve>().ToListAsync();
        }

        public Task<UserAchieve> GetUserAchieveAsync(int id)
        {
            return _database.Table<UserAchieve>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveUserAchieveAsync(UserAchieve userAchieve)
        {
            if (userAchieve.Id != 0)
            {
                return _database.UpdateAsync(userAchieve);
            }
            else
            {
                return _database.InsertAsync(userAchieve);
            }
        }

        public Task<int> DeleteUserAchieveAsync(UserAchieve userAchieve)
        {
            return _database.DeleteAsync(userAchieve);
        }

        // Additional methods for complex queries, initialization of default values, etc. can be added here.
    }
}
