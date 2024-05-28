using CommunityToolkit.Maui.Views;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System.Globalization;
using AIpaca_App.Resources.Localization;
using AIpaca_App.ViewModels;

namespace AIpaca_App.Views.Settings;

public partial class LanguageSelectionPopup : Popup
{
    private LanguageViewModel _viewModel;

    public LanguageSelectionPopup()
    {
        InitializeComponent();
        _viewModel = new LanguageViewModel();
    }

    private void OnKoreanSelected(object sender, EventArgs e)
    {
        _viewModel.change("ko");
        Close();
    }

    private void OnEnglishSelected(object sender, EventArgs e)
    {
        _viewModel.change("en");
        Close();
    }

    private void OnJapaneseSelected(object sender, EventArgs e)
    {
        _viewModel.change("ja");
        Close();
    }
}