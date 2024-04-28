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
        // ViewModel�� LoadRecords �޼��带 ȣ���Ͽ� ������ �ε�
        _viewModel.LoadRecordsCommand.Execute(null);
    }
}
