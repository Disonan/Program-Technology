using Bank;

namespace Bank
{
    /// <summary>Точка входа в программу.</summary>
    public class Program
    {
        /// <summary>Запускает демонстрацию работы банковских счетов.</summary>
        /// <param name="args">Аргументы командной строки.</param>
        static void Main(string[] args)
        {
            BankAccount account1 = new BankAccount("Yana", 100000000000);
            BankAccount account2 = new BankAccount("Roman", 100);
            Console.WriteLine
                ($"account: {account1.Owner} {account1.Balance} {account1.Number}");
            Console.WriteLine
                ($"account: {account2.Owner} {account2.Balance} {account2.Number}");

            account1.MakeDeposite(1000, DateTime.UtcNow, ":)");
            Console.WriteLine(account1.Balance);

            account1.MakeWithdrawal(100, DateTime.UtcNow, ":(");
            Console.WriteLine(account1.Balance);

            try
            {
                account2.MakeWithdrawal(1000, DateTime.UtcNow, "&&&");
                Console.WriteLine(account2.Balance);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }

            InterestEarningAccount interestEarning = new("pey ", 1000m);
            interestEarning.MakeDeposite(100m, DateTime.UtcNow, ";)");
            interestEarning.MakeWithdrawal(10m, DateTime.UtcNow, ";)");
            interestEarning.PerformMonthAndTransactions();
            Console.WriteLine(interestEarning);
            Console.WriteLine(interestEarning.GetAccountHistory());

            GiftCartAccount giftCart = new("pey", 1000m, 5000m);
            Console.WriteLine(giftCart);

            LineOfCreditAccount lineOfCredit = new("pey", 0m, 5000m);
            lineOfCredit.MakeWithdrawal(500m, DateTime.UtcNow, ";(");
            Console.WriteLine(lineOfCredit);

            // Демонстрация статусов (задача 1)
            Console.WriteLine();
            Console.WriteLine("=== Статусы ===");

            BankAccount regular = new("Regular", 1000m) { Status = AccountStatus.Regular };
            BankAccount premium = new("Premium", 50000m) { Status = AccountStatus.Premium };
            BankAccount vip = new("VIP", 50000m) { Status = AccountStatus.VIP };

            regular.PerformMonthAndTransactions();
            premium.PerformMonthAndTransactions();
            vip.MakeWithdrawal(1000m, DateTime.UtcNow, "VIP purchase");
            vip.PerformMonthAndTransactions();

            Console.WriteLine($"Regular balance: {regular.Balance}");
            Console.WriteLine($"Premium balance: {premium.Balance}");
            Console.WriteLine($"VIP balance: {vip.Balance}");

            // Демонстрация перевода (задача 3)
            Console.WriteLine();
            Console.WriteLine("=== Перевод ===");
            var transferService = new TransferService();
            decimal before1 = account1.Balance;
            decimal before2 = account2.Balance;
            transferService.Transfer(account1, account2, 500m, DateTime.UtcNow, "test transfer");
            Console.WriteLine($"account1: {before1} -> {account1.Balance}");
            Console.WriteLine($"account2: {before2} -> {account2.Balance}");

            List<BankAccount> accounts = new List<BankAccount>();
            accounts.Add(account1);
            accounts.Add(interestEarning);
            accounts.Add(lineOfCredit);
            accounts.Add(giftCart);

            foreach (BankAccount account in accounts)
            {
                Console.WriteLine(account);
                account.PerformMonthAndTransactions();
                Console.WriteLine(account.GetAccountHistory());
            }
        }
    }

    /// <summary>Сервис перевода средств между счетами.</summary>
    public class TransferService
    {
        /// <summary>Переводит средства со счёта-источника на счёт-получатель.</summary>
        /// <param name="from">Счёт-источник.</param>
        /// <param name="to">Счёт-получатель.</param>
        /// <param name="amount">Сумма перевода.</param>
        /// <param name="date">Дата операции.</param>
        /// <param name="note">Комментарий к операции.</param>
        /// <exception cref="InvalidOperationException">Выбрасывается, если списание или зачисление не удалось.</exception>
        public void Transfer(BankAccount from, BankAccount to, decimal amount, DateTime date, string note)
        {
            from.MakeWithdrawal(amount, date, $"Transfer to {to.Number}: {note}");
            try
            {
                to.MakeDeposite(amount, date, $"Transfer from {from.Number}: {note}");
            }
            catch
            {
                from.MakeDeposite(amount, date, $"Rollback transfer to {to.Number}");
                throw;
            }
        }
    }
}