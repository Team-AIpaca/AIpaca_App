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
            vm.LoadRecordsCommand.Execute(null);
        }
    }
}
