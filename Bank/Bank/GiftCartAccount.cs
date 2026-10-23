namespace Bank;

/// <summary>
/// Подарочный счёт с ежемесячным пополнением.
/// </summary>
public class GiftCartAccount : BankAccount
{
    /// <summary>Сумма ежемесячного пополнения.</summary>
    private readonly decimal _monthlyDeposit = 0m;

    /// <summary>Создаёт подарочный счёт.</summary>
    /// <param name="name">Имя владельца.</param>
    /// <param name="initialBalance">Начальный баланс.</param>
    /// <param name="monthlyDeposit">Сумма ежемесячного пополнения (по умолчанию 0).</param>
    public GiftCartAccount(string name, decimal initialBalance, decimal monthlyDeposit = 0)
        : base(name, initialBalance) => _monthlyDeposit = monthlyDeposit;

    /// <summary>Вносит ежемесячный депозит, если он задан.</summary>
    public override void PerformMonthAndTransactions()
    {
        base.PerformMonthAndTransactions();

        if (_monthlyDeposit != 0)
        {
            MakeDeposite(_monthlyDeposit, DateTime.UtcNow, "Add monthly deposit");
        }
    }

    /// <summary>Возвращает строковое представление счёта с суммой ежемесячного пополнения.</summary>
    /// <returns>Строка с типом счёта, владельцем, номером и ежемесячным депозитом.</returns>
    public override string ToString()
    {
        return base.ToString() + $"\tmonthly deposit: {_monthlyDeposit}";
    }
}