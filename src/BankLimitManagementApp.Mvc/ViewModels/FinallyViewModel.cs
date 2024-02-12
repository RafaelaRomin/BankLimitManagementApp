namespace BankLimitManagementApp.Mvc.ViewModels
{
    public class FinallyViewModel
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public decimal TransactionLimit { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
