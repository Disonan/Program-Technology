
using NumberQuest.Interfaces;
using NumberQuest.Models;

namespace NumberQuest.Services
{
    public class GameService : IGame
    {
        private readonly IRandomNumberGenerator _randomGenerator;

        private GameSettings _settings = new GameSettings();
        private int _secretNumber;
        private bool _isStarted;

        public event EventHandler<NumberQuest.Events.GameEventArgs>? GameEvent;

        public int AttemptsUsed { get; private set; }

        public bool IsFinished { get; private set; }

        public bool IsWon { get; private set; }

        public string LastMessage { get; private set; } = "";

        public GameService(IRandomNumberGenerator randomGenerator)
        {
            _randomGenerator = randomGenerator;
        }

        public void Start(GameSettings settings)
        {
            _settings = settings;

            _secretNumber = _randomGenerator.Generate(
                settings.MinNumber,
                settings.MaxNumber);

            AttemptsUsed = 0;
            IsFinished = false;
            IsWon = false;
            _isStarted = true;

            LastMessage = "Игра началась!";
        }

        public bool MakeGuess(int number)
        {
            if (!_isStarted || IsFinished)
            {
                throw new InvalidOperationException(
                    "Игра не запущена или уже завершена.");
            }

            if (number < _settings.MinNumber ||
                number > _settings.MaxNumber)
            {
                LastMessage = "Число находится вне заданного диапазона.";
                return false;
            }

            AttemptsUsed++;

            if (number == _secretNumber)
            {
                IsWon = true;
                IsFinished = true;
                LastMessage = "Поздравляем! Вы угадали число!";
                return true;
            }

            if (number < _secretNumber)
            {
                LastMessage = "Загаданное число больше.";
            }
            else
            {
                LastMessage = "Загаданное число меньше.";
            }

            if (AttemptsUsed >= _settings.MaxAttempts)
            {
                IsFinished = true;
                LastMessage += " Попытки закончились!";
            }

            return false;
        }
    }
}
