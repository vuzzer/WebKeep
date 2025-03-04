using WebKeep.Entites;
using WebKeep.Services;

namespace WebKeep.Pages;

public partial class Connexion : ContentPage
{
	public Connexion()
	{
		InitializeComponent();
	}

    private async void btnConnexion_Clicked(object sender, EventArgs e)
    {
		if (string.IsNullOrEmpty(txtNomUtilisateur.Text))
		{
            await DisplayAlert("Info", "Le nom d'utilisateur est obligatoire", "OK");
			return;
        }
		if (string.IsNullOrEmpty(txtPwd.Text))
		{
            await DisplayAlert("Info", "Le mot de passe est obligatoire", "OK");
            return;
        }

        var compte = ServiceDB.ConnexionDB.Table<Compte>().Where(c => c.NomUtilisateur.ToLower() == txtNomUtilisateur.Text.ToLower()).FirstOrDefault();

        if (compte == null)
        {

            bool answer = await DisplayAlert("Attention", "Un utilisateur avec ce nom n'existe pas. Voulez-vous créer un utilisateur avec ce nom et mot de passe", "Oui", "Non");

            // Créer un utilisateur
            if (answer)
            {
                compte = new Compte
                {
                    NomUtilisateur = txtNomUtilisateur.Text,
                    MotDePasse = txtPwd.Text
                };
                ServiceDB.ConnexionDB.Insert(compte);
                txtNomUtilisateur.Text = string.Empty;
                txtPwd.Text = string.Empty;
                await DisplayAlert("Info", "Utilisateur créé avec succès", "OK");
            }

            return;
        }

        // Vérifier le mot de passe
        if (compte.MotDePasse != txtPwd.Text)
        {
            await DisplayAlert("Attention", "Le mot de passe est incorrect", "Fermer");
            return;
        }

        // Rediriger vers la page principale
        Preferences.Set("Id",compte.Id);
        await Shell.Current.GoToAsync("//Accueil");
    }
}