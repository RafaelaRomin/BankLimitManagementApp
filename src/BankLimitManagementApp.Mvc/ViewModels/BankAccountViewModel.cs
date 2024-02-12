namespace BankLimitManagementApp.Mvc.ViewModels
{
    public class BankAccountViewModel
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string Document { get; set; }
        public string AgencyNumber { get; set; }
        public string AccountNumber { get; set; }
        public decimal TransactionLimit { get; set; }
        public string? LastChange { get; set; }
    }
}
