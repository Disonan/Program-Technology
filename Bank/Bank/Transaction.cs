namespace Bank;

/// <summary>
/// Неизменяемая запись об операции по счёту.
/// </summary>
public record Transaction
{
    /// <summary>Уникальный идентификатор транзакции.</summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>Идентификатор парной транзакции (для перевода между счетами).</summary>
    public Guid? RelatedTransactionId { get; init; }

    /// <summary>Сумма операции (отрицательная — списание).</summary>
    public decimal Amount { get; init; }

    /// <summary>Дата операции.</summary>
    public DateTime Date { get; init; }

    /// <summary>Комментарий к операции.</summary>
    public string Note { get; init; }

    /// <summary>Создаёт запись об операции.</summary>
    /// <param name="amount">Сумма операции (отрицательная — списание).</param>
    /// <param name="date">Дата операции.</param>
    /// <param name="note">Комментарий к операции.</param>
    public Transaction(decimal amount, DateTime date, string note)
    {
        Amount = amount;
        Date = date;
        Note = note;
    }
}