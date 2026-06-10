using System.Collections.Generic;

namespace zd2_Shop
{
    class Shop
    {
        // список товаров
        public List<Product> Products { get; private set; }

        // прибыль магазина
        public decimal Profit { get; private set; }

        // конструктор магазина
        public Shop()
        {
            Products = new List<Product>();
            Profit = 0;
        }

        // создать и добавить товар
        public void CreateProduct(string name, decimal price, int count)
        {
            Products.Add(new Product(name, price, count));
        }

        // поиск товара по имени
        public Product FindByName(string name)
        {
            foreach (Product product in Products)
            {
                if (product.Name == name)
                {
                    return product;
                }
            }
            return null;
        }

        // продажа товара по объекту
        public string Sell(Product product, int count)
        {
            if (product == null)
            {
                return "Товар не найден!";
            }

            if (product.Count < count)
            {
                return "Недостаточно товара в наличии!";
            }

            product.Count -= count;          // уменьшаем количество
            Profit += product.Price * count; // увеличиваем прибыль
            return $"Продано: {product.Name} x{count}";
        }

        // продажа товара по имени
        public string Sell(string name, int count)
        {
            // ищем товар по имени и вызываем первую перегрузку
            Product product = FindByName(name);
            return Sell(product, count);
        }
    }
}