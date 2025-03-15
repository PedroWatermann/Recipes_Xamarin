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
    public partial class PagPrincipal : FlyoutPage
    {
        public PagPrincipal()
        {
            InitializeComponent();

            btnHome_Clicked(new Object(), new EventArgs());
        }

        private void btnHome_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new PagHome());
            IsPresented = false;
        }

        private void btnInserir_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new PagInserir());
            IsPresented = false;
        }

        private void btnLocalizar_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new PagLocalizar());
            IsPresented = false;
        }

        private void btnSobre_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new PagSobre());
            IsPresented = false;
        }
    }
}