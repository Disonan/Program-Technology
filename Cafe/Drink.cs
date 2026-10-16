namespace Cafe
{
    /// <summary>
    /// Представляет напиток.
    /// </summary>
    public class Drink
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ShiftId { get; set; }
        public int BaristaId { get; set; }
        public decimal Price { get; set; }
        public int Volume { get; set; }

        /// <summary>
        /// Цена за миллилитр.
        /// </summary>
        public decimal PricePerMl
        {
            get
            {
                if (Volume == 0) return 0;
                return Price / Volume;
            }
        }

        /// <summary>
        /// Является ли напиток горячим (содержит "Кофе" или "Чай").
        /// </summary>
        public bool IsHot
        {
            get { return Name.Contains("Кофе") || Name.Contains("Чай"); }
        }

        /// <summary>
        /// Возвращает информацию о напитке.
        /// </summary>
        public string GetInfo()
        {
            return $"{Name} ({Price} руб., {Volume} мл)";
        }
    }
}