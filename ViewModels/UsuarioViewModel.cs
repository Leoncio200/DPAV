using DPAV.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;

namespace DPAV.ViewModels
{
    public class UsuarioViewModel
    {
        private ObservableCollection<Usuario> _usuarios;
        private Usuario _nuevoUsuario;
        private bool _isUsuariosEmpty;
        private bool _isInitialLoad = true;
        private bool _isLoading = false;
        private readonly ServiceHttpClient _serviceHttpClient;

        public ICommand EliminarUsuarioCommand { get; }


        public ObservableCollection<Usuario> Usuarios
        {
            get { return _usuarios; }
            set { SetProperty(ref _usuarios, value); }
        }

        public Usuario NuevoUsuario
        {
            get { return _nuevoUsuario; }
            set { SetProperty(ref _nuevoUsuario, value); }
        }

        public bool IsUsuariosEmpty
        {
            get { return _isUsuariosEmpty; }
            set { SetProperty(ref _isUsuariosEmpty, value); }
        }

        public async Task<bool> Login()
        {
            try
            {
                // Llamar a la API para registrar al usuario
                var response = await _serviceHttpClient.PostAsync("login", NuevoUsuario, false);

                // Procesar la respuesta
                using JsonDocument document = JsonDocument.Parse(response);
                JsonElement root = document.RootElement;

                // Acceder a las propiedades del JSON
                string message = root.GetProperty("message").ToString();
                string userElement = root.GetProperty("user").ToString();
                string token = root.GetProperty("token").ToString();

                var user = JsonConvert.DeserializeObject<Usuario>(userElement, new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Ignore, NullValueHandling = NullValueHandling.Ignore});

                if (user != null)
                {
                    if (user.RoleId != 3)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", $"No se pudo loguear", "OK");
                        return false;
                    }

                    await Singleton.Instance.SetSession(userElement, token, user);

                    Usuarios.Add(user);
                    NuevoUsuario = new Usuario();
                    //Singleton.Instance
                    // Limpiar el formulario
                    return true;
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Error al intentar logearse: {ex.Message}", "OK");
            }
            return false;
        }

        public async Task<bool> RegistrarUsuario()
        {
            try
            {
                _isLoading = true;

                // Llamar a la API para registrar al usuario
                var response = await _serviceHttpClient.PostAsync("registrar", NuevoUsuario, false);

                // Procesar la respuesta
                var user = JsonConvert.DeserializeObject<Usuario>(response);

                if (user != null)
                {
                    Usuarios.Add(user);
                    NuevoUsuario = new Usuario(); // Limpiar el formulario
                    await App.Current.MainPage.DisplayAlert("Éxito", "Usuario registrado correctamente.", "OK");
                    return true;
                }
                await App.Current.MainPage.DisplayAlert("Error", "No se pudo registrar el usuario", "OK");
            }
            catch (HttpRequestException ex)
            {
                // Manejar errores específicos
                await App.Current.MainPage.DisplayAlert("Error", $"Error al registrar el usuario: {ex.Message}", "OK");
            }
            catch (Exception ex)
            {
                // Manejar errores generales
                await App.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
            finally
            {
                _isLoading = false;
            }
            return false;
        }

        public async Task<bool> Logout()
        {
            try
            {
                // Llamar a la API para registrar al usuario
                var response = await _serviceHttpClient.PostAsync("logout", NuevoUsuario, true);

                
                if (response.Contains("Sesión cerrada"))
                {
                    Singleton.Instance.Logout();

                    Usuarios.Clear();
                    NuevoUsuario = new Usuario();
                   
                    return true;
                }
                await App.Current.MainPage.DisplayAlert("Error", $"No se pudo cerrar la sesion.", "OK");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Error al intentar cerrar sesion: {ex.Message}", "OK");
            }

            return false;
        }

        public UsuarioViewModel()
        {
            _serviceHttpClient = new ServiceHttpClient();
            Usuarios = new ObservableCollection<Usuario>();
            //EliminarCasaCommand = new Command<Perro>(async (casa) => await DeleteCasa(casa));
        }

        public async Task GetDatosAsync()
        {
            //await GetCasas();
        }

        private string NormalizeString(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var normalizedString = input.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            var cleanString = stringBuilder.ToString()
                .Replace(",", " ")
                .Replace(".", " ")
                .Replace("á", "a")
                .Replace("é", "e")
                .Replace("í", "i")
                .Replace("ó", "o")
                .Replace("ú", "u");

            cleanString = Regex.Replace(cleanString, @"\s+", " ").Trim();

            return cleanString.ToLower();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null!)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
