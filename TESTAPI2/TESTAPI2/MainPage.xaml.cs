using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAPIGSB.Pages;
using Xamarin.Forms;

namespace TESTAPI2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();   
        }
        private async void btnSecteurs_Clicked(object sender, EventArgs e)
        {
            PageSecteur page = new PageSecteur();
            await Navigation.PushModalAsync(new NavigationPage(page));
        }

        private async void btnRegions_Clicked(object sender, EventArgs e)
        {
            PageRegion page = new PageRegion();
            await Navigation.PushModalAsync(new NavigationPage(page));
        }

        private void btnVisiteurs_Clicked(object sender, EventArgs e)
        {

        }

        private void btnTravailler_Clicked(object sender, EventArgs e)
        {

        }
    }
}
