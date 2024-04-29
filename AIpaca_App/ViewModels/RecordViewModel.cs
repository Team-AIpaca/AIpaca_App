using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using AIpaca_App.Data;
using AIpaca_App.Models;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microcharts;
using MvvmHelpers;
using MvvmHelpers.Commands;
using SkiaSharp;


namespace AIpaca_App.ViewModels
{
    public class RecordViewModel : BaseViewModel
    {
        public Chart? ScoreChart { get; set; }
        private DatabaseService _dbService;
        public ObservableCollection<EvRecord> Records { get; private set; }
        public ICommand LoadRecordsCommand { get; }
        public ICommand AddRecordCommand { get; }

        public RecordViewModel()
        {
            _dbService = new DatabaseService();
            Records = new ObservableCollection<EvRecord>();
            LoadRecordsCommand = new AsyncCommand(LoadRecords);
            AddRecordCommand = new AsyncCommand<EvRecord>(AddRecord);
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
                //await LoadChartData();    // db에 세로운 데이터 insert 하는경우 그래프에도 적용되도록 함
            }
            else
            {
                await Toast.Make("레코드 추가에 실패했습니다.", ToastDuration.Short).Show();
            }
        }

    }
}
