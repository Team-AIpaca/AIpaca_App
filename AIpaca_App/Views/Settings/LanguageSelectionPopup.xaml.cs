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
    private AlertPopup updatePopup = new AlertPopup
    {
        MainText = AppResources.changelanguage + "?",
        btn1Text = AppResources.ok
    };

    public LanguageSelectionPopup()
    {
        InitializeComponent();
        _viewModel = new LanguageViewModel();
    }

    private async void OnKoreanSelected(object sender, EventArgs e)
    {
        // 확인 버튼 클릭시 뷰모델 api 저장 메서드 실행
        updatePopup.btn1Clicked += async (sender, e) =>
        {
            await _viewModel.LanguageSet("ko");
        };

        if (Application.Current?.MainPage != null)
        {
            await Application.Current.MainPage.ShowPopupAsync(updatePopup);
        }
        // 팝업을 닫습니다.
        await CloseAsync();
    }

    private async void OnEnglishSelected(object sender, EventArgs e)
    {
        // 확인 버튼 클릭시 뷰모델 api 저장 메서드 실행
        updatePopup.btn1Clicked += async (sender, e) =>
        {
            await _viewModel.LanguageSet("en");
        };

        if (Application.Current?.MainPage != null)
        {
            await Application.Current.MainPage.ShowPopupAsync(updatePopup);
        }
        // 팝업을 닫습니다.
        await CloseAsync();
    }

    private async void OnJapaneseSelected(object sender, EventArgs e)
    {
        // 확인 버튼 클릭시 뷰모델 api 저장 메서드 실행
        updatePopup.btn1Clicked += async (sender, e) =>
        {
            await _viewModel.LanguageSet("ja");
        };

        if (Application.Current?.MainPage != null)
        {
            await Application.Current.MainPage.ShowPopupAsync(updatePopup);
        }
        // 팝업을 닫습니다.
        await CloseAsync();
    }
}
