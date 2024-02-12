using BankLimitManagementApp.Domain.Entities;
using BankLimitManagementApp.Mvc.ViewModels;

namespace BankLimitManagementApp.Mvc.MappingViewModels
{
    public static class MappingTransactionAccountViewModel
    {
        public static IEnumerable<TransactionsAccountViewModel> ConvertTransactionAccountViewModel (this IEnumerable<TransactionAccount> transactionAccounts)
        {
            return (from transactionAccount in transactionAccounts
                    select new TransactionsAccountViewModel
                    {
                        Id = transactionAccount.Id,
                        ClientName = transactionAccount.BankAccount.ClientName,
                        Value = transactionAccount.Value,
                        TransactionDate = transactionAccount.TransactionDate.ToString("dd/MM/yyyy"),
                        TransactionStatus = transactionAccount.TransactionStatus,
                    }).ToList();
        }

        public static TransactionsAccountViewModel ConvertTransactionAccountViewModelById(this TransactionAccount transactionAccount)
        {
            return new TransactionsAccountViewModel
            {
                Id = transactionAccount.Id,
                ClientName = transactionAccount.BankAccount.ClientName,
                Document = transactionAccount.BankAccount.Document,
                AgencyNumber = transactionAccount.BankAccount.AgencyNumber,
                AccountNumber = transactionAccount.BankAccount.AccountNumber,
                Value = transactionAccount.Value,
                TransactionDate = transactionAccount.TransactionDate.ToString("dd/MM/yyyy"),
                TransactionStatus = transactionAccount.TransactionStatus
            };
        }
    }
}
