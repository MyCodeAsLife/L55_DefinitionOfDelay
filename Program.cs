using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace L55_DefinitionOfDelay
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Storage storage = new Storage();

            storage.ShowExpiredGoods();
        }
    }

    class Storage
    {
        private List<Stew> _stews;
        private int _stewsCount;
        private int _currentYear;

        public Storage()
        {
            _stews = new List<Stew>();
            _stewsCount = 30;
            _currentYear = 2023;

            Fill();
        }

        public void ShowExpiredGoods()
        {
            List<Stew> expiredGoods = _stews.Where(stew => stew.ExpirationDate <= (_currentYear - stew.ProductionYear)).ToList();
            int maxStringLenght = expiredGoods.Max(stew => stew.Name.Length);
            Console.WriteLine($"Всего товара: {_stewsCount} штук.\tИз них просроченные: {expiredGoods.Count} штук. Текущий год: {_currentYear}");

            foreach (var stew in expiredGoods)
                Console.WriteLine($"{{0, 13}} {{1, -{maxStringLenght}}} {{2, 45}}", $"Наименование:", stew.Name, $"Дата " +
                              $"производства: {stew.ProductionYear}\tСрок годности: {stew.ExpirationDate} лет.");
        }

        private void Fill()
        {
            int stewNameCount = Enum.GetNames(typeof(StewName)).Length;

            for (int i = 0; i < _stewsCount; i++)
                _stews.Add(new Stew((StewName)RandomGenerator.GetRandomNumber(stewNameCount), _currentYear));
        }
    }

    class Stew
    {
        public Stew(StewName name, int maxProductionYear)
        {
            int minProductionYear = 1977;
            int maxExpirationDate = 30;
            int minExpirationDate = 5;
            Name = name.ToString();

            ProductionYear = RandomGenerator.GetRandomNumber(minProductionYear, maxProductionYear + 1);
            ExpirationDate = RandomGenerator.GetRandomNumber(minExpirationDate, maxExpirationDate + 1);
        }

        public string Name { get; private set; }
        public int ProductionYear { get; private set; }
        public int ExpirationDate { get; private set; }
    }

    static class RandomGenerator
    {
        private static Random s_random = new Random();

        public static int GetRandomNumber(int minValue, int maxValue) => s_random.Next(minValue, maxValue);

        public static int GetRandomNumber(int maxValue) => s_random.Next(maxValue);
    }

    enum StewName
    {
        Beef,
        Pork,
        Chicken,
        Venison,
        Horsemeat,
        Elkmeat,
    }
}
