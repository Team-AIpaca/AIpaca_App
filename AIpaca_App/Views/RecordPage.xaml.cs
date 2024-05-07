using AIpaca_App.Data;
using AIpaca_App.Resources.Localization;
using AIpaca_App.ViewModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace AIpaca_App.Views;

public partial class RecordPage : ContentPage
{
    private RecordViewModel _viewModel;

    public RecordPage()
    {
        InitializeComponent();
        _viewModel = new RecordViewModel();
        this.BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // 첫 페이지 로드
        await _viewModel.LoadRecords(0);

        // 그래프 스크롤 뷰를 가장 오른쪽으로 스크롤
        this.Dispatcher.Dispatch(async () =>
        {
            await graphicsScrollView.ScrollToAsync(graphicsScrollView.ContentSize.Width, 0, true);
        });
    }

    private async void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
    {
        var scrollView = (ScrollView)sender;
        // 스크롤이 끝까지 내려갔는지 확인
        if (e.ScrollY >= scrollView.ContentSize.Height - scrollView.Height)
        {
            // 데이터 로드 중인지 확인하여 중복 로딩 방지
            if (_viewModel._isLoading)
            {
                return;
            }
            // 다음 페이지 로드
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
