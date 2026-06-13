using Xamarin.Forms;

namespace zd5_2_Laptev
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // запускаем карусель-страницу как главную
            MainPage = new MainCarousel();
        }

        protected override void OnStart() { }
        protected override void OnSleep() { }
        protected override void OnResume() { }
    }
}