namespace Bank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccount account1 = new BankAccount("Yana",100000000000);
            BankAccount account2 = new BankAccount("Roman", 100);
            Console.WriteLine
                ($"account: {account1.Owner} {account1.Balance} {account1.Number}");
            Console.WriteLine
                ($"account: {account2.Owner} {account2.Balance} {account2.Number}");
            
            account1.MakeDeposit(1000, DateTime.UtcNow, ":)");
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
        }
    }
}
