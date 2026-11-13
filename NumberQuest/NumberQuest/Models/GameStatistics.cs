using System;
using System.Collections.Generic;
using System.Text;


namespace NumberQuest.Models
{
    public class GameStatistics
    {
        public int TotalGames { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public int BestAttempts { get; set; }

        public List<GameResult> Results { get; set; } = new List<GameResult>();
    }
}

