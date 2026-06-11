using System;
using Xamarin.Forms;

namespace zd4_Laptev_Nikita.Views
{
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        private async void OnSignInClicked(object sender, EventArgs e)
        {
            //заполнение полей
            if (string.IsNullOrWhiteSpace(usernameEntry.Text))
            {
                await DisplayAlert("Ошибка", "Введите имя пользователя (Username)", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(passwordEntry.Text))
            {
                await DisplayAlert("Ошибка", "Введите пароль (Password)", "OK");
                return;
            }

            //переход с передачей фамилии на второй экран
            await Navigation.PushAsync(new CalculatorPage(usernameEntry.Text));
        }
    }
}