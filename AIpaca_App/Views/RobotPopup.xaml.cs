using AIpaca_App.Resources.Localization;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;

namespace AIpaca_App.Views;

public partial class RobotPopup : Popup
{
    private string _labelText;

    public RobotPopup(string labelText)
    {
        InitializeComponent();
        _labelText = labelText;
    }

    private async void OnCopyButtonClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(_labelText))
        {
            await Clipboard.Default.SetTextAsync(_labelText);
            await Toast.Make(AppResources.copy_successful, ToastDuration.Long).Show();
        }
        else
        {
            await Toast.Make(AppResources.copy_failed, ToastDuration.Long).Show();
        }
        await CloseAsync();
    }
}