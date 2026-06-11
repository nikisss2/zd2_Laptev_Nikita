using System;
using Xamarin.Forms;

namespace zd4_Laptev_Nikita.Views
{
    public partial class CalculatorPage : ContentPage
    {
        // Конструктор по умолчанию (на всякий случай)
        public CalculatorPage()
        {
            InitializeComponent();
            typePicker.SelectedIndex = 0;
        }

        // Конструктор с передачей фамилии (пункт №5)
        public CalculatorPage(string username) : this()
        {
            greetingLabel.Text = $"Здравствуйте, {username}!";
        }

        private void OnRateChanged(object sender, ValueChangedEventArgs e)
        {
            int rate = (int)Math.Round(e.NewValue);
            rateLabel.Text = $"{rate}%";
        }

        private void OnTypeChanged(object sender, EventArgs e)
        {
            if (monthlyLabel == null) return;
            monthlyLabel.IsVisible = (typePicker.SelectedIndex == 0);
        }

        private void OnCalculateClicked(object sender, EventArgs e)
        {
            // Проверки на входные данные
            if (!double.TryParse(sumEntry.Text, out double sum) || sum <= 0)
            {
                DisplayAlert("Ошибка", "Введите корректную сумму кредита", "OK");
                return;
            }

            if (!int.TryParse(termEntry.Text, out int months) || months <= 0)
            {
                DisplayAlert("Ошибка", "Введите корректный срок в месяцах", "OK");
                return;
            }

            double annualRate = Math.Round(rateSlider.Value);
            if (annualRate <= 0)
            {
                DisplayAlert("Ошибка", "Установите процентную ставку больше 0", "OK");
                return;
            }

            double monthlyRate = annualRate / 100 / 12;
            double totalPayment;
            double monthlyPayment;

            if (typePicker.SelectedIndex == 0)
            {
                double pow = Math.Pow(1 + monthlyRate, months);
                monthlyPayment = sum * (monthlyRate * pow) / (pow - 1);
                totalPayment = monthlyPayment * months;

                monthlyLabel.IsVisible = true;
                monthlyLabel.Text = $"Ежемесячный платеж: {monthlyPayment:F2}";
            }
            else
            {
                double mainPart = sum / months;
                totalPayment = 0;
                for (int m = 0; m < months; m++)
                {
                    double balance = sum - mainPart * m;
                    totalPayment += mainPart + balance * monthlyRate;
                }
                monthlyLabel.IsVisible = false;
            }

            double overpay = totalPayment - sum;
            totalLabel.Text = $"Общая сумма: {totalPayment:F2}";
            overpayLabel.Text = $"Переплата: {overpay:F2}";
        }
    }
}