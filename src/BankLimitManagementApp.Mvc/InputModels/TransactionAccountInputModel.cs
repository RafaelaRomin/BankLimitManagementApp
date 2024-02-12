using BankLimitManagementApp.Mvc.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace BankLimitManagementApp.Mvc.InputModels
{
    public class TransactionAccountInputModel
    {
        [Required]
        public int BankAccountId { get; set; }
        [Required]
        public decimal Value { get; set; }       
        public IEnumerable<BankAccountViewModel>? BankAccounts { get; set; }
    }
}
