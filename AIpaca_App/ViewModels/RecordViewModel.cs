using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using AIpaca_App.Data;
using AIpaca_App.Models;
using AIpaca_App.Resources.Localization;
using AIpaca_App.Views;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;

namespace AIpaca_App.ViewModels
{
    public class RecordViewModel : BaseViewModel
    {
        private DatabaseService databaseService;

        public ObservableCollection<EvRecord> Records { get; private set; }
        public ObservableCollection<EvRecord> GraphRecords { get; private set; }

        public int PageSize = 7; // 페이지당 로드할 데이터 개수
        private int _currentPage = 0;
        public bool _isLoading = false;
        private bool _isLastPage = false;

        public AsyncCommand LoadRecordsCommand { get; }
        public AsyncCommand<EvRecord> AddRecordCommand { get; }
        public AsyncCommand LoadNextPageCommand { get; }
        public AsyncCommand RefreshCommand { get; }

        public ScoreGraphDrawable GraphDrawable { get; private set; }
        public double GraphWidth => GraphRecords.Count * 50; // 너비를 조정할 속성

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public RecordViewModel()
        {
            databaseService = new DatabaseService();
            RefreshCommand = new AsyncCommand(RefreshGraph);
            Records = new ObservableCollection<EvRecord>();
            GraphRecords = new ObservableCollection<EvRecord>();
            LoadRecordsCommand = new AsyncCommand(() => LoadRecords(0)); // 첫 페이지 로드
            AddRecordCommand = new AsyncCommand<EvRecord>(AddRecord);
            LoadNextPageCommand = new AsyncCommand(LoadNextPage); 
            GraphDrawable = new ScoreGraphDrawable();
        }

        #region 페이지 로드 및 추가

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

            _isLastPage = recordList.Count < PageSize;
            _currentPage = pageNumber;
            _isLoading = false;

            UpdateGraphRecords();
        }

        public async Task LoadNextPage()
        {
            if (_isLastPage || _isLoading) return;

            await LoadRecords(_currentPage + 1);
        }

        private async Task AddRecord(EvRecord record)
        {
            var result = await databaseService.AddRecordAsync(record);
            if (result == 1)
            {
                Records.Insert(0, record); // 상단에 insert
                UpdateGraphRecords();
            }
            else
            {
                await Toast.Make(AppResources.record_addition_failed, ToastDuration.Short).Show();
            }
        }
        #endregion

        #region 그래프 업데이트
        private void UpdateGraphRecords()
        {
            GraphRecords.Clear();
            // 그래프에는 20개의 항목만 들어감, 반대로 정렬하여 최신 데이터가 오른쪽에 위치하도록 함
            var latestRecords = Records.OrderBy(r => r.RequestTime).TakeLast(20);
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

        private async Task RefreshGraph()
        {
            IsRefreshing = true;

            // 전체 데이터를 다시 로드하여 그래프를 새로 고침
            await LoadRecords(0);
            UpdateGraphRecords();

            IsRefreshing = false;
        }
        #endregion
    }
}
