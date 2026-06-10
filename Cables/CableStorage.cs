using System.Collections.Generic;
using System.Linq;

namespace Cables
{
    public class CableStorage
    {
        private List<Cable> cables = new List<Cable>();
        private Dictionary<int, Cable> cableMap = new Dictionary<int, Cable>();
        private int nextKey = 0;

        //получение всех кабелей
        public List<Cable> GetAll()
        {
            return cables;
        }

        //получение словаря
        public Dictionary<int, Cable> GetMap()
        {
            return cableMap;
        }

        //количество кабелей
        public int Count
        {
            get { return cables.Count; }
        }

        //добавление по объекту
        public void Add(Cable cable)
        {
            cables.Add(cable);
            cableMap.Add(nextKey, cable);
            nextKey++;
        }

        //перегрузка добавления по полям базового кабеля
        public void Add(string type, int cores, double diameter, string manufacturer, double price)
        {
            Cable cable = new Cable(type, cores, diameter, manufacturer, price);
            Add(cable);
        }

        //перегрузка добавления по полям потомка
        public void Add(string type, int cores, double diameter, string manufacturer, double price,
            bool hasShield, string shieldMaterial, bool isFlexible)
        {
            ShieldedCable cable = new ShieldedCable(type, cores, diameter, manufacturer, price, hasShield, shieldMaterial, isFlexible);
            Add(cable);
        }

        //удаление по индексу
        public void Remove(int index)
        {
            if (index < 0 || index >= cables.Count)
                return;

            Cable target = cables[index];
            cables.RemoveAt(index);

            //удаляем по значению
            int key = cableMap.FirstOrDefault(p => p.Value == target).Key;
            cableMap.Remove(key);
        }

        //перегрузка удаления по типу
        public void Remove(string type)
        {
            //ищем первый кабель с таким типом через LINQ
            Cable target = cables.FirstOrDefault(c => c.Type != null && c.Type.Trim().ToLower() == type.Trim().ToLower());

            if (target == null)
                return;

            cables.Remove(target);

            int key = cableMap.FirstOrDefault(p => p.Value == target).Key;
            cableMap.Remove(key);
        }

        //перегрузка удаления по объекту
        public void Remove(Cable cable)
        {
            if (!cables.Contains(cable))
                return;

            cables.Remove(cable);

            int key = cableMap.FirstOrDefault(p => p.Value == cable).Key;
            cableMap.Remove(key);
        }

        //сортировка по качеству через LINQ
        public List<Cable> SortByQuality()
        {
            return cables.OrderByDescending(c => c.Quality()).ToList();
        }

        //фильтр только экранированных через LINQ
        public List<Cable> OnlyShielded()
        {
            return cables.OfType<ShieldedCable>().Cast<Cable>().ToList();
        }

        //средняя цена
        public double AveragePrice()
        {
            if (cables.Count == 0)
                return 0;

            return cables.Average(c => c.Price);
        }
    }
}