using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using zd4_Laptev_Nikita.Services;
using zd4_Laptev_Nikita.Views;

namespace zd4_Laptev_Nikita
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            /*DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();*/
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
