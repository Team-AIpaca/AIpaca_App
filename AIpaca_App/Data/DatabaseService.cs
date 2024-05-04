using AIpaca_App.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AIpaca_App.Data
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection db;

        public DatabaseService()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AppLog.db");
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<EvRecord>().Wait(); // EvRecord 테이블 생성
            db.CreateTableAsync<Log>().Wait(); // ErrorLog 테이블 생성
            db.CreateTableAsync<AchieveList>().Wait();
            db.CreateTableAsync<UserAchieve>().Wait();
            db.CreateTableAsync<TransRecord>().Wait();
        }
        #region Record 관련 메서드
        public Task<int> AddRecordAsync(EvRecord record)
        {
            return db.InsertAsync(record);
        }

        public Task<List<EvRecord>> GetAllRecordsAsync()
        {
            //OrderByDescending(x => x.RequestTime)를 사용하여 역순으로 정렬 가능
            //return db.Table<EvRecord>().OrderByDescending(x => x.RequestTime).ToListAsync(); // 비동기적으로 모든 레코드를 가져옵니다
            return db.Table<EvRecord>().ToListAsync(); // 비동기적으로 모든 레코드를 가져옵니다
        }

        public async Task<List<EvRecord>> GetRecordsAsync(int skip, int take)
        {
            return await db.Table<EvRecord>()
                           .OrderByDescending(x => x.RequestTime)
                           .Skip(skip)
                           .Take(take)
                           .ToListAsync();
        }
        #endregion

        #region TransRecord 관련 메서드
        public Task<int> AddTransAsync(TransRecord trecord)
        {
            return db.InsertAsync(trecord);
        }

        public Task<List<TransRecord>> GetAllTransAsync()
        {
            return db.Table<TransRecord>().ToListAsync(); // 비동기적으로 모든 레코드를 가져옵니다
        }
        #endregion

        #region Log 관련 메서드
        public Task<int> AddLogAsync(Log log)
        {
            return db.InsertAsync(log);
        }

        public Task<List<Log>> GetAllLogsAsync()
        {
            return db.Table<Log>().OrderByDescending(x => x.Timestamp).ToListAsync(); // 역순으로 정렬하여 모든 오류 로그 가져오기
        }

        public async Task<List<Log>> GetLogsAsync(int skip, int take)
        {
            return await db.Table<Log>()
                           .OrderByDescending(x => x.Timestamp)
                           .Skip(skip)
                           .Take(take)
                           .ToListAsync();
        }

        #endregion
    }
}
