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
        // 첫 페이지 로드
        await _viewModel.LoadLogs(0);
    }

    private async void OnRemainingItemsThresholdReached(object sender, EventArgs e)
    {
        // 다음 페이지 로드
        await _viewModel.LoadNextPage();
    }

}
