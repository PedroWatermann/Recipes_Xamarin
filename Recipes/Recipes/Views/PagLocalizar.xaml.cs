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
            // Captura o valor de título, categoria e favorito
            string t = string.IsNullOrWhiteSpace(entTitulo.Text) ? "" : entTitulo.Text;
            string c = pckCategoria.SelectedIndex >= 0 ? pckCategoria.SelectedItem.ToString() : "";
            bool f = imgFavorito.Source is FileImageSource fileSource && fileSource.File == "star_white_filled.png";

            // Realiza a busca e exibe a lista
            SerDbRecipes db = new SerDbRecipes(App.DbCaminho);
            List<ModReceitas> listaReceitas = f ? db.Localizar(t, c, true) : db.Localizar(t, c);
            lvwReceita.ItemsSource = listaReceitas;

            // Exibe/esconde mensagem caso não haja/haja receitas cadastradas
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
            // Altera a imagem com base no click
            imgFavorito.Source = imgFavorito.Source is FileImageSource fileSource && fileSource.File == "star_white.png" ? "star_white_filled.png" : "star_white.png";
            atualizaLista();
        }

        private void tapImgCategoria_Tapped(object sender, EventArgs e)
        {
            pckCategoria.SelectedIndex = -1;
            imgCategoria.Opacity = 0;
            imgCategoria.IsEnabled = false;
            Grid.SetColumnSpan(pckCategoria, 2);
            atualizaLista();
        }

        private void pckCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            imgCategoria.Opacity = 1;
            imgCategoria.IsEnabled = true;
            Grid.SetColumnSpan(pckCategoria, 1);
            atualizaLista();
        }
    }
}