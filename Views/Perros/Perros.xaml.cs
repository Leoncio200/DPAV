using DPAV.Models;
using DPAV.ViewModels;

namespace DPAV.Views.Perros;

public partial class Perros : ContentPage
{
    private readonly RazaViewModel _razaViewModel;
    private readonly PerrosViewModel _perroViewModel;
	public Perros()
    {
        _razaViewModel = new RazaViewModel();
        _perroViewModel = new PerrosViewModel();

        BindingContext = _perroViewModel;
        InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _razaViewModel.GetDatosAsync();
        await _perroViewModel.GetDatosAsync();
    }

    private async void OnModificarClicked(object sender, EventArgs e)
    {
        var boton = (Button)sender;

        Perro perro = (Perro)boton.CommandParameter;

        await Navigation.PushAsync(new ModificarPerro(perro, _razaViewModel));
    }

    private async void OnAgregarClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AgregarPerro(_razaViewModel));
    }

    private async void OnEliminarClicked(object sender, EventArgs e)
    {
        var boton = (Button)sender;

        Perro perro = (Perro)boton.CommandParameter;

        bool respuesta = await DisplayAlert("Advertencia!!", $"Estas seguro de que deseas eliminar a {perro.Nombre}", "Si", "Cancelar");

        if (!respuesta) 
            return;

        await _perroViewModel.EliminarPerro(perro.Id);

        await _perroViewModel.GetDatosAsync();
    }
}