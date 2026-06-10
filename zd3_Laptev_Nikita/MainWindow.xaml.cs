using System.Collections.Generic;
using System.Windows;
using zd3_Laptev_Nikita;
using Cables;


namespace zd3_Laptev_Nikita
{
    public partial class MainWindow : Window
    {
        //хранилище кабелей
        private CableStorage storage = new CableStorage();

        public MainWindow()
        {
            InitializeComponent();
            RefreshList();
        }

        //обновление списка на экране
        private void RefreshList()
        {
            lstCables.ItemsSource = null;
            lstCables.ItemsSource = storage.GetAll();
        }

        //чтение базовых полей с проверкой
        private bool ReadBaseFields(out string type, out int cores, out double diameter,
            out string manufacturer, out double price)
        {
            type = txtType.Text.Trim();
            manufacturer = txtManufacturer.Text.Trim();
            cores = 0;
            diameter = 0;
            price = 0;

            if (string.IsNullOrEmpty(type))
            {
                MessageBox.Show("Введите тип кабеля.");
                return false;
            }

            if (!int.TryParse(txtCores.Text.Trim(), out cores) || cores <= 0)
            {
                MessageBox.Show("Неверное количество жил.");
                return false;
            }

            if (!double.TryParse(txtDiameter.Text.Trim(), out diameter) || diameter <= 0)
            {
                MessageBox.Show("Неверный диаметр.");
                return false;
            }

            if (!double.TryParse(txtPrice.Text.Trim(), out price) || price < 0)
            {
                MessageBox.Show("Неверная цена.");
                return false;
            }

            return true;
        }

        //очистка полей ввода
        private void ClearInputs()
        {
            txtType.Clear();
            txtCores.Clear();
            txtDiameter.Clear();
            txtManufacturer.Clear();
            txtPrice.Clear();
            txtShieldMaterial.Clear();
            chkShield.IsChecked = false;
            chkFlexible.IsChecked = false;
        }

        //добавление обычного кабеля
        private void AddBase_Click(object sender, RoutedEventArgs e)
        {
            string type, manufacturer;
            int cores;
            double diameter, price;

            if (!ReadBaseFields(out type, out cores, out diameter, out manufacturer, out price))
                return;

            //перегрузка добавления по полям базового
            storage.Add(type, cores, diameter, manufacturer, price);
            ClearInputs();
            RefreshList();
            txtStatus.Text = "Добавлен обычный кабель.";
        }

        //добавление экранированного кабеля
        private void AddChild_Click(object sender, RoutedEventArgs e)
        {
            string type, manufacturer;
            int cores;
            double diameter, price;

            if (!ReadBaseFields(out type, out cores, out diameter, out manufacturer, out price))
                return;

            bool hasShield = chkShield.IsChecked == true;
            bool isFlexible = chkFlexible.IsChecked == true;
            string shieldMaterial = txtShieldMaterial.Text.Trim();

            //перегрузка добавления по полям потомка
            storage.Add(type, cores, diameter, manufacturer, price, hasShield, shieldMaterial, isFlexible);
            ClearInputs();
            RefreshList();
            txtStatus.Text = "Добавлен экранированный кабель.";
        }

        //удаление выбранного по индексу
        private void RemoveByIndex_Click(object sender, RoutedEventArgs e)
        {
            int index = lstCables.SelectedIndex;

            if (index < 0)
            {
                MessageBox.Show("Выберите кабель в списке.");
                return;
            }

            //основной метод удаления по индексу
            storage.Remove(index);
            RefreshList();
            txtStatus.Text = "Кабель удалён по индексу.";
        }

        //удаление по типу
        private void RemoveByType_Click(object sender, RoutedEventArgs e)
        {
            string type = txtRemoveType.Text.Trim();

            if (string.IsNullOrEmpty(type))
            {
                MessageBox.Show("Введите тип для удаления.");
                return;
            }

            //перегрузка удаления по типу
            storage.Remove(type);
            txtRemoveType.Clear();
            RefreshList();
            txtStatus.Text = "Кабель удалён по типу.";
        }

        //сортировка по качеству
        private void SortByQuality_Click(object sender, RoutedEventArgs e)
        {
            lstCables.ItemsSource = null;
            lstCables.ItemsSource = storage.SortByQuality();
            txtStatus.Text = "Отсортировано по качеству.";
        }

        //только экранированные
        private void OnlyShielded_Click(object sender, RoutedEventArgs e)
        {
            lstCables.ItemsSource = null;
            lstCables.ItemsSource = storage.OnlyShielded();
            txtStatus.Text = "Показаны только экранированные.";
        }

        //показать все
        private void ShowAll_Click(object sender, RoutedEventArgs e)
        {
            RefreshList();
            txtStatus.Text = "Показаны все кабели.";
        }

        //средняя цена
        private void AveragePrice_Click(object sender, RoutedEventArgs e)
        {
            double avg = storage.AveragePrice();
            txtStatus.Text = "Средняя цена: " + avg.ToString("F2");
        }
    }
}