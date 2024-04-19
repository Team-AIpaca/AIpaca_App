using AIpaca_App.Data;
using AIpaca_App.ViewModels;

namespace AIpaca_App.Views;

public partial class RecordPage : ContentPage
{
    public RecordPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is RecordViewModel vm)
        {
            if (vm.Records.Count == 0) // 데이터가 이미 로드되지 않았다면
            {
                vm.LoadRecordsCommand.Execute(null);
            }
        }
    }
}
