using System;
using Xamarin.Forms;

namespace zd4_Laptev_Nikita.Views
{
    public partial class CalculatorPage : ContentPage
    {
        public CalculatorPage()
        {
            InitializeComponent();
            typePicker.SelectedIndex = 0; //по умолчанию аннуитетный
        }

        private void OnRateChanged(object sender, ValueChangedEventArgs e)
        {
            int rate = (int)Math.Round(e.NewValue);
            rateLabel.Text = $"{rate}%";
        }

        private void OnTypeChanged(object sender, EventArgs e)
        {
            if (monthlyLabel == null) return;
            //ежемесячный платёж виден только для аннуитетного
            monthlyLabel.IsVisible = (typePicker.SelectedIndex == 0);
        }

        private void OnCalculateClicked(object sender, EventArgs e)
        {
            if (!double.TryParse(sumEntry.Text, out double sum) || sum <= 0)
            {
                DisplayAlert("Ошибка", "Введите сумму кредита", "OK");
                return;
            }

            if (!int.TryParse(termEntry.Text, out int months) || months <= 0)
            {
                DisplayAlert("Ошибка", "Введите срок в месяцах", "OK");
                return;
            }

            double annualRate = Math.Round(rateSlider.Value);
            double monthlyRate = annualRate / 100 / 12;

            double totalPayment;
            double monthlyPayment;

            if (typePicker.SelectedIndex == 0)
            {
                //аннуитетный
                double pow = Math.Pow(1 + monthlyRate, months);
                monthlyPayment = sum * (monthlyRate * pow) / (pow - 1);
                totalPayment = monthlyPayment * months;

                monthlyLabel.IsVisible = true;
                monthlyLabel.Text = $"Ежемесячный платеж: {monthlyPayment:F2}";
            }
            else
            {
                //дифференцированный
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