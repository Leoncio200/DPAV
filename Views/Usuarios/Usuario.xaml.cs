using DPAV.Models;
using DPAV.ViewModels;
using DPAV.Views.Login;

namespace DPAV.Views.Usuarios;

public partial class Usuario : ContentPage
{
    private UsuarioViewModel _usuarioViewModel;
    private Models.Usuario _user;
	public Usuario()
	{
        _usuarioViewModel = new UsuarioViewModel();
        _user = Singleton.Instance.UGlobal;
        InitializeComponent();
	}

    protected override void OnAppearing()
    {
        LlenarInfo();
        base.OnAppearing();
    }

    private void LlenarInfo()
    {
        LabelUsuario.Text = _user.Nombre;
        LabelEmail.Text = _user.Email;  
        LabelTelefono.Text = _user.Telefono;
    }

    private async void BtnModificarPerfil_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new ModificarUsuario());
    }

    private async void OnClickLogout(object sender, EventArgs e)
	{
        if(await _usuarioViewModel.Logout())
        {
            App.Current.MainPage = new Login.Login();
        }
	}
}