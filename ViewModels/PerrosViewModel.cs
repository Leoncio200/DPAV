using DPAV.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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

        public PerrosViewModel()
        {
            Perros = new ObservableCollection<Perro>();
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
