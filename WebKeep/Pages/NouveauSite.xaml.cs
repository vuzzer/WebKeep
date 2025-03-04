using WebKeep.Enums;

namespace WebKeep.Pages;

public partial class NouveauSite : ContentPage
{
	Status statusCourant;
	public NouveauSite(Status status = Status.Creer)
	{
		InitializeComponent();
        statusCourant = status;

        if (statusCourant == Status.Creer)
        {
            appTitle.Text = "Nouveau site";
            btnSupprimer.IsVisible = false;
        }
        else
        {
            appTitle.Text = "Modification";
            btnSupprimer.IsVisible = true;
        }
    }

    private void btnEnregistrer_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtNom.Text))
        {
            DisplayAlert("Erreur", "Le nom du site est obligatoire", "OK");
        }
        else if (string.IsNullOrEmpty(txtNote.Text))
        {
            DisplayAlert("Erreur", "La note est obligatoire", "OK");
        }
        else
        {
            DisplayAlert("Succès", "Site enregistré avec succès", "OK");
        }
    }

    private void btnSupprimer_Clicked(object sender, EventArgs e)
    {

    }
}