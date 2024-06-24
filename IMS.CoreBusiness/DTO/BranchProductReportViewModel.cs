public class BranchProductReportViewModel
{
    public string ProductName { get; set; }
    public Dictionary<string, int> BranchQuantities { get; set; } = new Dictionary<string, int>();
}
