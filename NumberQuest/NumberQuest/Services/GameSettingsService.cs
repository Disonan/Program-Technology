using System;
using System.Collections.Generic;
using System.Text;
using NumberQuest.Models;


namespace NumberQuest.Services
{
    public class GameSettingsService
    {
        public GameSettings GetSettings(int choice)
        {
            switch (choice)
            {
                case 1:
                    return new GameSettings
                    {
                        MinNumber = 1,
                        MaxNumber = 50,
                        MaxAttempts = 10,
                        Difficulty = "Лёгкий"
                    };

                case 2:
                    return new GameSettings
                    {
                        MinNumber = 1,
                        MaxNumber = 100,
                        MaxAttempts = 10,
                        Difficulty = "Средний"
                    };

                case 3:
                    return new GameSettings
                    {
                        MinNumber = 1,
                        MaxNumber = 500,
                        MaxAttempts = 12,
                        Difficulty = "Сложный"
                    };

                default:
                    throw new ArgumentException(
                        "Неизвестный уровень сложности.");
            }
        }

        public GameSettings CreateCustomSettings(
            int minNumber,
            int maxNumber,
            int maxAttempts)
        {
            if (minNumber >= maxNumber)
            {
                throw new ArgumentException(
                    "Максимальное число должно быть больше минимального.");
            }

            if (maxAttempts < 1 || maxAttempts > 1000)
            {
                throw new ArgumentException(
                    "Количество попыток должно быть от 1 до 1000.");
            }

            return new GameSettings
            {
                MinNumber = minNumber,
                MaxNumber = maxNumber,
                MaxAttempts = maxAttempts,
                Difficulty = "Пользовательский"
            };
        }
    }
}
