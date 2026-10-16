using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace Cafe
{
    /// <summary>
    /// Репозиторий для загрузки данных из CSV-файлов.
    /// </summary>
    public class CsvRepository
    {
        private string _basePath;

        public CsvRepository(string basePath)
        {
            _basePath = basePath;
        }

        public List<Shift> GetShifts()
        {
            List<Shift> result = new List<Shift>();
            string path = Path.Combine(_basePath, "shifts.csv");
            if (!File.Exists(path)) return result;

            string[] lines = File.ReadAllLines(path);
            if (lines.Length < 2) return result;

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length < 3) continue;

                Shift s = new Shift();
                s.Id = int.Parse(parts[0]);
                s.Time = parts[1];
                s.Date = DateTime.ParseExact(parts[2], "dd.MM.yyyy", CultureInfo.InvariantCulture);
                result.Add(s);
            }
            return result;
        }

        public List<Barista> GetBaristas()
        {
            List<Barista> result = new List<Barista>();
            string path = Path.Combine(_basePath, "baristas.csv");
            if (!File.Exists(path)) return result;

            string[] lines = File.ReadAllLines(path);
            if (lines.Length < 2) return result;

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length < 4) continue;

                Barista b = new Barista();
                b.Id = int.Parse(parts[0]);
                b.FullName = parts[1];
                b.Experience = int.Parse(parts[2]);
                b.Rating = double.Parse(parts[3], CultureInfo.InvariantCulture);
                result.Add(b);
            }
            return result;
        }

        public List<Drink> GetDrinks()
        {
            List<Drink> result = new List<Drink>();
            string path = Path.Combine(_basePath, "drinks.csv");
            if (!File.Exists(path)) return result;

            string[] lines = File.ReadAllLines(path);
            if (lines.Length < 2) return result;

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length < 6) continue;

                Drink d = new Drink();
                d.Id = int.Parse(parts[0]);
                d.Name = parts[1];
                d.ShiftId = int.Parse(parts[2]);
                d.BaristaId = int.Parse(parts[3]);
                d.Price = decimal.Parse(parts[4], CultureInfo.InvariantCulture);
                d.Volume = int.Parse(parts[5]);
                result.Add(d);
            }
            return result;
        }
    }
}