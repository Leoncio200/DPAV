using DPAV.Models;
using DPAV.ViewModels;

namespace DPAV.Views.Perros;

public partial class AgregarPerro : ContentPage
{
	private readonly PerrosViewModel _perroViewModel;
	private readonly RazaViewModel _razaViewModel;
	private bool _DateSelect = false;
	public AgregarPerro(RazaViewModel binding)
	{
		_razaViewModel = binding;
		_perroViewModel = new PerrosViewModel();
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
		razasPicker.ItemsSource = _razaViewModel.Razas;

        base.OnAppearing();
    }

	private bool Validar()
	{
		if(String.IsNullOrEmpty(NombreEntry.Text) || razasPicker.SelectedItem == null || 
			String.IsNullOrEmpty(EdadEntry.Text) || String.IsNullOrEmpty(ColorEntry.Text) ||
            String.IsNullOrEmpty(AlturaEntry.Text) || tamañoPicker.SelectedItem == null ||
            String.IsNullOrEmpty(PesoEntry.Text) || SexoPicker.SelectedItem == null ||
            !_DateSelect)
		{ 
			_ = DisplayAlert("Alert", $"Llene todos los campos necesarios.", "OK");
			return false;
		}

        return true;
	}

    private void FechaNacimientoPicker_DateSelected(object sender, DateChangedEventArgs e)
    {
		_DateSelect = true;
    }

    private async void OnClickAceptar(object sender, EventArgs e)
    {
		if (!Validar())
			return;

		var raza = (Raza)razasPicker.SelectedItem;

		Perro perro = new Perro
		{
			Nombre = NombreEntry.Text,
			IDRaza = raza.Id,
			Edad = Convert.ToInt32(EdadEntry.Text),
			Color = ColorEntry.Text,
			Altura = Convert.ToDouble(AlturaEntry.Text),
			Tamaño = tamañoPicker.SelectedItem.ToString(),
			Peso = Convert.ToDouble(PesoEntry.Text),
			Sexo = SexoPicker.SelectedItem.ToString(),
			FechaNacimiento = FechaNacimientoPicker.Date,
			Estatus = 1,
			Esterilizado = EsterilizadoCheckBox.IsChecked ? "Si" : "No"
		};

		_perroViewModel.NuevoPerro = perro;

		if(await _perroViewModel.RegistrarPerro())
		{
			await Navigation.PopAsync();
		}
    }
}