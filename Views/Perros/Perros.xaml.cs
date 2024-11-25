namespace DPAV.Views.Perros;

public partial class Perros : ContentPage
{
	public Perros()
	{
		InitializeComponent();
	}

    private async void ButtonModificar_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new ModificarPerro());
    }
}