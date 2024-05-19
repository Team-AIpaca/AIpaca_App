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
        // ���� ���õ� �� ���
        var temp = LeftLanguagePicker.SelectedIndex;
        // ���õ� �� ���� �ٲٱ�
        LeftLanguagePicker.SelectedIndex = RightLanguagePicker.SelectedIndex;
        RightLanguagePicker.SelectedIndex = temp;
    }

    private void LeftLanguagePicker_SelectedIndexChanged(object? sender, EventArgs e)
    {
        // ViewModel�� SetOriginalLang �޼��� ȣ��
        if (BindingContext is RobotViewModel viewModel)
        {
            viewModel.SetOriginalLang(LeftLanguagePicker.SelectedIndex);
        }
    }

    private void RightLanguagePicker_SelectedIndexChanged(object? sender, EventArgs e)
    {
        // ViewModel�� SetOriginalLang �޼��� ȣ��
        if (BindingContext is RobotViewModel viewModel)
        {
            viewModel.SetTranslatedLang(RightLanguagePicker.SelectedIndex);
        }
    }

    public void OnTranslationButtonClicked(object sender, EventArgs e)
    {
        // Command ����
        if (_viewModel.TranslationCommand.CanExecute(null))
        {
            _viewModel.TranslationCommand.Execute(null);
        }
    }
}