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
        // ViewModel�� LoadLogs �޼��带 ���� ȣ���Ͽ� ������ �ε�
        await _viewModel.LoadLogs();
    }
}
