using BankLimitManagementApp.Domain.Entities;
using BankLimitManagementApp.Domain.Repositories;
using BankLimitManagementApp.Infra.Persistense;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLimitManagementApp.Infra.Persistense.Repositories
{
    public class BankAccountRepository(ApplicationDbContext applicationDbContext) : IBankAccountRepository
    {
        private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;
        public async Task AddAccountAsync(BankAccount bankAccount)
        {
            await _applicationDbContext.BankAccounts.AddAsync(bankAccount);

            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<BankAccount> GetAccountByIdAsync(int id)
        {
            return await _applicationDbContext.BankAccounts.SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task DeleteAccountAsync(BankAccount bankAccount)
        {
            _applicationDbContext.Remove(bankAccount);
            await _applicationDbContext.SaveChangesAsync();
        }

        public void UpdateLimitAccount(BankAccount bankAccount)
        {
            _applicationDbContext.BankAccounts.Update(bankAccount);
            _applicationDbContext.SaveChanges();
        }

        public async Task<List<BankAccount>> GetAllBankAccounts(string? filter)
        {
            if(filter == null) return await _applicationDbContext.BankAccounts.ToListAsync();

            return await _applicationDbContext.BankAccounts
                .Where(b => b.Document.Contains(filter) || b.ClientName.Contains(filter) || b.AccountNumber.Contains(filter))
                .ToListAsync();
        }
    }
}
