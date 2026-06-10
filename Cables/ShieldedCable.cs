using System;

namespace Cables
{
    public class ShieldedCable : Cable
    {
        public bool HasShield { get; set; }
        public string ShieldMaterial { get; set; }
        public bool IsFlexible { get; set; }

        //конструктор потомка
        public ShieldedCable(string type, int cores, double diameter, string manufacturer,
            double price, bool hasShield, string shieldMaterial, bool isFlexible)
            : base(type, cores, diameter, manufacturer, price)
        {
            HasShield = hasShield;
            ShieldMaterial = shieldMaterial;
            IsFlexible = isFlexible;
        }

        //переопределение функции качества Qp
        public override double Quality()
        {
            //есть ли оплетка
            if (HasShield)
            {
                return 2 * base.Quality();
            }
            else
            {
                return 0.7 * base.Quality();
            }
        }

        //вывод информации
        public override string Info()
        {
            //оплеткв
            string shield;

            if (HasShield)
            {
                shield = "есть";
            }
            else
            {
                shield = "нет";
            }

            //гибкость
            string flex;

            if (IsFlexible)
            {
                flex = "да";
            }
            else
            {
                flex = "нет";
            }

            return $"Тип: {Type}, жил: {Cores}, диаметр: {Diameter}, производитель: {Manufacturer}, цена: {Price}, оплетка: {shield}, материал оплетки: {ShieldMaterial}, гибкий: {flex}, Qp = {Math.Round(Quality(), 3)}";
        }
    }
}