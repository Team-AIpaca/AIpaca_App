using AIpaca_App.Data;
using AIpaca_App.ViewModels;

namespace AIpaca_App.Views;

public partial class LogPage : ContentPage
{
    private LogViewModel _viewModel;

    public LogPage()
    {
        InitializeComponent();
        _viewModel = new LogViewModel();
        this.BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // ViewModel의 LoadLogs 메서드를 직접 호출하여 데이터 로드
        await _viewModel.LoadLogs();
    }
}
