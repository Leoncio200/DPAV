using DPAV.Models;
using DPAV.ViewModels;

namespace DPAV.Views.Perros;

public partial class ModificarPerro : ContentPage
{
	private Perro _perro;
	private RazaViewModel _razaViewModel;
	private PerrosViewModel _perrosViewModel;
	public ModificarPerro(Perro perro, RazaViewModel razas)
	{
		_perro = perro;
		_razaViewModel = razas;
		_perrosViewModel = new PerrosViewModel();
		BindingContext = _perrosViewModel;
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
		LlenarPantalla();
        base.OnAppearing();
    }

    private void LlenarPantalla()
	{
		NombreEntry.Text = _perro.Nombre;
		razasPicker.ItemsSource = _razaViewModel.Razas;
        razasPicker.SelectedItem = _razaViewModel.Razas.FirstOrDefault(raza => raza.Id == _perro.IDRaza);
        EdadEntry.Text = _perro.Edad.ToString();
		ColorEntry.Text = _perro.Color;
		AlturaEntry.Text = _perro.Altura.ToString();
		tamañoPicker.SelectedItem = _perro.Tamaño;
		PesoEntry.Text = _perro.Peso.ToString();
		SexoPicker.SelectedItem = _perro.Sexo;
        if (_perro.FechaNacimiento.HasValue)
            FechaNacimientoPicker.Date = _perro.FechaNacimiento.Value;
        else
            FechaNacimientoPicker.Date = DateTime.Today;
		EsterilizadoCheckBox.IsChecked = _perro.Esterilizado == "Si" ? true : false;

    }

	private async void OnClickAceptar(object sender, EventArgs e)
	{
		if(!VerificarCampos())
			return;

        var raza = (Raza)razasPicker.SelectedItem;

		_perro.IDRaza = raza.Id;

		_perrosViewModel.NuevoPerro = _perro;

		if (await _perrosViewModel.ActualizarPerro(_perro.Id))
		{
            await Navigation.PopAsync();
        }
    }

	private bool VerificarCampos()
	{
		try
		{
            if (_perro.Nombre != NombreEntry.Text)
                _perro.Nombre = NombreEntry.Text;

            if (_perro.Edad.ToString() != EdadEntry.Text)
                _perro.Edad = Convert.ToInt32(EdadEntry.Text);

            if (_perro.Color != ColorEntry.Text)
                _perro.Color = ColorEntry.Text;

            if (_perro.Altura.ToString() != AlturaEntry.Text)
                _perro.Altura = Convert.ToInt32(AlturaEntry.Text);

			_perro.Tamaño = tamañoPicker.SelectedItem.ToString();

            if (_perro.Peso.ToString() != PesoEntry.Text)
                _perro.Peso = Convert.ToInt32(PesoEntry.Text);

			_perro.Sexo = SexoPicker.SelectedItem.ToString();

			_perro.FechaNacimiento = FechaNacimientoPicker.Date;

			_perro.Esterilizado = EsterilizadoCheckBox.IsChecked ? "Si" : "No";

            return true;
        }
		catch 
		{
			DisplayAlert("Error", "Llene los todos los campos", "OK");
			return false;
		}
	}
}