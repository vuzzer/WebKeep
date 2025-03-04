using WebKeep.DTO;
using WebKeep.Entites;
using WebKeep.Services;

namespace WebKeep.Pages;

public partial class Accueil : ContentPage
{
	public Accueil()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var idCompte = Preferences.Get("Id", 0);
        var taches = (from s in ServiceDB.ConnexionDB.Table<Site>()
                      where s.IdCompte == idCompte
                      orderby s.Date descending
                      select new SiteDTO
                      {
                          Nom = s.Nom,
                          Lien = s.Lien,
                          Note = s.Note,
                          Date = s.Date.ToString("dd MMM yyyy")
                      }).ToList();
        sites.ItemsSource = taches;
    }

    private async void tbAjouter_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NouveauSite());
    }

    private void tbSauvegarder_Clicked(object sender, EventArgs e)
    {

    }

    private async void sites_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0)
            return;

        var site = e.CurrentSelection[0] as SiteDTO;

        if (site == null) return;

        var param = ServiceDB.ConnexionDB.Table<Site>().Where(s => s.Nom.ToLower() == site.Nom.ToLower()).FirstOrDefault();
        await Navigation.PushAsync(new NouveauSite(param));
    }

    private void txtRecherche_TextChanged(object sender, TextChangedEventArgs e)
    {
        string searchText = e.NewTextValue?.ToLower() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(searchText))
        {
            sites.ClearValue();
            foreach (var site in Sites)
                FilteredSites.Add(site);
            return;
        }
    }
}