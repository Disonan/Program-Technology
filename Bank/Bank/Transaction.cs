namespace Bank;
// мы создали благодаря record неизменяемый тип данных
internal record Transaction(decimal Amount, DateTime Date, string Note);


//internal record Transaction
//{
//    public decimal Amount { get; }
//    public DateTime Date { get; }
//    public string Note { get; }
//    public Transaction(decimal Amount, DateTime Date, string Note)
//    {
//        this.Note = Note;
//        this.Amount = Amount;
//        this.Date = Date;
//    }
//}

