using System.ComponentModel.DataAnnotations;

namespace BankLimitManagementApp.Mvc.InputModels
{
    public class EditBankAccountInputModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public decimal TransactionLimit { get; set; }
    }
}
