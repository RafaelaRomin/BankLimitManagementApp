namespace BankLimitManagementApp.Domain.Entities
{
    public class BankAccount : BaseEntity
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

        public string ClientName { get; private set; }
        public string Document { get; private set; }
        public string AgencyNumber { get; private set; }
        public string AccountNumber { get; private set; }
        public decimal TransactionLimit { get; private set; }
        public decimal TotalAmount { get; private set; }
        public DateTime? LastChange { get; private set; }

        public void UpdateTransactionLimit(decimal transactionLimit)
        {
            TransactionLimit = transactionLimit;
            LastChange = DateTime.Now;
        }

        public void ChangeLimitAndTotalAmount(decimal transactionLimit, decimal totalAmount)
        {
            TransactionLimit -= transactionLimit;
            TotalAmount -= totalAmount;
        }
    }
}
