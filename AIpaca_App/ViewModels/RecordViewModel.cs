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
                    Records.Add(record);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading records: {ex.Message}");
                await Toast.Make($"업적 목록을 로드하는 데 실패했습니다: {ex.Message}", ToastDuration.Long).Show();
            }
        }

        private async Task AddRecord(EvRecord record)
        {
            var result = await _dbService.AddRecordAsync(record);
            if (result == 1)
            {
                Records.Add(record);
            }
            else
            {
                await Toast.Make("레코드 추가에 실패했습니다.", ToastDuration.Short).Show();
            }
        }

    }
}
