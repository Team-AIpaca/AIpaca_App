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
        public int PageSize = 15;// 페이지당 로드할 데이터 개수
        private int _currentPage = 0;
        public bool _isLoading = false;
        private bool _isLastPage = false;

        public LogViewModel()
        {
            _databaseService = new DatabaseService();
            Logs = new ObservableCollection<Log>();
        }

        #region 로그 출력
        public async Task LoadLogs(int pageNumber)
        {
            if (_isLoading || _isLastPage) return;

            _isLoading = true;

            var skip = pageNumber * PageSize;
            var logList = await _databaseService.GetLogsAsync(skip, PageSize);

            if (pageNumber == 0) Logs.Clear(); // 첫 페이지 로드 시 이전 데이터 지우기

            if (logList.Count > 0)
            {
                foreach (var log in logList)
                {
                    Logs.Add(log);
                }
            }

            _isLastPage = logList.Count < PageSize;
            _currentPage = pageNumber;
            _isLoading = false;
        }

        public async Task LoadNextPage()
        {
            if (_isLastPage || _isLoading) return;

            await LoadLogs(_currentPage + 1);
        }
        #endregion
    }
}
