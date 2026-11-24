
using NumberQuest.Models;

namespace NumberQuest.Interfaces
{
    public interface IGame
    {
        event EventHandler<NumberQuest.Events.GameEventArgs> GameEvent;

        void Start(GameSettings settings);

        bool MakeGuess(int number);
    }
}
