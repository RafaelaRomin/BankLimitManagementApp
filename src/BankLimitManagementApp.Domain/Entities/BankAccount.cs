using Amazon.DynamoDBv2.DataModel;

namespace BankLimitManagementApp.Domain.Entities
{
    [DynamoDBTable("BankAccount")]
    public class BankAccount
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

        [DynamoDBHashKey]
        public int Id { get; set; }
        [DynamoDBProperty]
        public string ClientName { get; private set; }
        [DynamoDBProperty]
        public string Document { get; private set; }
        [DynamoDBProperty]
        public string AgencyNumber { get; private set; }
        [DynamoDBProperty]
        public string AccountNumber { get; private set; }
        [DynamoDBProperty]
        public decimal TransactionLimit { get; private set; }
        [DynamoDBProperty]
        public decimal TotalAmount { get; private set; }
        [DynamoDBProperty]
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
