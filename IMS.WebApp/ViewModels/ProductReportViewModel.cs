using IMS.CoreBusiness;

namespace IMS.WebApp.ViewModels
{
    public class ProductReportViewModel
    {
        public string ProductName { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public ProductTransactionType? TransactionType { get; set; }
    }
}
