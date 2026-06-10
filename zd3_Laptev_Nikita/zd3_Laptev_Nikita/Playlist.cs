using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zd3_Laptev_Nikita
{
    class Playlist
    {
        private List<Song> list;
        private int currentIndex;

        public Playlist()
        {
            list = new List<Song>();
            currentIndex = 0;
        }

        // доступ для отображения
        public List<Song> Songs
        {
            get { return list; }
        }

        public int CurrentIndex
        {
            get { return currentIndex; }
        }

        public int Count
        {
            get { return list.Count; }
        }

        // текущая аудиозапись
        public Song CurrentSong()
        {
            if (list.Count > 0)
                return list[currentIndex];
            else
                throw new IndexOutOfRangeException(
                    "невозможно получить текущую аудиозапись для пустого плейлиста!");
        }

        // добавление готовой структуры
        public void Add(Song song)
        {
            list.Add(song);
        }

        // добавление по отдельным полям
        public void Add(string author, string title, string filename)
        {
            list.Add(new Song(author, title, filename));
        }

        // добавление без имени файла
        public void Add(string author, string title)
        {
            string filename = title.ToLower().Replace(" ", "_") + ".mp3";
            list.Add(new Song(author, title, filename));
        }

        // переход к следующей
        public Song Next()
        {
            if (list.Count == 0)
                throw new IndexOutOfRangeException("плейлист пуст!");

            currentIndex++;
            if (currentIndex >= list.Count) // защита от выхода за границы
                currentIndex = 0;           // переход в начало

            return list[currentIndex];
        }

        // переход к предыдущей
        public Song Previous()
        {
            if (list.Count == 0)
                throw new IndexOutOfRangeException("плейлист пуст!");

            currentIndex--;
            if (currentIndex < 0)              // защита от выхода за границы
                currentIndex = list.Count - 1; // переход на конец

            return list[currentIndex];
        }

        // переход по индексу
        public Song JumpTo(int index)
        {
            if (list.Count == 0)
                throw new IndexOutOfRangeException("плейлист пуст!");

            if (index < 0 || index >= list.Count)
                throw new IndexOutOfRangeException(
                    $"индекс {index} вне диапазона [0..{list.Count - 1}]!");

            currentIndex = index;
            return list[currentIndex];
        }

        // переход к началу
        public Song ToStart()
        {
            if (list.Count == 0)
                throw new IndexOutOfRangeException("плейлист пуст!");

            currentIndex = 0;
            return list[currentIndex];
        }

        // удаление по индексу
        public void Remove(int index)
        {
            if (index < 0 || index >= list.Count)
                throw new IndexOutOfRangeException(
                    $"невозможно удалить: индекс {index} вне диапазона!");

            list.RemoveAt(index);

            // корректировка текущего индекса
            if (currentIndex >= list.Count)
            {
                if (list.Count > 0)
                    currentIndex = list.Count - 1;
                else
                    currentIndex = 0;
            }
        }

        // удаление по значению
        public void Remove(Song song)
        {
            int idx = -1;

            // поиск первого совпадения обычным циклом
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Author == song.Author &&
                    list[i].Title == song.Title &&
                    list[i].Filename == song.Filename)
                {
                    idx = i;
                    break;
                }
            }

            if (idx == -1)
                throw new InvalidOperationException(
                    "указанная композиция не найдена в плейлисте!");

            list.RemoveAt(idx);

            if (currentIndex >= list.Count)
            {
                if (list.Count > 0)
                    currentIndex = list.Count - 1;
                else
                    currentIndex = 0;
            }
        }

        // удаление по автору и названию
        public void Remove(string author, string title)
        {
            int idx = -1;

            // поиск первого совпадения обычным циклом
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Author == author &&
                    list[i].Title == title)
                {
                    idx = i;
                    break;
                }
            }

            if (idx == -1)
                throw new InvalidOperationException(
                    "композиция с таким автором и названием не найдена!");

            list.RemoveAt(idx);

            if (currentIndex >= list.Count)
            {
                if (list.Count > 0)
                    currentIndex = list.Count - 1;
                else
                    currentIndex = 0;
            }
        }

        // очистка плейлиста
        public void ClearAll()
        {
            list.Clear();
            currentIndex = 0;
        }

        // проверка есть ли уже такая песня в плейлисте
        public bool Contains(Song song)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Author == song.Author &&
                    list[i].Title == song.Title &&
                    list[i].Filename == song.Filename)
                {
                    return true;
                }
            }
            return false;
        }
    }
}