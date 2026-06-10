using System;
using System.Windows;

namespace zd3_Laptev_Nikita
{
    public partial class MainWindow : Window
    {
        Playlist playlist = new Playlist();

        public MainWindow()
        {
            InitializeComponent();
            RefreshList();
        }


        // обновление списка
        private void RefreshList()
        {
            lstSongs.Items.Clear();
            for (int i = 0; i < playlist.Count; i++)
                lstSongs.Items.Add($"{i}: {playlist.Songs[i]}");

            if (playlist.Count > 0)
            {
                lstSongs.SelectedIndex = playlist.CurrentIndex;
                lblCurrent.Text = "Текущая: " + playlist.CurrentSong();
            }
            else
            {
                lblCurrent.Text = "Текущая: ";
            }
        }

        // добавление по полям
        private void AddByFields_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtAuthor.Text) ||
                    string.IsNullOrWhiteSpace(txtTitle.Text) ||
                    string.IsNullOrWhiteSpace(txtFilename.Text))
                {
                    MessageBox.Show("заполните все поля!");
                    return;
                }
                Song s = new Song(txtAuthor.Text, txtTitle.Text, txtFilename.Text);
                if (playlist.Contains(s)) { MessageBox.Show("Этот трек уже содержится в плейлисте"); return; }

                playlist.Add(txtAuthor.Text, txtTitle.Text, txtFilename.Text);
                RefreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ошибка: " + ex.Message);
            }
        }

        // добавление по Song
        private void AddBySong_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtAuthor.Text) ||
                    string.IsNullOrWhiteSpace(txtTitle.Text) ||
                    string.IsNullOrWhiteSpace(txtFilename.Text))
                {
                    MessageBox.Show("заполните все поля!");
                    return;
                }

                Song s = new Song(txtAuthor.Text, txtTitle.Text, txtFilename.Text);

                if (playlist.Contains(s)) { MessageBox.Show("Этот трек уже содержится в плейлисте"); return; }
                playlist.Add(s);
                RefreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ошибка: " + ex.Message);
            }
        }

        // следующая
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                playlist.Next();
                RefreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // предыдущая
        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                playlist.Previous();
                RefreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // текущая
        private void Current_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Song s = playlist.CurrentSong();
                MessageBox.Show("сейчас играет:\n" + s);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // в начало
        private void ToStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                playlist.ToStart();
                RefreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // переход по индексу
        private void JumpTo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtIndex.Text == string.Empty)
                {
                    MessageBox.Show("Введите индекс");
                }
                int index = int.Parse(txtIndex.Text);
                if (!CheckIndex(index)) return;

                playlist.JumpTo(index);
                RefreshList();
            }
            catch (FormatException)
            {
                MessageBox.Show("Неккоректный формат");
            }
        }

        // удаление по индексу
        private void RemoveByIndex_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtIndex.Text == string.Empty)
                {
                    MessageBox.Show("Введите индекс");
                }
                int index = int.Parse(txtIndex.Text);
                if (!CheckIndex(index)) return;

                playlist.Remove(index);
                RefreshList();
            }
            catch (FormatException)
            {
                MessageBox.Show("индекс должен быть целым числом!");
            }
        }

        // удаление по значению Song
        private void RemoveBySong_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lstSongs.SelectedIndex < 0)
                {
                    MessageBox.Show("выберите композицию в списке!");
                    return;
                }

                Song s = playlist.Songs[lstSongs.SelectedIndex];
                playlist.Remove(s);
                RefreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // очистка
        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                playlist.ClearAll();
                RefreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ошибка: " + ex.Message);
            }
        }

        // двойной клик = переход по индексу
        private void lstSongs_DoubleClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lstSongs.SelectedIndex >= 0)
                {
                    playlist.JumpTo(lstSongs.SelectedIndex);
                    RefreshList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private bool CheckIndex (int index)
        {
            if (index > playlist.Count - 1 || index < 0) { MessageBox.Show("Индекс вышел за границу"); return false; }
            else return true;
        }
    }
}