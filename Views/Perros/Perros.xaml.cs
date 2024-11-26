namespace DPAV.Views.Perros;

public partial class Perros : ContentPage
{
	public Perros()
	{
		InitializeComponent();
	}
    
    private async void OnModificarClicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new ModificarPerro());
    }

    private async void OnAgregarClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AgregarPerro());
    }
}