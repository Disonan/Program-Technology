using System.Text;

namespace Bank;

// BankAccoun - потомок класса object => можно переопределить 
/// <summary>
/// Банковский счет
/// <summary>

public class BankAccount
{
    /// <summary>
    /// минимальный допустимый баланс счета
    /// </summary>
    private readonly decimal _minimumBalance;
    /// <summary>
    /// список всех транзакций
    ///   </summary>
    private List<Transaction> _allTransactions = new List<Transaction>();


    public string Owner { get; private set; }
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
    public string Number { get; }
    private static int s_accountNumberSeed = 1000000000;
    public BankAccount(string name, decimal initialBalance) : this(name, initialBalance, 0) { }
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
    public void MakeDeposite(decimal amount, DateTime date, string note)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount off deposite must be positive");
        }
        var deposite = new Transaction(amount, date, note);
        _allTransactions.Add(deposite);
    }

    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
        Transaction? overdraftTransaction = CheckWithdrawalLimit(Balance - amount < _minimumBalance);
        Transaction? withdrawal = new(-amount, date, note);
        _allTransactions.Add(withdrawal);

        if (overdraftTransaction is not null)
        {
            _allTransactions.Add(overdraftTransaction);
        }
    }

    protected virtual Transaction? CheckWithdrawalLimit(bool v)
    {
        if (v)
        {
            throw new InvalidOperationException
                ("Not sufficient rubls for this withdrawal");
        }
        return default;
    }

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



    // ключевое слово virtual позволяет в дочерном классе представить другую реализацию метода PerformMonthAndTransactions()

    public virtual void PerformMonthAndTransactions()

    {

    }
    // переопрелеояем метод который унаследовали от object 
    // этот метод должен возвращать строку с состоянием объекта 
    //public override string ToString()
    //{
    //    return $"Type: Owner: {Owner}\tNumber of account: {Number}";
    //}
    public override string ToString()
        => $"Type: {GetType().Name}\tOwner: {Owner}\tNumber of account: {Number}";
}
