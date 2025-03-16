using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Recipes.Views;

namespace Recipes
{
    public partial class App : Application
    {
        public static String DbNome;
        public static String DbCaminho;

        public App ()
        {
            InitializeComponent();

            MainPage = new PagMain();
        }

        public App (string dbNome, string dbCaminho)
        {
            InitializeComponent();
            DbNome = dbNome;
            DbCaminho = dbCaminho;
            MainPage = new PagMain();
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}
