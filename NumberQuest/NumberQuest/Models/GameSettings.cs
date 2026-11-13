using System;
using System.Collections.Generic;
using System.Text;


namespace NumberQuest.Models
{
    public class GameSettings
    {
        public int MinNumber { get; set; } = 1;

        public int MaxNumber { get; set; } = 100;

        public int MaxAttempts { get; set; } = 10;

        public string Difficulty { get; set; } = "Средний";
    }
}
