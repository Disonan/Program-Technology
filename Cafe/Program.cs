using System;
using System.Collections.Generic;

namespace Cafe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выберите источник данных:");
            Console.WriteLine("1 - InMemoryRepository");
            Console.WriteLine("2 - CsvRepository (папка data)");
            Console.Write("Ваш выбор: ");

            string choice = Console.ReadLine();
            List<Shift> shifts;
            List<Barista> baristas;
            List<Drink> drinks;

            switch (choice)
            {
                case "1":
                    InMemoryRepository memRepo = new InMemoryRepository();
                    shifts = memRepo.GetShifts();
                    baristas = memRepo.GetBaristas();
                    drinks = memRepo.GetDrinks();
                    break;
                case "2":
                    CsvRepository csvRepo = new CsvRepository("data");
                    shifts = csvRepo.GetShifts();
                    baristas = csvRepo.GetBaristas();
                    drinks = csvRepo.GetDrinks();
                    break;
                default:
                    Console.WriteLine("Неверный выбор");
                    return;
            }

            Console.WriteLine("\n1. FindBarista(\"Латте\"):");
            Barista foundBarista = FindBarista("Латте", drinks, baristas);
            Console.WriteLine(foundBarista != null ? foundBarista.GetInfo() : "null");

            Console.WriteLine("\n2. FindShift(drink \"Латте\"):");
            Shift foundShift = FindShift("Латте", drinks, shifts);
            Console.WriteLine(foundShift != null ? foundShift.GetInfo() : "null");

            Console.WriteLine("\n3. GetTotalVolume:");
            int totalVolume = GetTotalVolume(drinks);
            Console.WriteLine($"{totalVolume} мл");

            Console.WriteLine("\n4. GetBaristaRating:");
            Dictionary<string, double> ratings = GetBaristaRating(baristas);
            foreach (var kvp in ratings)
            {
                Console.WriteLine($"{kvp.Key} — {kvp.Value}");
            }

            Console.WriteLine("\n5. PrintAllDrinks:");
            PrintAllDrinks(drinks, baristas, shifts);

            Console.WriteLine("\nНе найдено: FindBarista(\"Неизвестный напиток\") → " +
                (FindBarista("Неизвестный напиток", drinks, baristas) == null ? "null" : "найдено"));

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        /// <summary>
        /// Находит бариста, приготовившего указанный напиток.
        /// </summary>
        public static Barista FindBarista(string drinkName, List<Drink> drinks, List<Barista> baristas)
        {
            foreach (Drink d in drinks)
            {
                if (d.Name == drinkName)
                {
                    foreach (Barista b in baristas)
                    {
                        if (b.Id == d.BaristaId)
                        {
                            return b;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Находит смену, в которую был приготовлен указанный напиток.
        /// </summary>
        public static Shift FindShift(string drinkName, List<Drink> drinks, List<Shift> shifts)
        {
            foreach (Drink d in drinks)
            {
                if (d.Name == drinkName)
                {
                    foreach (Shift s in shifts)
                    {
                        if (s.Id == d.ShiftId)
                        {
                            return s;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Подсчитывает общий объем всех напитков.
        /// </summary>
        public static int GetTotalVolume(List<Drink> drinks)
        {
            int total = 0;
            foreach (Drink d in drinks)
            {
                total += d.Volume;
            }
            return total;
        }

        /// <summary>
        /// Возвращает средний рейтинг бариста. Округляет до 1 знака.
        /// </summary>
        public static Dictionary<string, double> GetBaristaRating(List<Barista> baristas)
        {
            Dictionary<string, double> result = new Dictionary<string, double>();
            foreach (Barista b in baristas)
            {
                double roundedRating = Math.Round(b.Rating, 1);
                result[b.FullName] = roundedRating;
            }
            return result;
        }

        /// <summary>
        /// Выводит все напитки с информацией о бариста и смене.
        /// </summary>
        public static void PrintAllDrinks(List<Drink> drinks, List<Barista> baristas, List<Shift> shifts)
        {
            foreach (Drink d in drinks)
            {
                Barista b = null;
                foreach (Barista item in baristas)
                {
                    if (item.Id == d.BaristaId)
                    {
                        b = item;
                        break;
                    }
                }

                Shift s = null;
                foreach (Shift item in shifts)
                {
                    if (item.Id == d.ShiftId)
                    {
                        s = item;
                        break;
                    }
                }

                string baristaName = b != null ? b.FullName : "—";
                string shiftTime = s != null ? s.Time : "—";

                Console.WriteLine($"\"{d.GetInfo()}\" — бариста {baristaName}, смена \"{shiftTime}\"");
            }
        }
    }
}