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
    protected override void OnAppearing()
    {
        base.OnAppearing();
        // ViewModel의 LoadRecords 메서드를 호출하여 데이터 로드
        _viewModel.LoadRecordsCommand.Execute(null);
    }
}
