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
            sklBotoes.IsVisible = false;
        }

        public PagInserir(ModReceitas r)
        {
            InitializeComponent();

            // Exibe os botões de edição e exclusão
            btnSalvar.IsVisible = false;
            btnEditar.IsVisible = true;
            btnExcluir.IsVisible = true;
            sklBotoes.IsVisible = true;

            // Preenche com as informações da receita
            lblId.Text = r.id.ToString();
            pckCategoria.SelectedItem = r.categoria;
            entTitulo.Text = r.titulo.ToString();
            if (string.IsNullOrEmpty(r.ingredientes))
            {
                edtIngrediente.Text = string.Empty;
            }
            else
            {
                string[] i = r.ingredientes.Split('@');
                foreach (string s in i)
                {
                    edtIngrediente.Text += s + '\n';
                }
            }
            if (string.IsNullOrEmpty(r.modoPreparo))
            {
                edtModoPreparo.Text = string.Empty;
            }
            else 
            { 
                string[] m = r.modoPreparo.Split('@');
                foreach (string s in m)
                {
                    edtModoPreparo.Text += s + '\n';
                }
            }
            entLink.Text = r.link.ToString();
            imgFavorito.Source = r.favorito ? "star_white_filled.png" : "star_white";
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

        private async void btnSalvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Obtém os valore de ingredientes, modo de preparo e favorito
                string i = string.IsNullOrEmpty(edtIngrediente.Text) ? "" : edtIngrediente.Text.Replace('\n', '@');
                string m = string.IsNullOrEmpty(edtModoPreparo.Text) ? "" : edtModoPreparo.Text.Replace('\n', '@');
                bool f = imgFavorito.Source is FileImageSource fileSource && fileSource.File == "star_white_filled.png";

                if (string.IsNullOrWhiteSpace(entTitulo.Text))
                {
                    await DisplayAlert("Erro", "O campo 'Título' não pode estar vazio!", "OK");
                    return;
                }

                if (pckCategoria.SelectedItem == null)
                {
                    await DisplayAlert("Erro", "Selecione uma categoria!", "OK");
                    return;
                }

                // Verifica campos vazios e avisa o usuário
                List<string> vazios = new List<string>();
                if (i == "")
                    vazios.Add("'ingredientes'");
                if (m == "")
                    vazios.Add("'modo de preparo'");
                if (entLink.Text == "")
                    vazios.Add("'link'");
                if (vazios.Count > 0)
                {
                    bool res = await DisplayAlert("Campos Vazios", string.Format("\n{0} está(ão) vazio(s)!\n\nDeseja continuar?", string.Join(", ", vazios)), "Sim", "Não");
                    if (!res)
                    {
                        return;
                    }
                }

                // Executa a inserção
                ModReceitas r = new ModReceitas()
                {
                    titulo = entTitulo.Text,
                    categoria = pckCategoria.SelectedItem?.ToString() ?? "",
                    ingredientes = i,
                    modoPreparo = m,
                    link = entLink.Text,
                    favorito = f
                };
                SerDbRecipes db = new SerDbRecipes(App.DbCaminho);
                db.Inserir(r);

                await DisplayAlert("Adição", db.MensagemStatus, "OK");

                FlyoutPage fp = (FlyoutPage)Application.Current.MainPage;
                fp.Detail = new NavigationPage(new PagHome());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async void btnEditar_Clicked(object sender, EventArgs e)
        {
            try
            {
                string i = string.IsNullOrEmpty(edtIngrediente.Text) ? "" : edtIngrediente.Text.Replace('\n', '@');
                string m = string.IsNullOrEmpty(edtModoPreparo.Text) ? "" : edtModoPreparo.Text.Replace('\n', '@');
                bool f = imgFavorito.Source is FileImageSource fileSource && fileSource.File == "star_white_filled.png";

                // Verifica campos vazios e avisa o usuário
                List<string> vazios = new List<string>();
                if (i == "")
                    vazios.Add("'ingredientes'");
                if (m == "")
                    vazios.Add("'modo de preparo'");
                if (entLink.Text == "")
                    vazios.Add("'link'");
                if (vazios.Count > 0)
                {
                    if (!await DisplayAlert("Campos Vazios", string.Format("\n{0} está(ão) vazio(s)!\n\nDeseja continuar?", string.Join(", ", vazios)), "Sim", "Não"))
                    {
                        return;
                    }
                }

                ModReceitas r = new ModReceitas()
                {
                    id = Convert.ToInt32(lblId.Text),
                    titulo = entTitulo.Text,
                    categoria = pckCategoria.SelectedItem.ToString(),
                    ingredientes = i,
                    modoPreparo = m,
                    link = entLink.Text,
                    favorito = f
                };

                SerDbRecipes db = new SerDbRecipes(App.DbCaminho);
                db.Alterar(r, this);

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
            if (await DisplayAlert("Excluir", "Deseja realmete excluir esta receita?", "Sim", "Não"))
            {
                int id = Convert.ToInt32(lblId.Text);

                new SerDbRecipes(App.DbCaminho).Excluir(id);

                await DisplayAlert("Exclusão", "Receita excluída com sucesso!", "OK");

                FlyoutPage fp = (FlyoutPage)Application.Current.MainPage;
                fp.Detail = new NavigationPage(new PagHome());
            }
        }

        private void tapImgFavorito_Tapped(object sender, EventArgs e)
        {
            imgFavorito.Source = imgFavorito.Source is FileImageSource fileSource && fileSource.File == "star_white.png" ? "star_white_filled.png" : imgFavorito.Source = "star_white.png";
        }
    }
}