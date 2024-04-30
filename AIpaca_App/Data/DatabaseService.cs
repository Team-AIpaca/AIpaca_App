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
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "records.db");
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<EvRecord>().Wait(); // 비동기 테이블 생성을 동기적으로 기다립니다
        }

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
    }
}
