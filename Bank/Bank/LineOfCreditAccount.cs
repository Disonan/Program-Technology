namespace Bank;

/// <summary>
/// Кредитный счёт с лимитом и начислением процентов при отрицательном балансе.
/// </summary>
public class LineOfCreditAccount : BankAccount
{
    /// <summary>Создаёт кредитный счёт.</summary>
    /// <param name="name">Имя владельца.</param>
    /// <param name="initialBalance">Начальный баланс.</param>
    /// <param name="creditLimit">Кредитный лимит.</param>
    public LineOfCreditAccount(string name, decimal initialBalance, decimal creditLimit)
        : base(name, initialBalance, -creditLimit) { }

    /// <summary>Начисляет проценты при отрицательном балансе.</summary>
    public override void PerformMonthAndTransactions()
    {
        base.PerformMonthAndTransactions();

        if (Balance < 0)
        {
            decimal interest = -Balance * 0.07m;
            MakeWithdrawal(interest, DateTime.UtcNow, "Charge monthly interest");
        }
    }

    /// <summary>Возвращает транзакцию комиссии за овердрафт при превышении лимита.</summary>
    /// <param name="isOverdrawn">Признак превышения лимита.</param>
    /// <returns>Транзакция комиссии за овердрафт или null, если превышения нет.</returns>
    protected override Transaction? CheckWithdrawalLimit(bool isOverdrawn)
        => isOverdrawn ? new Transaction(-20, DateTime.UtcNow, "apply overdraft") : default;
}