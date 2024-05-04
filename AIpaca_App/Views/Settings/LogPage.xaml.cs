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
        // ù ������ �ε�
        await _viewModel.LoadLogs(0);
    }

    private async void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
    {
        var scrollView = (ScrollView)sender;
        // ��ũ���� ������ ���������� Ȯ��
        if (e.ScrollY >= scrollView.ContentSize.Height - scrollView.Height)
        {
            // ������ �ε� ������ Ȯ���Ͽ� �ߺ� �ε� ����
            if (_viewModel._isLoading)
            {
                return;
            }
            // ���� ������ �ε�
            await _viewModel.LoadNextPage();
        }
    }
}