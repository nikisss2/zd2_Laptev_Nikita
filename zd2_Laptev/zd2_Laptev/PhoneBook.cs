using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace zd2_Laptev
{
    internal class PhoneBook
    {
        private List<Contact> contacts = new List<Contact>();

        // получение списка всех контактов
        public List<Contact> GetAll()
        {
            return contacts;
        }

        // количество контактов
        public int Count
        {
            get { return contacts.Count; }
        }

        // добавление контакта (объектом)
        public void Add(Contact contact)
        {
            contacts.Add(contact);
        }

        // перегрузка добавления контакта (по полям)
        public void Add(string name, string phone)
        {
            contacts.Add(new Contact(name, phone));
        }

        // удаление контакта по индексу
        public void Remove(int index)
        {
            if (index < 0 || index >= contacts.Count) { MessageBox.Show("Индекс выходит за границу!"); return; }

            contacts.RemoveAt(index);
        }

        // перегрузка удаления контакта (по имени)
        public void Remove(string name)
        {
            // ищем контакт без учёта регистра и пробелов по краям
            Contact contact = contacts.FirstOrDefault(c =>
                c.Name != null &&
                c.Name.Trim().ToLower() == name.Trim().ToLower());

            if (contact == null) { MessageBox.Show("Контакт не найден!"); return; }

            contacts.Remove(contact);
        }

        // поиск контактов по имени
        public List<Contact> Find(string name)
        {
            // выбираем все контакты, имя которых содержит подстроку
            return contacts
                .Where(c => c.Name != null && c.Name.ToLower()
                .Contains(name.ToLower()))
                .ToList();
        }

        // сортировка контактов по имени
        public List<Contact> SortByName()
        {
            return contacts.OrderBy(c => c.Name).ToList();
        }

        // очистка
        public void Clear()
        {
            contacts.Clear();
        }
    }
}