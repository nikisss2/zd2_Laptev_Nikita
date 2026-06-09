using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace zd2_Laptev
{
    internal class Contact
    {
        // имя контакта
        public string Name { get; set; }

        // телефон контакта
        public string Phone { get; set; }

        public Contact() { }
        public Contact(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }

        // вывод в строке
        public override string ToString()
        {
            return Name + " - " + Phone;
        }

    }
}