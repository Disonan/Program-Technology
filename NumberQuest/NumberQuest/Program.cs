
using NumberQuest.Models;
using NumberQuest.Services;

GameSettingsService settingsService = new GameSettingsService();

Console.WriteLine("");
Console.WriteLine("       NUMBER QUEST");
Console.WriteLine("");
Console.WriteLine();

Console.WriteLine("Выберите уровень сложности:");
Console.WriteLine("1. Лёгкий");
Console.WriteLine("2. Средний");
Console.WriteLine("3. Сложный");
Console.WriteLine("4. Свои настройки");
Console.WriteLine();

int choice = ReadNumber("Ваш выбор: ", 1, 4);

GameSettings settings;

if (choice == 4)
{
    Console.WriteLine();
    Console.WriteLine("Настройка собственной игры");

    int min = ReadNumber("Минимальное число: ");
    int max = ReadNumber("Максимальное число: ");
    int attempts = ReadNumber("Количество попыток: ", 1, 1000);

    try
    {
        settings = settingsService.CreateCustomSettings(
            min, max, attempts);
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine("Ошибка: " + ex.Message);
        return;
    }
}
else
{
    settings = settingsService.GetSettings(choice);
}

Console.WriteLine();
Console.WriteLine("Настройки игры:");
Console.WriteLine("Сложность: " + settings.Difficulty);
Console.WriteLine("Диапазон: " + settings.MinNumber +
                  " - " + settings.MaxNumber);
Console.WriteLine("Попыток: " + settings.MaxAttempts);

Console.WriteLine();
Console.WriteLine("Настройки успешно применены!");

static int ReadNumber(
    string message, int? min = null, int? max = null)
{
    while (true)
    {
        Console.Write(message);

        if (int.TryParse(Console.ReadLine(), out int number))
        {
            if ((!min.HasValue || number >= min.Value) &&
                (!max.HasValue || number <= max.Value))
            {
                return number;
            }
        }

        Console.WriteLine("Некорректное значение. Попробуйте ещё раз.");
    }
}
