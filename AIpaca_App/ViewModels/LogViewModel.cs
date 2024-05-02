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

        public LogViewModel()
        {
            databaseService = new DatabaseService();
            Logs = new ObservableCollection<Log>();
            LoadLogsCommand = new AsyncCommand(LoadLogs);
        }

        public async Task LoadLogs()
        {
            try
            {
                Logs.Clear();
                var LogList = await databaseService.GetAllLogsAsync();
                foreach (var log in LogList)
                {
                    Logs.Add(log);
                }
            }
            catch (Exception ex)
            {
                await Toast.Make($"로그 목록을 로드하는 데 실패했습니다: {ex.Message}", ToastDuration.Long).Show();
            }
        }
    }
}
