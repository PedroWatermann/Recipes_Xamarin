using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Recipes.Views;

namespace Recipes
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new PagPrincipal();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
