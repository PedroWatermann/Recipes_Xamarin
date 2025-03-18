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

            bool f = false;
            if (imgFavorito.Source is FileImageSource fileSource && fileSource.File == "star_white_filled.png")
            {
                f = true;
            }

            var listaReceitas = f ? db.Localizar(t, true) : db.Localizar(t);

            lvwReceita.ItemsSource = listaReceitas;

            if (listaReceitas != null && listaReceitas.Any())
            {
                lblReceita.IsVisible = false;
                frmReceita.IsVisible = false;

                frmBorda.IsVisible = true;
                frmLista.IsVisible = true;
            }
            else
            {
                lblReceita.IsVisible = true;
                frmReceita.IsVisible = true;

                frmBorda.IsVisible = false;
                frmLista.IsVisible = false;
            }
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
        
        private void lvwReceita_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ModReceitas r = (ModReceitas)lvwReceita.SelectedItem;

            FlyoutPage fp = (FlyoutPage)Application.Current.MainPage;
            fp.Detail = new NavigationPage(new PagInserir(r))
            {
                BarBackgroundColor = Color.FromHex("#C85400")
            };
            fp.IsPresented = false;
        }

        private void entTitulo_TextChanged(object sender, TextChangedEventArgs e)
        {
            atualizaLista();
        }

        private void tapImgFavorito_Tapped(object sender, EventArgs e)
        {
            if (imgFavorito.Source is FileImageSource fileSource && fileSource.File == "star_white.png")
            {
                imgFavorito.Source = "star_white_filled.png";
                atualizaLista();
            }
            else
            {
                imgFavorito.Source = "star_white.png";
                atualizaLista();
            }
        }
    }
}