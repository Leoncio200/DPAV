using DPAV.Models;
using DPAV.ViewModels;
using DPAV.Views.Perros;
using System.Windows.Input;

namespace DPAV.Views.Login;

public partial class Login : ContentPage
{
    private readonly UsuarioViewModel _usuarioViewModel;
    public Login()
	{
        _usuarioViewModel = new UsuarioViewModel();
        BindingContext = _usuarioViewModel;

        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Cargar sesión y redirigir si el usuario ya está logueado
        await CheckIfLoggedInAsync();
    }

    private async Task CheckIfLoggedInAsync()
    {
        // Cargar la sesión
        await Singleton.Instance.LoadSessionAsync();

        // Verificar si el usuario está logueado
        if (await Singleton.Instance.UserIsLogin())
        {
            // Redirigir a la página principal
            App.Current.MainPage = new NavigationPage(new AppShell());
        }
    }

    private async void OnLogin(object sender, EventArgs e)
    {
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;

        if(!Validar(email,password))
            return;

        Usuario User = new Usuario { Email = email, Password = password };

        _usuarioViewModel.NuevoUsuario = User;


        if (await _usuarioViewModel.Login())
        {
            App.Current.MainPage = new NavigationPage(new AppShell());
        }
    }

    private void OnRegister(object sender, EventArgs e)
    {
        App.Current.MainPage = new Register();
    }

    private bool Validar(string email, string password)
    {
        if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password))
        {
            _ = DisplayAlert("Error", "Por favor, complete todos los campos.", "OK");
            return false;
        }

        return true;
    }

}