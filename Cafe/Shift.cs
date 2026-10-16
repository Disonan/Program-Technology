using System;

namespace Cafe
{
    /// <summary>
    /// Представляет смену в кофейне.
    /// </summary>
    public class Shift
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public DateTime Date { get; set; }

        /// <summary>
        /// Является ли смена утренней.
        /// </summary>
        public bool IsMorning
        {
            get { return Time == "Утренняя"; }
        }

        /// <summary>
        /// Возвращает строковое представление смены.
        /// </summary>
        public string GetInfo()
        {
            return $"{Time} ({Date:dd.MM.yyyy})";
        }
    }
}