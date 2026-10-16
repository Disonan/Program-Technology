namespace Cafe
{
    /// <summary>
    /// Представляет бариста.
    /// </summary>
    public class Barista
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Experience { get; set; }
        public double Rating { get; set; }

        /// <summary>
        /// Является ли бариста опытным (стаж больше 2 лет).
        /// </summary>
        public bool IsExperienced
        {
            get { return Experience > 2; }
        }

        /// <summary>
        /// Возвращает информацию о бариста.
        /// </summary>
        public string GetInfo()
        {
            return $"{FullName} ({Experience} года, рейтинг {Rating})";
        }
    }
}