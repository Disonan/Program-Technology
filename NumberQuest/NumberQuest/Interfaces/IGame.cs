using System;
using System.Collections.Generic;
using System.Text;
using NumberQuest.Models;


namespace NumberQuest.Interfaces
{
    public interface IGame
    {
        event EventHandler<GameEventArgs> GameEvent;

        void Start(GameSettings settings);
        bool MakeGuess(int number);
    }
}
