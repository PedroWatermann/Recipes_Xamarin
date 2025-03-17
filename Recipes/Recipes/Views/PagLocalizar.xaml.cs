using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recipes.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PagLocalizar : ContentPage
	{
		public PagLocalizar ()
		{
			InitializeComponent ();
		}

        private void btnHome_Clicked(object sender, EventArgs e)
        {
            FlyoutPage fp = (FlyoutPage)Application.Current.MainPage;
            fp.Detail = new NavigationPage(new PagHome())
            {
                BarBackgroundColor = Color.FromHex("#C85400")
            };
            fp.IsPresented = false;
        }

        private void lvwReceita_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void swtFavorito_Toggled(object sender, ToggledEventArgs e)
        {

        }

        private void btnPesquisar_Clicked(object sender, EventArgs e)
        {

        }
    }
}