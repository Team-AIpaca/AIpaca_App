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
        private DatabaseService _databaseService;
        public ObservableCollection<Log> Logs { get; private set; }

        private const int PageSize = 15; // 페이지당 로드할 데이터 개수
        private int _currentPage = 0;
        private bool _isLoading = false;
        private bool _isLastPage = false; // 마지막 페이지 여부 플래그

        public LogViewModel()
        {
            _databaseService = new DatabaseService();
            Logs = new ObservableCollection<Log>();
        }

        public async Task LoadLogs(int pageNumber)
        {
            if (_isLoading || _isLastPage) return;

            _isLoading = true;

            var skip = pageNumber * PageSize;
            var logList = await _databaseService.GetLogsAsync(skip, PageSize);

            if (pageNumber == 0) Logs.Clear(); // 첫 페이지 로드 시 이전 데이터 지우기

            foreach (var log in logList)
            {
                Logs.Add(log);
            }

            // 다음 페이지가 없는 경우 마지막 페이지로 간주
            _isLastPage = logList.Count < PageSize;
            _currentPage = pageNumber;
            _isLoading = false;
        }

        public async Task LoadNextPage()
        {
            await LoadLogs(_currentPage + 1);
        }
    }
}
