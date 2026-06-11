using Xamarin.Forms;
using zd4_Laptev_Nikita.Views;

namespace zd4_Laptev_Nikita
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Навигация через NavigationPage, стартовый экран — Welcome
            MainPage = new NavigationPage(new WelcomePage());
        }

        protected override void OnStart() { }
        protected override void OnSleep() { }
        protected override void OnResume() { }
    }
}