using BankLimitManagementApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLimitManagementApp.Domain.Repositories
{
    public interface ITransactionAccountRepository
    {
        Task<List<TransactionAccount>> GetAllTransactions();
        Task<TransactionAccount> GetTransactionById(int id);
        Task AddTransaction(TransactionAccount transactionAccount);
    }
}
