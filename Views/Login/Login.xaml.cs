using DPAV.Views.Perros;
using System.Windows.Input;

namespace DPAV.Views.Login;

public partial class Login : ContentPage
{
    public Login()
	{
		InitializeComponent();
    }

    private void OnLogin(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AppShell());
    }

    private void OnRegister(object sender, EventArgs e)
    {
        App.Current.MainPage = new Register();
    }

}