using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace zd2_Laptev
{
    static class PhoneBookLoader
    {
        // загрузка записей из CSV-файла
        public static void Load(PhoneBook phoneBook, string fileName)
        {
            if (!File.Exists(fileName)) { MessageBox.Show("Файл не найден!"); return; }

            phoneBook.Clear();

            var contacts = File.ReadAllLines(fileName)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => line.Split(';'))
                .Where(parts => parts.Length >= 2)
                .Select(parts => new Contact(parts[0].Trim(),
                                             parts[1].Trim()));

            // добавляем все найденные контакты в книгу
            foreach (var c in contacts)
                phoneBook.Add(c);
        }

        // сохранение записей в CSV-файл 
        public static void Save(PhoneBook phoneBook, string fileName)
        {
            var lines = phoneBook.GetAll()
                .Select(c => c.Name + ";" + c.Phone);

            File.WriteAllLines(fileName, lines);
        }
    }
}