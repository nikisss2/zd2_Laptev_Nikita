namespace zd2_Shop
{
    class Product
    {
        // цена товара
        public decimal Price { get; set; }

        // наименование товара
        public string Name { get; set; }

        // количество товара
        public int Count { get; set; }

        // конструктор товара
        public Product(string name, decimal price, int count)
        {
            Name = name;
            Price = price;
            Count = count;
        }

        // строка для отображения в списке
        public override string ToString()
        {
            return $"{Name}, цена: {Price} руб., количество: {Count}";
        }
    }
}