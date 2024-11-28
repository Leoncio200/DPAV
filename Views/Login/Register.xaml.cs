using DPAV.Models;
using DPAV.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace DPAV.Views.Login;

public partial class Register : ContentPage
{
    private readonly UsuarioViewModel _usuarioViewModel;
	public Register()
	{
		InitializeComponent();
        _usuarioViewModel = new UsuarioViewModel();
        BindingContext = _usuarioViewModel;
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        
        try
        {
            // Obtener valores de los campos
            string txtnombre = NombreEntry.Text;
            string txtapellidoPaterno = ApellidoPaternoEntry.Text;
            string txttelefono = TelefonoEntry.Text;
            string txtemail = EmailEntry.Text;
            string txtpassword = PasswordEntry.Text;
            string txtconfirmPassword = ConfirmPasswordEntry.Text;

            bool resVal = Validator(txtnombre, txtapellidoPaterno, txttelefono, txtemail, txtpassword, txtconfirmPassword);

            if (!resVal)
                return;

            // Preparar datos para la API
            _usuarioViewModel.NuevoUsuario = new Usuario
            {

                Nombre = txtnombre,
                ApellidoPaterno = txtapellidoPaterno,
                Telefono = txttelefono,
                Email = txtemail,
                Password = txtpassword

            };

            await _usuarioViewModel.RegistrarUsuario();
        }
        catch (HttpRequestException ex)
        {
            // Mostrar errores devueltos por la API
            await DisplayAlert("Error", $"No se pudo registrar el usuario. {ex.Message}", "OK");
        }

        App.Current.MainPage = new Login();
    }

	private bool Validator(string nombre, string apellidoPaterno, string telefono, string email, string password, string confirmPassword)
	{

        // Validar campos obligatorios
        if (string.IsNullOrWhiteSpace(nombre) ||
            string.IsNullOrWhiteSpace(apellidoPaterno) ||
            string.IsNullOrWhiteSpace(telefono) ||
            string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password) ||
            string.IsNullOrWhiteSpace(confirmPassword))
        {
            _ = DisplayAlert("Error", "Por favor, complete todos los campos.", "OK");
            return false;
        }

        // Validar coincidencia de contraseñas
        if (password != confirmPassword)
        {
            _ = DisplayAlert("Error", "Las contraseñas no coinciden.", "OK");
            return false;
        }

        return true;
    }
}