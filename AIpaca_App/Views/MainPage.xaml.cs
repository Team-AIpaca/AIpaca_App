namespace AIpaca_App.Views
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
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
