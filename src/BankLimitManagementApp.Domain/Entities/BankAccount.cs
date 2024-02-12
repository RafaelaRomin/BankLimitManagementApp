namespace BankLimitManagementApp.Domain.Entities
{ 
    public class BankAccount: BaseEntity
    {
        public BankAccount() { }
        public BankAccount(string clientName, string document, string agencyNumber, string accountNumber, decimal transactionLimit, decimal totalAmount)
        {
            ClientName = clientName;
            Document = document;
            AgencyNumber = agencyNumber;
            AccountNumber = accountNumber;
            TransactionLimit = transactionLimit;
            TotalAmount = totalAmount;
        }

        public string ClientName { get;   set; }
        public string Document { get;  set; }
        public string AgencyNumber { get;  set; }
        public string AccountNumber { get;  set; }
        public decimal TransactionLimit { get;  set; }
        public decimal TotalAmount { get;  set; } 
        public DateTime? LastChange { get;  set; }

        public void UpdateTransactionLimit(decimal transactionLimit)
        {
            TransactionLimit = transactionLimit;
            LastChange = DateTime.Now;
        }
    }

   
}
