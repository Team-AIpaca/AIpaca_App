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
        private DatabaseService databaseService;
        public ObservableCollection<EvRecord> Records { get; private set; }
        public ObservableCollection<EvRecord> GraphRecords { get; private set; }

        private const int PageSize = 5; // 페이지당 로드할 데이터 개수
        private int _currentPage = 0;
        private bool _isLoading = false;

        public AsyncCommand LoadRecordsCommand { get; }
        public AsyncCommand<EvRecord> AddRecordCommand { get; }
        public AsyncCommand LoadNextPageCommand { get; }

        public ScoreGraphDrawable GraphDrawable { get; private set; }
        public double GraphWidth => GraphRecords.Count * 50; // 너비를 조정할 속성

        public RecordViewModel()
        {
            databaseService = new DatabaseService();
            Records = new ObservableCollection<EvRecord>();
            GraphRecords = new ObservableCollection<EvRecord>();
            LoadRecordsCommand = new AsyncCommand(() => LoadRecords(0)); // 첫 페이지 로드
            AddRecordCommand = new AsyncCommand<EvRecord>(AddRecord);
            LoadNextPageCommand = new AsyncCommand(LoadNextPage);
            GraphDrawable = new ScoreGraphDrawable();
        }

        public async Task LoadRecords(int pageNumber)
        {
            if (_isLoading) return;

            _isLoading = true;

            var skip = pageNumber * PageSize;
            var recordList = await databaseService.GetRecordsAsync(skip, PageSize);

            if (pageNumber == 0) Records.Clear(); // 첫 페이지 로드 시 이전 데이터 지우기

            foreach (var record in recordList)
            {
                Records.Add(record);
            }

            _currentPage = pageNumber;
            _isLoading = false;

            UpdateGraphRecords();
        }

        public async Task LoadNextPage()
        {
            await LoadRecords(_currentPage + 1);
        }

        private async Task AddRecord(EvRecord record)
        {
            var result = await databaseService.AddRecordAsync(record);
            if (result == 1)
            {
                Records.Insert(0, record); // 상단에 insert
                UpdateGraphRecords(); // db에 세로운 데이터 insert 하는경우 그래프에도 적용되도록 함
            }
            else
            {
                await Toast.Make("레코드 추가에 실패했습니다.", ToastDuration.Short).Show();
            }
        }

        private void UpdateGraphRecords()
        {
            GraphRecords.Clear();
            // 그래프에는 30개의 항목만 들어감, 반대로 정렬하여 최신 데이터가 오른쪽에 위치하도록 함
            var latestRecords = Records.OrderBy(r => r.RequestTime).TakeLast(30);
            foreach (var record in latestRecords)
            {
                GraphRecords.Add(record);
            }
            UpdateGraph(); // 그래프 업데이트
        }

        private void UpdateGraph()
        {
            GraphDrawable.Records = new List<EvRecord>(GraphRecords);
            OnPropertyChanged(nameof(GraphWidth)); // GraphWidth 업데이트 알림
        }

        
    }
}
