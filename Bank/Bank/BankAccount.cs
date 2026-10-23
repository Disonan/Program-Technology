using System.Text;

namespace Bank;

/// <summary>Статус клиента банка.</summary>
public enum AccountStatus { Regular, Premium, VIP }

/// <summary>
/// Банковский счёт с поддержкой операций пополнения и списания.
/// </summary>
public class BankAccount
{
    /// <summary>Минимальный допустимый баланс счёта.</summary>
    private readonly decimal _minimumBalance;

    /// <summary>Список всех транзакций по счёту.</summary>
    private List<Transaction> _allTransactions = new List<Transaction>();

    /// <summary>Владелец счёта.</summary>
    public string Owner { get; private set; }

    /// <summary>Текущий баланс, вычисляемый по всем транзакциям.</summary>
    public decimal Balance
    {
        get
        {
            decimal balance = 0;
            foreach (var transaction in _allTransactions)
            {
                balance += transaction.Amount;
            }
            return balance;
        }
    }

    /// <summary>Номер счёта.</summary>
    public string Number { get; }

    /// <summary>Статус клиента.</summary>
    public AccountStatus Status { get; set; } = AccountStatus.Regular;

    /// <summary>Счётчик для генерации номеров счетов.</summary>
    private static int s_accountNumberSeed = 1000000000;

    /// <summary>Создаёт счёт с нулевым минимальным балансом.</summary>
    /// <param name="name">Имя владельца.</param>
    /// <param name="initialBalance">Начальный баланс.</param>
    public BankAccount(string name, decimal initialBalance) : this(name, initialBalance, 0) { }

    /// <summary>Создаёт счёт с заданным минимальным балансом.</summary>
    /// <param name="name">Имя владельца.</param>
    /// <param name="initialBalance">Начальный баланс.</param>
    /// <param name="minimumBalance">Минимально допустимый баланс.</param>
    public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
    {
        Owner = name;
        _minimumBalance = minimumBalance;
        if (initialBalance > 0)
        {
            MakeDeposite(initialBalance, DateTime.UtcNow, "initial balance");
        }
        Number = s_accountNumberSeed.ToString();
        s_accountNumberSeed++;
    }

    /// <summary>Добавляет транзакцию в список и журнал аудита.</summary>
    /// <param name="transaction">Транзакция для добавления.</param>
    protected void AddTransaction(Transaction transaction)
    {
        _allTransactions.Add(transaction);
        AuditLog.Log(Number, transaction);
    }

    /// <summary>Пополняет счёт на указанную сумму и регистрирует транзакцию.</summary>
    /// <param name="amount">Сумма пополнения. Должна быть положительной.</param>
    /// <param name="date">Дата операции.</param>
    /// <param name="note">Комментарий к операции.</param>
    /// <exception cref="ArgumentOutOfRangeException">Выбрасывается, если сумма отрицательна.</exception>
    public void MakeDeposite(decimal amount, DateTime date, string note)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount off deposite must be positive");
        }
        var deposite = new Transaction(amount, date, note);
        AddTransaction(deposite);
    }

    /// <summary>Списывает сумму со счёта и применяет комиссию при превышении лимита.</summary>
    /// <param name="amount">Сумма списания. Должна быть положительной.</param>
    /// <param name="date">Дата операции.</param>
    /// <param name="note">Комментарий к операции.</param>
    /// <exception cref="ArgumentOutOfRangeException">Выбрасывается, если сумма меньше или равна нулю.</exception>
    /// <exception cref="InvalidOperationException">Выбрасывается при недостатке средств.</exception>
    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
        Transaction? overdraftTransaction = CheckWithdrawalLimit(Balance - amount < _minimumBalance);
        Transaction? withdrawal = new(-amount, date, note);
        AddTransaction(withdrawal);

        if (overdraftTransaction is not null)
        {
            AddTransaction(overdraftTransaction);
        }
    }

    /// <summary>Проверяет, не превышен ли лимит списания.</summary>
    /// <param name="isOverdrawn">Признак превышения лимита.</param>
    /// <returns>Транзакция комиссии за овердрафт или null, если превышения нет.</returns>
    /// <exception cref="InvalidOperationException">Выбрасывается при недостатке средств.</exception>
    protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
    {
        if (isOverdrawn)
        {
            throw new InvalidOperationException
                ("Not sufficient rubls for this withdrawal");
        }
        return default;
    }

    /// <summary>Возвращает историю операций по счёту в виде строки.</summary>
    /// <returns>Отчёт с датой, суммой, балансом и комментарием по каждой операции.</returns>
    public string GetAccountHistory()
    {
        var report = new StringBuilder();

        decimal balance = 0;
        report.AppendLine("Data\t\tAmount\tBalance\tNote");
        foreach (var item in _allTransactions)
        {
            balance += item.Amount;
            report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Note}");
        }
        return report.ToString();
    }

    /// <summary>Начисляет проценты и списывает комиссию согласно статусу клиента.</summary>
    public virtual void PerformMonthAndTransactions()
    {
        switch (Status)
        {
            case AccountStatus.Regular:
                MakeWithdrawal(50m, DateTime.UtcNow, "Monthly fee");
                break;

            case AccountStatus.Premium:
                if (Balance > 10000m)
                {
                    MakeDeposite((Balance - 10000m) * 0.01m, DateTime.UtcNow, "Premium interest");
                }
                break;

            case AccountStatus.VIP:
                if (Balance > 10000m)
                {
                    MakeDeposite((Balance - 10000m) * 0.03m, DateTime.UtcNow, "VIP interest");
                }
                ApplyCashback();
                break;
        }
    }

    /// <summary>Начисляет кэшбэк 5% на все списания за текущий месяц.</summary>
    private void ApplyCashback()
    {
        var monthStart = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
        decimal withdrawals = 0;
        foreach (var t in _allTransactions)
        {
            if (t.Amount < 0 && t.Date >= monthStart)
            {
                withdrawals += -t.Amount;
            }
        }
        if (withdrawals > 0)
        {
            MakeDeposite(withdrawals * 0.05m, DateTime.UtcNow, "VIP cashback");
        }
    }

    /// <summary>Возвращает строковое представление счёта.</summary>
    /// <returns>Строка с типом счёта, владельцем, номером и статусом.</returns>
    public override string ToString()
        => $"Type: {GetType().Name}\tOwner: {Owner}\tNumber of account: {Number}\tStatus: {Status}";
}

/// <summary>Журнал аудита операций по счетам.</summary>
public static class AuditLog
{
    /// <summary>Записывает операцию в журнал аудита.</summary>
    /// <param name="accountNumber">Номер счёта.</param>
    /// <param name="transaction">Транзакция для записи.</param>
    public static void Log(string accountNumber, Transaction transaction)
    {
        Console.WriteLine(
            $"[AUDIT] Account: {accountNumber}, Amount: {transaction.Amount}, Date: {transaction.Date}, Note: {transaction.Note}");
    }
}