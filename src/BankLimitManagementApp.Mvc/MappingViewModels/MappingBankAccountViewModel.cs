using BankLimitManagementApp.Domain.Entities;
using BankLimitManagementApp.Mvc.ViewModels;

namespace BankLimitManagementApp.Mvc.MappingViewModels
{
    public static class MappingBankAccountViewModel
    {
        public static IEnumerable<BankAccountViewModel> ConvertBankAccountViewModel (this IEnumerable<BankAccount> bankAccounts)
        {
            return (from bankAccount in bankAccounts
                    select new BankAccountViewModel
                    {
                        Id = bankAccount.Id,
                        ClientName = bankAccount.ClientName,
                        Document = bankAccount.Document,
                        AgencyNumber = bankAccount.AgencyNumber,
                        AccountNumber = bankAccount.AccountNumber,
                        TransactionLimit = bankAccount.TransactionLimit,
                        LastChange = bankAccount.LastChange?.ToString("dd/MM/yyyy"),
                    }).ToList();
        }

        public static BankAccountViewModel ConvertBankAccountViewModelById (this BankAccount bankAccount)
        {
            return new BankAccountViewModel
            {
                Id = bankAccount.Id,
                ClientName = bankAccount.ClientName,
                Document = bankAccount.Document,
                AgencyNumber = bankAccount.AgencyNumber,
                AccountNumber = bankAccount.AccountNumber,
                TransactionLimit = bankAccount.TransactionLimit,
                LastChange = bankAccount.LastChange?.ToString("dd/MM/yyyy"),
            };
        }

        public static FinallyViewModel ConvertFinallyViewModel (this BankAccount bankAccount)
        {
            return new FinallyViewModel
            {
                Id = bankAccount.Id,
                ClientName = bankAccount.ClientName,
                TransactionLimit = bankAccount.TransactionLimit,
                TotalAmount = bankAccount.TotalAmount,
            };
        }
    }
}
