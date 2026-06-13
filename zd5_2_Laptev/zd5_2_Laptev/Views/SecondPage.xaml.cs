using Xamarin.Forms;

namespace zd5_2_Laptev.Views
{
    public partial class SecondPage : ContentPage
    {
        public SecondPage()
        {
            InitializeComponent();
        }

        // получаем и отображаем фамилию пользователя
        public void SetSurname(string surname)
        {
            surnameLabel.Text = surname;
        }
    }
}