using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zd1
{
    class Cat
    {
        private string name;
        private double weight;

        public string Name
        {
            get { return name; }
            set
            {
                bool onlyLetters = true;

                if (string.IsNullOrWhiteSpace(value))
                {
                    onlyLetters = false;
                }
                else
                {
                    foreach (char ch in value)
                    {
                        if (!char.IsLetter(ch))
                        {
                            onlyLetters = false;
                        }
                    }
                }

                if (onlyLetters)
                {
                    name = value;
                }
                else
                {
                    Console.WriteLine($"{value} неправильное имя! Имя не изменено");
                }
            }
        }

        public double Weight
        {
            get { return weight; }
            set
            {
                if (value > 0 && value < 22)
                {
                    weight = value;
                }
                else
                {
                    Console.WriteLine($"{value} неправильный вес! Вес не изменён");
                }
            }
        }

        public Cat(string catName, double catWeight)
        {
            Name = catName;
            Weight = catWeight;
        }

        public void Meow()
        {
            Console.WriteLine($"{name} (вес {weight} кг): МЯЯЯЯУ!!!!");
        }
    }
}
