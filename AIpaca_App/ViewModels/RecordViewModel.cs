using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using AIpaca_App.Data;
using AIpaca_App.Models;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MvvmHelpers;
using MvvmHelpers.Commands;

namespace AIpaca_App.ViewModels
{
    public class RecordViewModel : BaseViewModel
    {
        private DatabaseService _dbService;
        public ObservableCollection<EvRecord> Records { get; private set; }
        public ObservableCollection<EvRecord> GraphRecords { get; private set; }
        public ICommand LoadRecordsCommand { get; }
        public ICommand AddRecordCommand { get; }
        public ScoreGraphDrawable GraphDrawable { get; private set; }

        public RecordViewModel()
        {
            _dbService = new DatabaseService();
            Records = new ObservableCollection<EvRecord>();
            GraphRecords = new ObservableCollection<EvRecord>();
            LoadRecordsCommand = new AsyncCommand(LoadRecords);
            AddRecordCommand = new AsyncCommand<EvRecord>(AddRecord);
            GraphDrawable = new ScoreGraphDrawable();
        }

        private async Task LoadRecords()
        {
            try
            {
                Records.Clear();
                var records = await _dbService.GetAllRecordsAsync();
                foreach (var record in records)
                {
                    Records.Insert(0, record); // 역순으로 삽입
                }
                UpdateGraphRecords();
            }
            catch (Exception ex)
            {
                await Toast.Make($"업적 목록을 로드하는 데 실패했습니다: {ex.Message}", ToastDuration.Long).Show();
            }
        }

        private async Task AddRecord(EvRecord record)
        {
            var result = await _dbService.AddRecordAsync(record);
            if (result == 1)
            {
                Records.Insert(0, record); // 상단에 insert
                UpdateGraphRecords();    // db에 세로운 데이터 insert 하는경우 그래프에도 적용되도록 함
            }
            else
            {
                await Toast.Make("레코드 추가에 실패했습니다.", ToastDuration.Short).Show();
            }
        }

        private void UpdateGraphRecords()
        {
            GraphRecords.Clear();
            // 그래프에는 30개의 항목만 들어감
            foreach (var record in Records.OrderByDescending(r => r.RequestTime).Take(30))
            {
                GraphRecords.Add(record);
            }
            UpdateGraph(); // 그래프 업데이트
        }

        private void UpdateGraph()
        {
            GraphDrawable.Records = new List<EvRecord>(GraphRecords);
        }
    }
}
