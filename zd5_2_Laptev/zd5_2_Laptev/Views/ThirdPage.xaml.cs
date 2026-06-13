using Xamarin.Forms;

namespace zd5_2_Laptev.Views
{
    public partial class ThirdPage : ContentPage
    {
        public ThirdPage()
        {
            InitializeComponent();
        }

        // обработчик нажатия button
        private async void OnButtonClicked(object sender, System.EventArgs e)
        {
            // проверка что опция выбрана
            if (optionPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Ошибка", "Выберите опцию из списка", "OK");
                return;
            }

            // получаем выбранное значение из dropdown
            string selected = optionPicker.SelectedItem.ToString();

            // расшифровка информации по выбранной опции
            string info = GetDescription(selected);

            // максимальное значение из слайдера
            double max = valueSlider.Maximum;

            // показываем значение из списка с расшифровкой и макс. значением слайдера
            resultLabel.Text =
                $"Выбрано: {selected}\n" +
                $"Расшифровка: {info}\n" +
                $"Максимум слайдера: {max}";
        }

        // возвращает расшифровку для выбранной опции
        private string GetDescription(string option)
        {
            switch (option)
            {
                case "OPTION 1":
                    return "Первый вариант — базовый режим";
                case "OPTION 2":
                    return "Второй вариант - расширенный режим";
                case "OPTION 3":
                    return "Третий вариант — премиум режим";
                default:
                    return "Нет данных";
            }
        }
    }
}