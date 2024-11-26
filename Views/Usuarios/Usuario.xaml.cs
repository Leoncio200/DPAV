namespace DPAV.Views.Usuarios;

public partial class Usuario : ContentPage
{
	public Usuario()
	{
		InitializeComponent();
	}

    private async void BtnModificarPerfil_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new ModificarUsuario());
    }
}