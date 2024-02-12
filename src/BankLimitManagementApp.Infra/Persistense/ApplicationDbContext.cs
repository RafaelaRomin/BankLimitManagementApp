
using BankLimitManagementApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLimitManagementApp.Infra.Persistense
{
    public class ApplicationDbContext : DbContext    
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<TransactionAccount> TransactionAccounts { get; set; }
    }
}
