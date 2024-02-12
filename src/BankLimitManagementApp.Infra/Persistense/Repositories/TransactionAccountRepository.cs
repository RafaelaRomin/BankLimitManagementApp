using Amazon.DynamoDBv2.DataModel;
using BankLimitManagementApp.Domain.Entities;
using BankLimitManagementApp.Domain.Repositories;

namespace BankLimitManagementApp.Infra.Persistense.Repositories
{
    public class TransactionAccountRepository(IDynamoDBContext dbContext) : ITransactionAccountRepository
    {
        public async Task<List<TransactionAccount>> GetAllTransactions()
        {
            var transactions = await dbContext.ScanAsync<TransactionAccount>(new List<ScanCondition>()).GetRemainingAsync();

            var bankAccountIds = transactions.Select(t => t.BankAccountId).Distinct().ToList();

            var bankAccounts = new List<BankAccount>();

            foreach (var bankAccountId in bankAccountIds)
            {
                var bankAccount = await dbContext.LoadAsync<BankAccount>(bankAccountId);

                if (bankAccount != null)
                {
                    bankAccounts.Add(bankAccount);
                }
            }

            foreach (var transaction in transactions)
            {
                var associatedBankAccount = bankAccounts.SingleOrDefault(b => b.Id == transaction.BankAccountId);

                if (associatedBankAccount != null)
                {
                    transaction.BankAccount = associatedBankAccount;
                }
            }

            return transactions;
        }

        public async Task<TransactionAccount> GetTransactionById(int id)
        {
            var transaction = await dbContext.LoadAsync<TransactionAccount>(id);

            var bankAccount = await dbContext.LoadAsync<BankAccount>(transaction.BankAccountId);

            transaction.BankAccount = bankAccount;

            return transaction;
        }

        public async Task AddTransaction(TransactionAccount transactionAccount)
        {
            await dbContext.SaveAsync(transactionAccount);
        }
    }
}

