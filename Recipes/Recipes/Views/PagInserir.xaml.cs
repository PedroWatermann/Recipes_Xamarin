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
	public partial class PagInserir : ContentPage
	{
		public PagInserir ()
		{
			InitializeComponent ();

            btnSalvar.IsVisible = true;
            btnEditar.IsVisible = false;
            btnExcluir.IsVisible = false;
		}

        public PagInserir(ModReceitas r)
        {
            InitializeComponent();

            btnSalvar.IsVisible = false;
            btnEditar.IsVisible = true;
            btnExcluir.IsVisible = true;

            lblId.Text = r.id.ToString();
            entTitulo.Text = r.titulo.ToString();
            string[] i = r.ingredientes.Split('@');
            foreach (string s in i)
            {
                edtIngrediente.Text += s;
                edtIngrediente.Text += '\n';
            }
            entLink.Text = r.link.ToString();
            swtFavorito.IsToggled = r.favorito;
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
            imgFavorito.Source = swtFavorito.IsToggled ? "star_white_filled.png" : "star_white.png";
        }

        private async void btnSalvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                string i = edtIngrediente.Text.Replace('\n', '@');

                ModReceitas r = new ModReceitas()
                {
                    titulo = entTitulo.Text,
                    ingredientes = i,
                    link = entLink.Text,
                    favorito = swtFavorito.IsToggled
                };

                SerDbRecipes db = new SerDbRecipes(App.DbCaminho);
                db.Inserir(r);

                await DisplayAlert("Adição", db.MensagemStatus, "OK");

                FlyoutPage fp = (FlyoutPage)Application.Current.MainPage;
                fp.Detail = new NavigationPage(new PagHome());
            }
            catch (Exception ex)
            {
                throw new Exception("Erro: ", ex);
            }
        }

        private async void btnEditar_Clicked(object sender, EventArgs e)
        {
            try
            {
                string i = edtIngrediente.Text.Replace('\n', '@');

                ModReceitas r = new ModReceitas()
                {
                    id = Convert.ToInt32(lblId.Text),
                    titulo = entTitulo.Text,
                    ingredientes = i,
                    link = entLink.Text,
                    favorito = swtFavorito.IsToggled
                };

                SerDbRecipes db = new SerDbRecipes(App.DbCaminho);
                if (!await db.Alterar(r, this))
                    return;

                await DisplayAlert("Atualização", db.MensagemStatus, "OK");

                FlyoutPage fp = (FlyoutPage)Application.Current.MainPage;
                fp.Detail = new NavigationPage(new PagHome());
            }
            catch (Exception ex)
            {
                throw new Exception("Erro: ", ex);
            }

        }

        private async void btnExcluir_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Excluir", "Deseja realmete excluír esta receita?", "Sim", "Não"))
            {
                int id = Convert.ToInt32(lblId.Text);

                new SerDbRecipes(App.DbCaminho).Excluir(id);

                await DisplayAlert("Exclusão", "Receita excluída com sucesso!", "OK");

                FlyoutPage fp = (FlyoutPage)Application.Current.MainPage;
                fp.Detail = new NavigationPage(new PagHome());
            }
        }
    }
}