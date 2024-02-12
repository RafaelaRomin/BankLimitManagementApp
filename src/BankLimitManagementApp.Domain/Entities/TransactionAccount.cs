using Amazon.DynamoDBv2.DataModel;
using BankLimitManagementApp.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BankLimitManagementApp.Domain.Entities
{
    [DynamoDBTable("TransactionAccount")]
    public class TransactionAccount 
    {
        public TransactionAccount() { }

        public TransactionAccount(int bankAccountId, decimal value)
        {
            BankAccountId = bankAccountId;
            Value = value;
            TransactionDate = DateTime.Now;
            TransactionStatus = TransactionStatusEnum.Pending.ToString();
        }

        [DynamoDBHashKey]
        public int Id { get; set; }
        [DynamoDBProperty]
        public int BankAccountId { get;  private set; }
        [DynamoDBProperty]
        public decimal Value { get;  private set; }
        [DynamoDBProperty]  
        public DateTime TransactionDate { get;  private set; }
        [DynamoDBProperty]
        public string? TransactionStatus { get;  private set; }
        public BankAccount BankAccount { get;  set; }

        public void SetTransactionApproved()
        {
            TransactionStatus = TransactionStatusEnum.Approved.ToString();
        }

        public void SetTransactionDenied()
        {
            TransactionStatus = TransactionStatusEnum.Denied.ToString();
        }
    }
}
