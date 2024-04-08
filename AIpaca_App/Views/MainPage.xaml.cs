using AIpaca_App.ViewModels;

namespace AIpaca_App.Views
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            // MainViewModel 인스턴스를 생성하고 BindingContext로 설정합니다.
            _viewModel = new MainViewModel();
            BindingContext = _viewModel;
        }

        private void OnSwitchLanguageClicked(object sender, EventArgs e)
        {
            // 현재 선택된 언어를 기억
            var temp = LeftLanguagePicker.SelectedIndex;
            // 선택된 언어를 서로 바꾸기
            LeftLanguagePicker.SelectedIndex = RightLanguagePicker.SelectedIndex;
            RightLanguagePicker.SelectedIndex = temp;
        }

        public void OnEvaluateButtonClicked(object sender, EventArgs e)
        {
            // Command 실행
            if (_viewModel.EvaluateTranslationCommand.CanExecute(null))
            {
                _viewModel.EvaluateTranslationCommand.Execute(null);
            }
        }

        private void LeftLanguagePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ViewModel의 SetOriginalLang 메서드 호출
            if (BindingContext is MainViewModel viewModel)
            {
                viewModel.SetOriginalLang(LeftLanguagePicker.SelectedIndex);
            }
        }

        private void RightLanguagePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ViewModel의 SetOriginalLang 메서드 호출
            if (BindingContext is MainViewModel viewModel)
            {
                viewModel.SetTranslatedLang(RightLanguagePicker.SelectedIndex);
            }
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height); // must be called

            if (width > height)
            {
                // Landscape layout
                TextFieldsGrid.RowDefinitions.Clear();
                TextFieldsGrid.ColumnDefinitions.Clear();

                TextFieldsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                TextFieldsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

                Grid.SetRow(InputEditor.Parent as Frame, 0);
                Grid.SetColumn(InputEditor.Parent as Frame, 0);

                Grid.SetRow(TranslatedEditor.Parent as Frame, 0);
                Grid.SetColumn(TranslatedEditor.Parent as Frame, 1);
            }
            else
            {
                // Portrait layout
                TextFieldsGrid.RowDefinitions.Clear();
                TextFieldsGrid.ColumnDefinitions.Clear();

                TextFieldsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                TextFieldsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                Grid.SetRow(InputEditor.Parent as Frame, 0);
                Grid.SetColumn(InputEditor.Parent as Frame, 0);

                Grid.SetRow(TranslatedEditor.Parent as Frame, 1);
                Grid.SetColumn(TranslatedEditor.Parent as Frame, 0);
            }
        }

    }
        
}
