using BankLimitManagementApp.Domain.Entities;
using BankLimitManagementApp.Domain.Repositories;
using BankLimitManagementApp.Domain.Services;

namespace BankLimitManagementApp.Mvc.Services
{
    public class TransactionService : ITransactionService
    {
        public bool CheckLimitIsValid(BankAccount bankAccount, TransactionAccount transactionAccount)
        {
            if (bankAccount.TransactionLimit <= transactionAccount.Value)
            {
                throw new Exception("O valor transacionado é maior que o permitido, contate sua agência");
            }

            if (bankAccount.TotalAmount <= transactionAccount.Value)
            {
                throw new Exception("O valor transacionado não está disponivel em conta!");
            }

            return true;

        }

        public void DebitValueAccount(BankAccount bankAccount, TransactionAccount transactionAccount)
        {
            bankAccount.TransactionLimit -= transactionAccount.Value;
            bankAccount.TotalAmount -= transactionAccount.Value;
        }
    }
}
