using BankLimitManagementApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankLimitManagementApp.Infra.Persistense.Configurations
{
    public class TransactionAccountConfiguration : IEntityTypeConfiguration<TransactionAccount>
    {
        public void Configure(EntityTypeBuilder<TransactionAccount> builder)
        {
            builder
                .HasKey(x => x.Id);

            //builder
            //    .HasOne(a => a.BankAccount)
            //    .WithMany(t => t.Transactions)
            //    .HasForeignKey(a => a.BankAccountId);
                
        }
    }
}
