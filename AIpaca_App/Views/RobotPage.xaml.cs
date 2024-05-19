using AIpaca_App.Data;
using AIpaca_App.ViewModels;

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
}