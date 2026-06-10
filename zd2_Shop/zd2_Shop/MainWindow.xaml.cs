using System;
using System.Windows;

namespace zd2_Shop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // объект магазина
        private Shop shop;

        // конструктор окна
        public MainWindow()
        {
            InitializeComponent();
            shop = new Shop();

            // стартовые товары
            shop.CreateProduct("Кола", 85, 200);
            shop.CreateProduct("Сок", 100, 50);

            RefreshList();
        }

        // обновить список и прибыль на экране
        private void RefreshList()
        {
            ProductsList.Items.Clear();
            foreach (Product product in shop.Products)
            {
                ProductsList.Items.Add(product);
            }
            ProfitText.Text = $"Прибыль магазина: {shop.Profit} руб.";
        }

        // пункт меню "добавить товар"
        private void AddMenu_Click(object sender, RoutedEventArgs e)
        {
            // проверяем название на пустоту
            if (string.IsNullOrWhiteSpace(NameBox.Text))
            {
                MessageBox.Show("Введите название товара!");
                return;
            }

            // проверяем что такого товара ещё нет
            if (shop.FindByName(NameBox.Text) != null)
            {
                MessageBox.Show("Такой товар уже существует!");
                return;
            }

            // проверяем цену
            decimal price = 0;
            try
            {
                price = decimal.Parse(PriceBox.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Некорректный ввод.");
                return;
            }

            // цена должна быть больше 0
            if (price <= 0)
            {
                MessageBox.Show("Цена должна быть больше нуля!");
                return;
            }

            // проверяем количество
            int count = 0;
            try
            {
                count = int.Parse(CountBox.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Некорректный ввод.");
                return;
            }

            // количество должно быть больше 0
            if (count <= 0)
            {
                MessageBox.Show("Количество должно быть больше нуля!");
                return;
            }

            // добавляем товар в магазин
            shop.CreateProduct(NameBox.Text, price, count);
            RefreshList();

            // очищаем поля ввода
            NameBox.Clear();
            PriceBox.Clear();
            CountBox.Clear();
        }

        // пункт меню "продать товар"
        private void SellMenu_Click(object sender, RoutedEventArgs e)
        {
            // проверяем количество для продажи
            int count = 0;
            try
            {
                count = int.Parse(SellCountBox.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Некорректный ввод.");
                return;
            }

            // количество должно быть больше 0
            if (count <= 0)
            {
                MessageBox.Show("Количество должно быть больше нуля!");
                return;
            }

            string result;
            Product selected = null; // товар, который продаём

            // ЕСЛИ введено название в поле продаём ПО ИМЕНИ
            if (!string.IsNullOrWhiteSpace(NameBox.Text))
            {
                selected = shop.FindByName(NameBox.Text);
                result = shop.Sell(NameBox.Text, count);
            }
            // ИНАЧЕ продаём по выбранному элементу из списка
            else if (ProductsList.SelectedItem != null)
            {
                selected = (Product)ProductsList.SelectedItem;
                result = shop.Sell(selected, count);
            }
            else
            {
                MessageBox.Show("Введите название товара или выберите его из списка!");
                return;
            }

            // если товар закончился то удаляем из списка (для обоих случаев)
            if (selected != null && selected.Count <= 0)
            {
                shop.Products.Remove(selected);
            }

            MessageBox.Show(result);
            RefreshList();
        }

        // пункт меню "отчёт"
        private void ReportMenu_Click(object sender, RoutedEventArgs e)
        {
            // выводим в messagebox сколько заработал магазин
            MessageBox.Show($"Магазин заработал: {shop.Profit} руб.", "Отчёт");
        }
    }
}