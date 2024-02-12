using BankLimitManagementApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
