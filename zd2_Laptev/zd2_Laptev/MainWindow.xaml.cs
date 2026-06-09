using System;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Windows;

namespace zd2_Laptev
{
    public partial class MainWindow : Window
    {
        // телефонная книга
        private PhoneBook phoneBook = new PhoneBook();

        public MainWindow()
        {
            InitializeComponent();
        }

        // обновление списка всеми контактами
        private void RefreshList()
        {
            listBoxContacts.Items.Clear();

            foreach (Contact c in phoneBook.GetAll())
            {
                listBoxContacts.Items.Add(c.ToString());
            }
        }

        // вывод произвольного списка контактов
        private void ShowContacts(List<Contact> list)
        {
            listBoxContacts.Items.Clear();

            foreach (Contact c in list)
            {
                listBoxContacts.Items.Add(c.ToString());
            }
        }

        // проверка корректности имени
        private bool IsValidName(string name)
        {
            // имя не пустое
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Имя не может быть пустым!");
                return false;
            }

            // длина имени от 2 до 30 символов
            if (name.Trim().Length < 2 || name.Trim().Length > 30)
            {
                MessageBox.Show("Имя должно быть от 2 до 30 символов!");
                return false;
            }

            // имя не должно содержать цифр
            foreach (char ch in name)
            {
                if (char.IsDigit(ch))
                {
                    MessageBox.Show("Имя не должно содержать цифр!");
                    return false;
                }
            }

            return true;
        }

        // проверка корректности телефона
        private bool IsValidPhone(string phone)
        {
            // телефон не пустой
            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Телефон не может быть пустым!");
                return false;
            }

            // проверяем каждый символ
            for (int i = 0; i < phone.Length; i++)
            {
                char ch = phone[i];

                // плюс разрешён только как первый символ
                if (ch == '+')
                {
                    if (i != 0)
                    {
                        MessageBox.Show("Плюс допустим только в начале номера!");
                        return false;
                    }

                    continue;
                }

                // все остальные символы должны быть цифрами
                if (!char.IsDigit(ch))
                {
                    MessageBox.Show("Телефон может содержать только цифры и + в начале!");
                    return false;
                }
            }

            // считаем количество цифр без плюса
            string digits = phone;
            if (digits.StartsWith("+"))
                digits = digits.Substring(1);

            try
            {
                // проверяем что это число
                long number = long.Parse(digits);

                // длина от 5 до 15 цифр
                if (digits.Length < 5 || digits.Length > 15)
                {
                    MessageBox.Show("Телефон должен содержать от 5 до 15 цифр!");
                    return false;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Телефон должен содержать цифры!");
                return false;
            }
            catch (OverflowException)
            {
                MessageBox.Show("Телефон слишком длинный!");
                return false;
            }

            return true;
        }

        // открытие файла
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "CSV файлы (*.csv)|*.csv|Все файлы (*.*)|*.*";

            if (dlg.ShowDialog() == true)
            {
                try
                {
                    PhoneBookLoader.Load(phoneBook, dlg.FileName);
                    RefreshList();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Неверный формат данных в файле!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка открытия: " + ex.Message);
                }
            }
        }

        // сохранение в файл
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // проверяем что есть что сохранять
            if (phoneBook.Count == 0)
            {
                MessageBox.Show("Список контактов пуст!");
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "CSV файлы (*.csv)|*.csv|Все файлы (*.*)|*.*";

            if (dlg.ShowDialog() == true)
            {
                try
                {
                    PhoneBookLoader.Save(phoneBook, dlg.FileName);
                    MessageBox.Show("Файл сохранён!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка сохранения: " + ex.Message);
                }
            }
        }

        // выход
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // показать все контакты
        private void ShowAll_Click(object sender, RoutedEventArgs e)
        {
            // проверяем что книга не пуста
            if (phoneBook.Count == 0)
            {
                MessageBox.Show("Список контактов пуст!");
                return;
            }

            RefreshList();
        }

        // поиск по имени
        private void Find_Click(object sender, RoutedEventArgs e)
        {
            // проверка имени
            if (!IsValidName(txtName.Text))
                return;

            List<Contact> found = phoneBook.Find(txtName.Text);

            if (found.Count == 0)
            {
                listBoxContacts.Items.Clear();
                MessageBox.Show("Ничего не найдено!");
            }
            else
            {
                ShowContacts(found);
            }
        }

        // сортировка по имени
        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            // проверяем что книга не пуста
            if (phoneBook.Count == 0)
            {
                MessageBox.Show("Список контактов пуст!");
                return;
            }

            List<Contact> sorted = phoneBook.SortByName();
            ShowContacts(sorted);
        }

        // добавление по объекту
        private void AddObject_Click(object sender, RoutedEventArgs e)
        {
            // проверки имени и телефона
            if (!IsValidName(txtName.Text))
                return;

            if (!IsValidPhone(txtPhone.Text))
                return;

            try
            {
                // перегрузка Add(Contact)
                Contact c = new Contact(txtName.Text.Trim(), txtPhone.Text.Trim());
                phoneBook.Add(c);

                ClearInputs();
                RefreshList();
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный формат данных!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления: " + ex.Message);
            }
        }

        // добавление по полям (перегрузка)
        private void AddFields_Click(object sender, RoutedEventArgs e)
        {
            // проверки имени и телефона
            if (!IsValidName(txtName.Text))
                return;

            if (!IsValidPhone(txtPhone.Text))
                return;

            try
            {
                // перегрузка Add(string, string)
                phoneBook.Add(txtName.Text.Trim(), txtPhone.Text.Trim());

                ClearInputs();
                RefreshList();
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный формат данных!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления: " + ex.Message);
            }
        }

        // удаление по индексу
        private void RemoveIndex_Click(object sender, RoutedEventArgs e)
        {
            // проверяем выбран ли контакт
            if (listBoxContacts.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите контакт в списке!");
                return;
            }

            try
            {
                // перегрузка Remove(int)
                phoneBook.Remove(listBoxContacts.SelectedIndex);
                RefreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления: " + ex.Message);
            }
        }

        // удаление по имени (перегрузка)
        private void RemoveName_Click(object sender, RoutedEventArgs e)
        {
            // проверка имени
            if (!IsValidName(txtName.Text))
                return;

            try
            {
                // перегрузка Remove(string)
                phoneBook.Remove(txtName.Text);

                ClearInputs();
                RefreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления: " + ex.Message);
            }
        }

        // очистка полей ввода
        private void ClearInputs()
        {
            txtName.Clear();
            txtPhone.Clear();
        }
    }
}