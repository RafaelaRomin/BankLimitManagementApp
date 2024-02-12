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
    public class TransactionAccountRepository(ApplicationDbContext applicationDbContext) : ITransactionAccountRepository
    {
        private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task<List<TransactionAccount>> GetAllTransactions()
        {
            return await _applicationDbContext.TransactionAccounts
                .Include(b => b.BankAccount)
                .ToListAsync();
        }

        public async Task<TransactionAccount> GetTransactionById(int id)
        {
            return await _applicationDbContext.TransactionAccounts
                .Include(b => b.BankAccount)
                .SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddTransaction(TransactionAccount transactionAccount)
        {
            await _applicationDbContext.TransactionAccounts.AddAsync(transactionAccount);

            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
