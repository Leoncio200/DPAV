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
    public class RazaViewModel: INotifyPropertyChanged
    {
        private ObservableCollection<Raza> _razas;
        private dynamic _nuevaRaza;
        private bool _isRazasEmpty;
        private bool _isInitialLoad = true;
        private bool _isLoading = false;

        public ICommand EliminarRazaCommand { get; }


        public ObservableCollection<Raza> Razas
        {
            get { return _razas; }
            set { SetProperty(ref _razas, value); }
        }

        public dynamic NuevoPerro
        {
            get { return _nuevaRaza; }
            set { SetProperty(ref _nuevaRaza, value); }
        }

        public bool IsPerrosEmpty
        {
            get { return _isRazasEmpty; }
            set { SetProperty(ref _isRazasEmpty, value); }
        }

        public RazaViewModel()
        {
            Razas = new ObservableCollection<Raza>();
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
