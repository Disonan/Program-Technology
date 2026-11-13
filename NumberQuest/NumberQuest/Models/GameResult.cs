using System;
using System.Collections.Generic;
using System.Text;


namespace NumberQuest.Models
{
    public class GameResult
    {
        public DateTime Date { get; set; }

        public bool IsWin { get; set; }

        public int Attempts { get; set; }

        public string Difficulty { get; set; } = "";
    }
}
