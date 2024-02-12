using System.ComponentModel.DataAnnotations;

namespace BankLimitManagementApp.Mvc.InputModels
{
    public class BankAccountInputModel
    {
        [Required]
        public string ClientName { get; set; }
        [Required]
        public string Document { get; set; }
        [Required]
        public string AgencyNumber { get; set; }
        [Required]
        public string AccountNumber { get; set; }
        [Required]
        public decimal TransactionLimit { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
    }
}
