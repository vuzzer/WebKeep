using WebKeep.Entites;
using WebKeep.Enums;
using WebKeep.Services;

namespace WebKeep.Pages;

public partial class NouveauSite : ContentPage
{
	Status statusCourant;
    Site siteCourant;
    public NouveauSite()
	{
		InitializeComponent();
        statusCourant = Status.Creer;
        btnSupprimer.IsVisible = false;
    }

    public NouveauSite(Site site)
    {
        InitializeComponent();
        statusCourant = Status.Modifier;
        siteCourant = site;
        txtNom.Text = site.Nom;
        txtNote.Text = site.Note;
        txtLien.Text = site.Lien;
        appTitle.Text = "Modification";
        btnSupprimer.IsVisible = true;
    }

    private async void btnEnregistrer_Clicked(object sender, EventArgs e)
    {
        int idCompte = Preferences.Get("Id", 0);
        if (string.IsNullOrEmpty(txtNom.Text))
        {
            await DisplayAlert("Erreur", "Le nom du site est obligatoire", "OK");
            return;
        }
        else if (string.IsNullOrEmpty(txtNote.Text) && string.IsNullOrEmpty(txtLien.Text))
        {

            bool answer = await DisplayAlert("Erreur", "La note et le lien sont vides. Souhaitez-vous continuer l'enregistrement ?", "Oui", "Non");
            if(answer)
            {

                var note = ServiceDB.ConnexionDB.Table<Site>().Where(s => s.Nom.ToLower() == txtNom.Text.ToLower() && s.IdCompte == idCompte ).FirstOrDefault();

                if (note != null)
                {
                    await DisplayAlert("Erreur", "Ce site existe déjà", "OK");
                    return;
                }

                var siteExistant = ServiceDB.ConnexionDB.Table<Site>().Where(s => s.Nom.ToLower() == txtLien.Text.ToLower() && s.IdCompte == idCompte).FirstOrDefault();
                if (siteExistant != null) {
                    bool answerTwo = await DisplayAlert("Erreur", "Ce lien existe déjà. Souhaitez-vous continuer l'enregistrement ?", "Oui", "Non");
                    if (!answerTwo)
                    {
                        return;
                    }
                }
            }
            return;
        }
        else if(!string.IsNullOrEmpty(txtLien.Text))
        {
            string pattern = @"^www\..+\.(com|ca)$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtLien.Text, pattern))
            {
                await DisplayAlert("Erreur", "Le lien n'est pas valide", "OK");
                return;
            }
        }

        // Enregistrer le site
        if (statusCourant == Status.Modifier)
        {
            siteCourant.Nom = txtNom.Text;
            siteCourant.Note = txtNote.Text;
            siteCourant.Lien = txtLien.Text;
            ServiceDB.ConnexionDB.Update(siteCourant);
            await DisplayAlert("Succès", "Site modifié avec succès", "OK");
        }
        else
        {
            Site site = new Site
            {
                Nom = txtNom.Text,
                Note = txtNote.Text,
                Lien = txtLien.Text,
                Date = DateTime.Now,
                IdCompte = idCompte
            };
            ServiceDB.ConnexionDB.Insert(site);
            await DisplayAlert("Succès", "Site enregistré avec succès", "OK");
        }


        await Navigation.PopAsync();
    }

    private async void btnSupprimer_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Attention", "Voulez-vous vraiment supprimer ce site ?", "Oui", "Non");
        if (answer)
        {
            ServiceDB.ConnexionDB.Delete(siteCourant);
            await DisplayAlert("Succès", "Site supprimé avec succès", "OK");
            await Navigation.PopAsync();
        }
    }
}