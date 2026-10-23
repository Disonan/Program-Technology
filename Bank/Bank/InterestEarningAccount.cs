namespace Bank;

/// <summary>
/// Сберегательный счёт с начислением процентов на остаток.
/// </summary>
public class InterestEarningAccount : BankAccount
{
    /// <summary>Создаёт сберегательный счёт.</summary>
    /// <param name="name">Имя владельца.</param>
    /// <param name="initialBalance">Начальный баланс.</param>
    public InterestEarningAccount(string name, decimal initialBalance) : base(name, initialBalance)
    { }

    /// <summary>Начисляет ежемесячные проценты на остаток свыше 500.</summary>
    public override void PerformMonthAndTransactions()
    {
        base.PerformMonthAndTransactions();

        if (Balance > 500m)
        {
            decimal interest = Balance * 0.02m;
            MakeDeposite(interest, DateTime.UtcNow, "Apply month interest");
        }
    }
}