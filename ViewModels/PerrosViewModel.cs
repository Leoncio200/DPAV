using DPAV.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DPAV.ViewModels
{
    public class PerrosViewModel: INotifyPropertyChanged
    {
        private ObservableCollection<Perro> _perros;
        private dynamic _nuevoPerro;
        private bool _isPerrosEmpty;
        private bool _isInitialLoad = true;
        private bool _isLoading = false;
        private readonly ServiceHttpClient _serviceHttpClient;

        public ICommand EliminarCasaCommand { get; }


        public ObservableCollection<Perro> Perros
        {
            get { return _perros; }
            set { SetProperty(ref _perros, value); }
        }

        public dynamic NuevoPerro
        {
            get { return _nuevoPerro; }
            set { SetProperty(ref _nuevoPerro, value); }
        }

        public bool IsPerrosEmpty
        {
            get { return _isPerrosEmpty; }
            set { SetProperty(ref _isPerrosEmpty, value); }
        }

        public async Task GetPerros()
        {
            try
            {
                // Llamar a la API para registrar al usuario
                var response = await _serviceHttpClient.GetAsync("mostrarPerros", true);

                // Procesar la respuesta
                using JsonDocument document = JsonDocument.Parse(response);
                JsonElement root = document.RootElement;

                string message = root.GetProperty("message").ToString();
                string perrosElement = root.GetProperty("perros").ToString();

                if (message == "Perros encontrados")
                {
                    var perros = JsonConvert.DeserializeObject<ObservableCollection<Perro>>(perrosElement, new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Ignore, NullValueHandling = NullValueHandling.Ignore });
                    
                    if(perros != null)
                    {
                        Perros = perros;
                        return;
                    }
                }
                await App.Current.MainPage.DisplayAlert("Error", $"Error al intentar traer los perros.", "OK");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Error al intentar en la funcion: {ex.Message}", "OK");
            }
        }

        public async Task RegistrarPerro()
        {
            try
            {
                _isLoading = true;

                // Llamar a la API para registrar al perro
                var response = await _serviceHttpClient.PostAsync("crearPerro", NuevoPerro, true);

                // Procesar la respuesta
                using JsonDocument document = JsonDocument.Parse(response);
                JsonElement root = document.RootElement;

                // Acceder a las propiedades del JSON
                string message = root.GetProperty("message").ToString();
                string perroElement = root.GetProperty("perro").ToString();

                if (message == "Perro creado correctamente")
                {
                    await App.Current.MainPage.DisplayAlert("Success", message, "OK");
                    return;
                }
                await App.Current.MainPage.DisplayAlert("Error", "No se pudo registrar el perro", "OK");
            }
            catch (HttpRequestException ex)
            {
                // Manejar errores específicos
                await App.Current.MainPage.DisplayAlert("Error", $"Error al registrar el perro: {ex.Message}", "OK");
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
        }

        public async Task ActualizarPerro(int idPerro)
        {
            try
            {
                _isLoading = true;

                // Llamar a la API para registrar al perro
                var response = await _serviceHttpClient.PutAsync("actualizarPerro/" + idPerro, NuevoPerro, true);

                // Procesar la respuesta
                using JsonDocument document = JsonDocument.Parse(response);
                JsonElement root = document.RootElement;

                // Acceder a las propiedades del JSON
                string message = root.GetProperty("message").ToString();
                string perroElement = root.GetProperty("perro").ToString();

                if (message == "Perro actualizado correctamente")
                {
                    await App.Current.MainPage.DisplayAlert("Success", message, "OK");
                    return;
                }
                await App.Current.MainPage.DisplayAlert("Error", "No se pudo actualizar el perro", "OK");
            }
            catch (HttpRequestException ex)
            {
                // Manejar errores específicos
                await App.Current.MainPage.DisplayAlert("Error", $"Error al actualizar el perro: {ex.Message}", "OK");
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
        }

        public async Task EliminarPerro(int idPerro)
        {
            try
            {
                _isLoading = true;

                // Llamar a la API para registrar al perro
                var response = await _serviceHttpClient.DeleteAsync("eliminarPerro/" + idPerro, true);

                // Procesar la respuesta
                using JsonDocument document = JsonDocument.Parse(response);
                JsonElement root = document.RootElement;

                // Acceder a las propiedades del JSON
                string message = root.GetProperty("message").ToString();

                if (message == "Perro eliminado correctamente")
                {
                    await App.Current.MainPage.DisplayAlert("Success", message, "OK");
                    return;
                }
                await App.Current.MainPage.DisplayAlert("Error", message, "OK");
            }
            catch (HttpRequestException ex)
            {
                // Manejar errores específicos
                await App.Current.MainPage.DisplayAlert("Error", $"Error al mandar la peticion: {ex.Message}", "OK");
            }
            catch (Exception ex)
            {
                // Manejar errores generales
                await App.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error al intentar eliminar el perro: {ex.Message}", "OK");
            }
            finally
            {
                _isLoading = false;
            }
        }

        public PerrosViewModel()
        {
            Perros = new ObservableCollection<Perro>();
            _serviceHttpClient = new ServiceHttpClient();
            //EliminarCasaCommand = new Command<Perro>(async (casa) => await DeleteCasa(casa));
        }

        public async Task GetDatosAsync()
        {
            await GetPerros();
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
