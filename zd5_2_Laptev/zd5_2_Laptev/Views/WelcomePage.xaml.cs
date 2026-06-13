using Xamarin.Forms;

namespace zd5_2_Laptev.Views
{
    public partial class WelcomePage : ContentPage
    {
        private MainCarousel _carousel;   // ссылка на карусель
        private SecondPage _secondPage;   // ссылка на второй экран

        public WelcomePage()
        {
            InitializeComponent();
        }

        //инициализация с каруселью и вторым экраном
        public void Init(MainCarousel carousel, SecondPage secondPage)
        {
            _carousel = carousel;
            _secondPage = secondPage;
        }

        //нажатие sign in
        private async void OnSignInClicked(object sender, System.EventArgs e)
        {
            // проверка на заполнение полей
            if (string.IsNullOrWhiteSpace(usernameEntry.Text))
            {
                await DisplayAlert("Ошибка", "Введите фамилию (Username)", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(passwordEntry.Text))
            {
                await DisplayAlert("Ошибка", "Введите пароль", "OK");
                return;
            }

            // передаём фамилию на второй экран
            _secondPage.SetSurname(usernameEntry.Text);

            // переход на второй экран
            _carousel.GoTo(_secondPage);
        }
    }
}