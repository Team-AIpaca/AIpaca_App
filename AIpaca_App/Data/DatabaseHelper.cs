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
        readonly SQLiteAsyncConnection _database;

        public DatabaseHelper(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);

            // 아래의 CreateTableAsync 호출은 데이터베이스에 테이블이 없을 때만 새로 생성합니다.
            _database.CreateTableAsync<AchieveList>().Wait();
            _database.CreateTableAsync<UserAchieve>().Wait();
        }

        // 업적 정보 리스트를 데이터베이스에서 조회하는 메서드
        public Task<List<AchieveList>> GetAchievementsAsync()
        {
            return _database.Table<AchieveList>().ToListAsync();
        }

        // 사용자의 업적 달성 정보를 데이터베이스에서 조회하는 메서드
        public Task<List<UserAchieve>> GetUserAchievementsAsync()
        {
            return _database.Table<UserAchieve>().ToListAsync();
        }

        // 새로운 업적 정보를 데이터베이스에 삽입하는 메서드
        public Task<int> SaveAchievementAsync(AchieveList achievement)
        {
            return _database.InsertAsync(achievement);
        }

        // 사용자의 새로운 업적 달성 정보를 데이터베이스에 삽입하는 메서드
        public Task<int> SaveUserAchievementAsync(UserAchieve userAchieve)
        {
            return _database.InsertAsync(userAchieve);
        }

        // 업적 정보를 데이터베이스에서 업데이트하는 메서드
        public Task<int> UpdateAchievementAsync(AchieveList achievement)
        {
            return _database.UpdateAsync(achievement);
        }

        // 사용자의 업적 달성 정보를 데이터베이스에서 업데이트하는 메서드
        public Task<int> UpdateUserAchievementAsync(UserAchieve userAchieve)
        {
            return _database.UpdateAsync(userAchieve);
        }

        // 업적 정보를 데이터베이스에서 삭제하는 메서드
        public Task<int> DeleteAchievementAsync(AchieveList achievement)
        {
            return _database.DeleteAsync(achievement);
        }

        // 사용자의 업적 달성 정보를 데이터베이스에서 삭제하는 메서드
        public Task<int> DeleteUserAchievementAsync(UserAchieve userAchieve)
        {
            return _database.DeleteAsync(userAchieve);
        }
    }
}
