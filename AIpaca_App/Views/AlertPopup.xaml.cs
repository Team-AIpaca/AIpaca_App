using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;

namespace AIpaca_App.Views;

public partial class AlertPopup : Popup
{

    public event EventHandler? btn1Clicked;

    public AlertPopup()
	{
		InitializeComponent();
	}

    // "업데이트" 버튼이 클릭될 때 실행될 메소드
    private async void Onbtn1Clicked(object sender, EventArgs e)
    {
        // SplashPage에서 처리할 수 있도록 이벤트를 발생시킵니다.
        btn1Clicked?.Invoke(sender, e);
        await this.CloseAsync();
    }

    // "취소" 버튼이 클릭될 때 실행될 메소드
    private async void Onbtn2Clicked(object sender, EventArgs e)
    {
        await this.CloseAsync();
    }

    public string MainText
    {
        get => maintext.Text;
        set => maintext.Text = value;
    }

    public string btn1Text
    {
        get => btn1.Text;
        set => btn1.Text = value;
    }

}
