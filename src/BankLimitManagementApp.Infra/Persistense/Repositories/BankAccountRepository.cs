using Amazon.DynamoDBv2.DataModel;
using BankLimitManagementApp.Domain.Entities;
using BankLimitManagementApp.Domain.Repositories;

namespace BankLimitManagementApp.Infra.Persistense.Repositories
{
    public class BankAccountRepository(IDynamoDBContext dbContext) : IBankAccountRepository
    {
        public async Task AddAccountAsync(BankAccount bankAccount)
        {
            await dbContext.SaveAsync(bankAccount);
        }

        public async Task<BankAccount> GetAccountByIdAsync(int id)
        {
            return await dbContext.LoadAsync<BankAccount>(id);
        }

        public async Task DeleteAccountAsync(BankAccount bankAccount)
        {
            await dbContext.DeleteAsync<BankAccount>(bankAccount.Id);
        }

        public void UpdateLimitAccount(BankAccount bankAccount)
        {
            dbContext.SaveAsync(bankAccount);
        }

        public async Task<List<BankAccount>> GetAllBankAccounts(string? filter)
        {
            if (filter == null)
            {
                return await dbContext.ScanAsync<BankAccount>(new List<ScanCondition>()).GetRemainingAsync();
            }

            var listBankAccount = await dbContext.ScanAsync<BankAccount>(new List<ScanCondition>()).GetRemainingAsync();

            return listBankAccount
                .Where(b =>
                    b.Document.Contains(filter, StringComparison.InvariantCultureIgnoreCase) ||
                    b.ClientName.Contains(filter, StringComparison.InvariantCultureIgnoreCase) ||
                    b.AccountNumber.Contains(filter, StringComparison.InvariantCultureIgnoreCase))
                .ToList();
        }
    }
}
