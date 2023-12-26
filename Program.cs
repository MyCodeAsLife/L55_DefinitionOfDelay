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
        private Random _random;

        private int _stewsCount;
        private int _currentYear;

        public Storage()
        {
            _stews = new List<Stew>();
            _random = new Random();
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
                _stews.Add(new Stew((StewName)_random.Next(stewNameCount), _currentYear, _random));
        }
    }

    class Stew
    {
        Random _random;

        private int _minProductionYear;
        private int _maxProductionYear;
        private int _minExpirationDate;
        private int _maxExpirationDate;

        public Stew(StewName name, int maxProductionYear, Random random)
        {
            _random = random;
            _maxProductionYear = maxProductionYear;
            _minProductionYear = 1977;
            _maxExpirationDate = 30;
            _minExpirationDate = 5;
            Name = name.ToString();
            ProductionYear = _random.Next(_minProductionYear, _maxProductionYear + 1);
            ExpirationDate = _random.Next(_minExpirationDate, _maxExpirationDate + 1);
        }

        public string Name { get; private set; }
        public int ProductionYear { get; private set; }
        public int ExpirationDate { get; private set; }
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
