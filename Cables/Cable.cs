namespace Cables
{
    public class Cable
    {
        public string Type { get; set; }
        public int Cores { get; set; }
        public double Diameter { get; set; }
        public string Manufacturer { get; set; }
        public double Price { get; set; }

        //конструктор
        public Cable(string type, int cores, double diameter, string manufacturer, double price)
        {
            Type = type;
            Cores = cores;
            Diameter = diameter;
            Manufacturer = manufacturer;
            Price = price;
        }

        //функция качества Q
        public virtual double Quality()
        {
            return Diameter / Cores;
        }

        //вывод информации об объекте
        public virtual string Info()
        {
            return $"Тип: {Type}, жил: {Cores}, диаметр: {Diameter}, производитель: {Manufacturer}, цена: {Price}, Q = {Math.Round(Quality(), 3)}";
        }
        
    }
}
