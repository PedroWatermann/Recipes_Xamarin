using Recipes.Models;
using Recipes.Services;
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

            atualizaLista();
		}

        public void atualizaLista()
        {
            string t = string.IsNullOrWhiteSpace(entTitulo.Text) ? "" : entTitulo.Text;

            SerDbRecipes db = new SerDbRecipes(App.DbCaminho);

            lvwReceita.ItemsSource = swtFavorito.IsToggled ? db.Localizar(t, true) : db.Localizar(t);
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

        private void swtFavorito_Toggled(object sender, ToggledEventArgs e)
        {
            atualizaLista();
        }

        private void btnPesquisar_Clicked(object sender, EventArgs e)
        {
            atualizaLista();
        }
        
        private void lvwReceita_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ModReceitas r = (ModReceitas)lvwReceita.SelectedItem;

            FlyoutPage fp = (FlyoutPage)Application.Current.MainPage;
            fp.Detail = new NavigationPage(new PagInserir(r));
        }
    }
}