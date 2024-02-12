using BankLimitManagementApp.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BankLimitManagementApp.Domain.Entities
{
    public class TransactionAccount : BaseEntity
    {
        public TransactionAccount() { }

        public TransactionAccount(int bankAccountId, decimal value)
        {
            BankAccountId = bankAccountId;
            Value = value;
            TransactionDate = DateTime.Now;
            TransactionStatus = TransactionStatusEnum.Pending;
        }

        public int BankAccountId { get;  private set; } 
        public decimal Value { get;  private set; } 
        public DateTime TransactionDate { get;  private set; } 
        public TransactionStatusEnum? TransactionStatus { get;  private set; }
        public BankAccount BankAccount { get;  private set; }

        public void SetTransactionApproved()
        {
            TransactionStatus = TransactionStatusEnum.Approved;
        }

        public void SetTransactionDenied()
        {
            TransactionStatus = TransactionStatusEnum.Denied;
        }
    }
}
