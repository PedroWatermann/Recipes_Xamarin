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
	public partial class PagHome : ContentPage
	{
		public PagHome ()
		{
			InitializeComponent ();
		}

        private void tapImgAdicionar_Tapped(object sender, EventArgs e)
        {
			FlyoutPage fp = (FlyoutPage)Application.Current.MainPage;
			fp.Detail = new NavigationPage(new PagInserir())
            {
                BarBackgroundColor = Color.FromHex("#C85400")
            };
			fp.IsPresented = false;
        }

        private void tapImgPesquisar_Tapped(object sender, EventArgs e)
        {
            FlyoutPage fp = (FlyoutPage)Application.Current.MainPage;
            fp.Detail = new NavigationPage(new PagLocalizar())
            {
                BarBackgroundColor = Color.FromHex("#C85400")
            };
            fp.IsPresented = false;
        }
    }
}