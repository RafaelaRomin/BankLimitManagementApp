using BankLimitManagementApp.Domain.Enums;

namespace BankLimitManagementApp.Mvc.ViewModels
{
    public class TransactionsAccountViewModel
    {
        public int Id { get; set; }
        public string ClientName { get;  set; }
        public string Document { get; set; }
        public string AgencyNumber { get; set; }
        public string AccountNumber { get; set; }
        public decimal Value { get; set; }
        public string TransactionDate { get; set; }
        public string? TransactionStatus { get; set; }
    }
}
