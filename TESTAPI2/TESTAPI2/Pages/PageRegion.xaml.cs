using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestAPIGSB.ClassesMetier;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Region = TestAPIGSB.ClassesMetier.Region;

namespace TestAPIGSB.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageRegion : ContentPage
    {
        public PageRegion()
        {
            InitializeComponent();
        }
        HttpClient ws;
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            List<Region> lesRegions = new List<Region>();

            ws = new HttpClient();
            var reponse = await ws.GetAsync("http://10.0.2.2/Bidon/SIO2ALT/SIO2ALT/APIGSB/regions/");
            var content = await reponse.Content.ReadAsStringAsync();
            lesRegions = JsonConvert.DeserializeObject<List<Region>>(content);
            lvRegions.ItemsSource = lesRegions;
        }

        private void lvRegions_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (lvRegions.SelectedItem != null)
            {
                txtNomRegion.Text = (lvRegions.SelectedItem as Region).Nom;
            }
        }

        private async void btnModifier_Clicked(object sender, EventArgs e)
        {
            if (txtNomRegion.Text == null)
            {
                Toast.MakeText(Android.App.Application.Context, "Sélectionner une region", ToastLength.Short).Show();
            }
            else
            {
                ws = new HttpClient();
                Region reg = (lvRegions.SelectedItem as Region);
                reg.Nom = txtNomRegion.Text;
                JObject jreg = new JObject
{
{"Identifiant",reg.Identifiant},
{"Nom",reg.Nom}
};
                string json = JsonConvert.SerializeObject(jreg);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var reponse = await ws.PutAsync("http://10.0.2.2/SIO2ALT/APIGSB/regions/", content);
                List<Region> lesRegions = new List<Region>();

                ws = new HttpClient();
                reponse = await ws.GetAsync("http://10.0.2.2/SIO2ALT/APIGSB/regions/");
                var flux = await reponse.Content.ReadAsStringAsync();
                lesRegions = JsonConvert.DeserializeObject<List<Region>>(flux);
                lvRegions.ItemsSource = lesRegions;
            }
        }

        private async void btnAjouter_Clicked(object sender, EventArgs e)
        {
            if (txtNomRegion.Text == null)
            {
                Toast.MakeText(Android.App.Application.Context, "Saisir un nom de region", ToastLength.Short).Show();
            }
            else
            {
                ws = new HttpClient();
                //Secteur newSecteur = new Secteur();
                //newSecteur.Nom = txtNomSecteur.Text;
                JObject reg = new JObject
{
{ "Reg", txtNomRegion.Text}
};
                string json = JsonConvert.SerializeObject(reg);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                var reponse = await ws.PostAsync("http://10.0.2.2/SIO2ALT/APIGSB/regions/", content);
                List<Region> lesRegions = new List<Region>();

                ws = new HttpClient();
                reponse = await ws.GetAsync("http://10.0.2.2/SIO2ALT/APIGSB/regions/");
                var flux = await reponse.Content.ReadAsStringAsync();
                lesRegions = JsonConvert.DeserializeObject<List<Region>>(flux);
                lvRegions.ItemsSource = lesRegions;
            }
        }
    }
}