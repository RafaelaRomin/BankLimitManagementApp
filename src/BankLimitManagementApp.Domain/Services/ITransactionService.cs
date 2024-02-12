using BankLimitManagementApp.Domain.Entities;

namespace BankLimitManagementApp.Domain.Services
{
    public interface ITransactionService
    {
        bool CheckLimitIsValid(BankAccount bankAccount, TransactionAccount transactionAccount);
        void DebitValueAccount(BankAccount bankAccount, TransactionAccount transactionAccount);
    }
}
