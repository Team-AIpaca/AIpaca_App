using AIpaca_App.Resources.Localization;
using AIpaca_App.ViewModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

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

    private async void OnCopyButtonClicked(object sender, EventArgs e)
    {
        var stackLayout = sender as StackLayout;
        if (stackLayout != null)
        {
            var labels = stackLayout.Children.OfType<Label>().ToList();
            string labelText = string.Join("\n", labels.Select(l => l.Text).ToArray());

            if (!string.IsNullOrEmpty(labelText))
            {
                await Clipboard.Default.SetTextAsync(labelText);
                await Toast.Make(AppResources.copy_successful, ToastDuration.Long).Show();
            }
            else
            {
                await Toast.Make(AppResources.copy_failed, ToastDuration.Long).Show();
            }
        }
    }

}