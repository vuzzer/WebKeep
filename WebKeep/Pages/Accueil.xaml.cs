namespace WebKeep.Pages;

public partial class Accueil : ContentPage
{
	public Accueil()
	{
		InitializeComponent();
	}

    private async void tbAjouter_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NouveauSite());
    }

    private void tbSauvegarder_Clicked(object sender, EventArgs e)
    {

    }
}