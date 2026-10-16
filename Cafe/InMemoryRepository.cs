using System;
using System.Collections.Generic;

namespace Cafe
{
    /// <summary>
    /// Репозиторий с тестовыми данными в памяти.
    /// </summary>
    public class InMemoryRepository
    {
        private List<Shift> _shifts;
        private List<Barista> _baristas;
        private List<Drink> _drinks;

        public InMemoryRepository()
        {
            _shifts = new List<Shift>
            {
                new Shift { Id = 1, Time = "Утренняя", Date = new DateTime(2025, 9, 1) },
                new Shift { Id = 2, Time = "Дневная", Date = new DateTime(2025, 9, 1) },
                new Shift { Id = 3, Time = "Вечерняя", Date = new DateTime(2025, 9, 2) }
            };

            _baristas = new List<Barista>
            {
                new Barista { Id = 1, FullName = "Петров П.П.", Experience = 3, Rating = 4.8 },
                new Barista { Id = 2, FullName = "Иванов И.И.", Experience = 1, Rating = 4.5 },
                new Barista { Id = 3, FullName = "Сидоров С.С.", Experience = 5, Rating = 5.0 }
            };

            _drinks = new List<Drink>
            {
                new Drink { Id = 1, Name = "Латте", ShiftId = 1, BaristaId = 1, Price = 250, Volume = 300 },
                new Drink { Id = 2, Name = "Капучино", ShiftId = 1, BaristaId = 2, Price = 220, Volume = 250 },
                new Drink { Id = 3, Name = "Эспрессо", ShiftId = 2, BaristaId = 1, Price = 150, Volume = 50 },
                new Drink { Id = 4, Name = "Чай зеленый", ShiftId = 2, BaristaId = 3, Price = 180, Volume = 400 },
                new Drink { Id = 5, Name = "Кофе с молоком", ShiftId = 3, BaristaId = 2, Price = 200, Volume = 300 }
            };
        }

        public List<Shift> GetShifts() { return _shifts; }
        public List<Barista> GetBaristas() { return _baristas; }
        public List<Drink> GetDrinks() { return _drinks; }
    }
}