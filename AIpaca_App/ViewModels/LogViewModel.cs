using AIpaca_App.Data;
using AIpaca_App.Models;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AIpaca_App.ViewModels
{
    public class LogViewModel : BaseViewModel
    {
        private DatabaseService databaseService;
        public ObservableCollection<Log> Logs { get; }
        public AsyncCommand LoadLogsCommand { get; }

        private const int PageSize = 10; // 페이지당 로드할 데이터 개수
        private int _currentPage = 0;
        private bool _isLoading = false;

        public LogViewModel()
        {
            databaseService = new DatabaseService();
            Logs = new ObservableCollection<Log>();
            LoadLogsCommand = new AsyncCommand(() => LoadLogs(0)); // 첫 페이지 로드
        }

        public async Task LoadLogs(int pageNumber)
        {
            if (_isLoading) return;

            _isLoading = true;

            var skip = pageNumber * PageSize;
            var logList = await databaseService.GetLogsAsync(skip, PageSize);

            if (pageNumber == 0) Logs.Clear(); // 첫 페이지 로드 시 이전 데이터 지우기

            foreach (var log in logList)
            {
                Logs.Add(log);
            }

            _currentPage = pageNumber;
            _isLoading = false;
        }

        public Task LoadNextPage()
        {
            return LoadLogs(_currentPage + 1);
        }
    }
}
