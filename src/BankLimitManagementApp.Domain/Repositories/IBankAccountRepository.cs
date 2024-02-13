using BankLimitManagementApp.Domain.Entities;

namespace BankLimitManagementApp.Domain.Repositories
{
    public interface IBankAccountRepository
    {
        Task<List<BankAccount>> GetAllBankAccounts(string? filter);
        Task<BankAccount> GetAccountByIdAsync(int id);
        Task AddAccountAsync(BankAccount bankAccount);
        void UpdateLimitAccount(BankAccount bankAccount);
        Task DeleteAccountAsync(BankAccount bankAccount);
    }
}
