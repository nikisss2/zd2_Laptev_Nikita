using System;
using zd1;

int count = 0;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

// ввод количества котов с проверкой
while (true)
{
    Console.Write("Введите количество котов: ");
    try
    {
        string input = Console.ReadLine();
        count = int.Parse(input);

        if (count <= 0 )
        {
            Console.WriteLine("Количество должно быть больше нуля, повторите ввод");
        }
        else
        {
            break;
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("Некорректный ввод");
    }
}

Cat[] cats = new Cat[count];

// ввод данных для каждого кота
for (int i = 0; i < count; i++)
{
    Console.WriteLine($"\nКот {i + 1}");

    // ввод имени
    string catName = "";
    while (true)
    {
        Console.Write("Введите имя кота: ");
        catName = Console.ReadLine();

        bool onlyLetters = true;

        if (string.IsNullOrWhiteSpace(catName))
        {
            onlyLetters = false;
        }
        else
        {
            foreach (char ch in catName)
            {
                if (!char.IsLetter(ch))
                {
                    onlyLetters = false;
                }
            }
        }

        if (onlyLetters)
        {
            break;
        }
        else
        {
            Console.WriteLine("Имя должно содержать только буквы, повторите ввод");
        }
    }

    // ввод веса
    double catWeight = 0;
    while (true)
    {
        Console.Write("Введите вес кота (кг): ");
        try
        {
            string weightInput = Console.ReadLine();
            catWeight = double.Parse(weightInput);

            if (catWeight > 0)
            {
                break;
            }
            else
            {
                Console.WriteLine("Вес должен быть положительным, повторите ввод.");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Некорректный ввод");
        }
    }

    cats[i] = new Cat(catName, catWeight);
}



// проверка результата
//Console.WriteLine("\nВсе коты мяукают");
foreach (Cat cat in cats)
{
    if (cat != null)
    {
        cat.Meow();
    }
    else
    {
        Console.WriteLine("Кот не создан");
    }
}

Console.ReadLine();