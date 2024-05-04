using AIpaca_App.Data;
using AIpaca_App.ViewModels;

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
}
