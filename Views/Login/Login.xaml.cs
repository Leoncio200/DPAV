using DPAV.Views.Perros;

namespace DPAV.Views.Login;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

    private async void ButtonRegistro_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Register());
    }

    private void ButtonInicio_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AppShell());
    }
}