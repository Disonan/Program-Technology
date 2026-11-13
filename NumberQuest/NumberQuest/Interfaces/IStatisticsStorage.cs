using System;
using System.Collections.Generic;
using System.Text;
using NumberQuest.Models;


namespace NumberQuest.Interfaces
{
    public interface IStatisticsStorage
    {
        GameStatistics Load();

        void Save(GameStatistics statistics);
    }
}

