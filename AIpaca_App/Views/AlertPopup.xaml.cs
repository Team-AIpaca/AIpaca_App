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

    // "������Ʈ" ��ư�� Ŭ���� �� ����� �޼ҵ�
    private async void Onbtn1Clicked(object sender, EventArgs e)
    {
        // SplashPage���� ó���� �� �ֵ��� �̺�Ʈ�� �߻���ŵ�ϴ�.
        btn1Clicked?.Invoke(sender, e);
        await this.CloseAsync();
    }

    // "���" ��ư�� Ŭ���� �� ����� �޼ҵ�
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
