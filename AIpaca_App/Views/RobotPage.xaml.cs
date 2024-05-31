using AIpaca_App.Data;
using AIpaca_App.Resources.Localization;
using AIpaca_App.ViewModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;

namespace AIpaca_App.Views;

public partial class RobotPage : ContentPage
{
    private RobotViewModel _viewModel;
    public RobotPage()
	{
		InitializeComponent();
        _viewModel = new RobotViewModel();
        BindingContext = _viewModel;
        _viewModel.SetOriginalLang(LeftLanguagePicker.SelectedIndex);
        _viewModel.SetTranslatedLang(RightLanguagePicker.SelectedIndex);

        LeftLanguagePicker.SelectedIndexChanged += LeftLanguagePicker_SelectedIndexChanged;
        RightLanguagePicker.SelectedIndexChanged += RightLanguagePicker_SelectedIndexChanged;
    }

    private void OnSwitchLanguageClicked(object sender, EventArgs e)
    {
        // 현재 선택된 언어를 기억
        var temp = LeftLanguagePicker.SelectedIndex;
        // 선택된 언어를 서로 바꾸기
        LeftLanguagePicker.SelectedIndex = RightLanguagePicker.SelectedIndex;
        RightLanguagePicker.SelectedIndex = temp;
    }

    private void LeftLanguagePicker_SelectedIndexChanged(object? sender, EventArgs e)
    {
        // ViewModel의 SetOriginalLang 메서드 호출
        if (BindingContext is RobotViewModel viewModel)
        {
            viewModel.SetOriginalLang(LeftLanguagePicker.SelectedIndex);
        }
    }

    private void RightLanguagePicker_SelectedIndexChanged(object? sender, EventArgs e)
    {
        // ViewModel의 SetOriginalLang 메서드 호출
        if (BindingContext is RobotViewModel viewModel)
        {
            viewModel.SetTranslatedLang(RightLanguagePicker.SelectedIndex);
        }
    }

    public void OnTranslationButtonClicked(object sender, EventArgs e)
    {
        // Command 실행
        if (_viewModel.TranslationCommand.CanExecute(null))
        {
            _viewModel.TranslationCommand.Execute(null);
        }
    }

    private async void OnLongPressPopupButtonClicked(object sender, EventArgs e)
    {
        var stackLayout = sender as VerticalStackLayout;
        if (stackLayout != null)
        {
            var labels = stackLayout.Children.OfType<Label>().ToList();
            var labelText = string.Join("\n", labels.Select(l => l.Text).ToArray());

            var robotpopup = new RobotPopup(labelText);
            await this.ShowPopupAsync(robotpopup);
        }
    }
}