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
        // ù ������ �ε�
        await _viewModel.LoadRecords(0);
    }

    private async void OnRemainingItemsThresholdReached(object sender, EventArgs e)
    {
        // ���� ������ �ε�
        await _viewModel.LoadNextPage();
    }
}
