using DPAV.Models;
using Newtonsoft.Json;
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

namespace DPAV.ViewModels
{
    public class RazaViewModel: INotifyPropertyChanged
    {
        private ObservableCollection<Raza> _razas;
        private dynamic _nuevaRaza;
        private bool _isRazasEmpty;
        private bool _isInitialLoad = true;
        private bool _isLoading = false;
        private readonly ServiceHttpClient _serviceHttpClient;

        public ICommand EliminarRazaCommand { get; }


        public ObservableCollection<Raza> Razas
        {
            get { return _razas; }
            set { SetProperty(ref _razas, value); }
        }

        public dynamic NuevaRaza
        {
            get { return _nuevaRaza; }
            set { SetProperty(ref _nuevaRaza, value); }
        }

        public bool IsPerrosEmpty
        {
            get { return _isRazasEmpty; }
            set { SetProperty(ref _isRazasEmpty, value); }
        }

        /*public async Task<bool> RegistrarRaza()
        {
            try
            {
                _isLoading = true;

                // Llamar a la API para registrar al perro
                var response = await _serviceHttpClient.PostAsync("crearRaza", NuevaRaza, true);

                var raza =  JsonConvert.DeserializeObject<Raza>(response);

                if (raza != null)
                {
                    await App.Current.MainPage.DisplayAlert("Success", "Raza creada correctamente", "OK");
                    return true;
                }
                await App.Current.MainPage.DisplayAlert("Error", "No se pudo registrar la raza", "OK");
            }
            catch (HttpRequestException ex)
            {
                // Manejar errores específicos
                await App.Current.MainPage.DisplayAlert("Error", $"Error al registrar la raza: {ex.Message}", "OK");
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

        public async Task<bool> ActualizarRaza(int idPerro)
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
                    return true;
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
            return false;
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
                    if (Perros.Count == 1)
                        Perros = new ObservableCollection<Perro>();
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
        }*/

        public RazaViewModel()
        {
            Razas = new ObservableCollection<Raza>();
            _serviceHttpClient = new ServiceHttpClient();
            //EliminarCasaCommand = new Command<Perro>(async (casa) => await DeleteCasa(casa));
        }

        public async Task GetRazas()
        {
            try
            {
                // Llamar a la API para registrar al usuario
                var response = await _serviceHttpClient.GetAsync("razas", false);

                // Procesar la respuesta
                using JsonDocument document = JsonDocument.Parse(response);
                JsonElement root = document.RootElement;

                var razas = JsonConvert.DeserializeObject<ObservableCollection<Raza>>(response, new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Ignore, NullValueHandling = NullValueHandling.Ignore });

                if (razas != null)
                {
                    Razas = razas;
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Error al intentar traer las razas: {ex.Message}", "OK");
            }
        }

        public async Task GetDatosAsync()
        {
            await GetRazas();
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
