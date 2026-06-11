using System;
using Xamarin.Forms;

namespace zd4_Laptev_Nikita.Views
{
    public partial class CurrencyPage : ContentPage
    {
        public CurrencyPage()
        {
            InitializeComponent();
            datePicker.Date = DateTime.Today; //текущая дата
        }
    }
}