namespace Portfolio.Admin.Models;

public class PoData
{
    public string PoNumber { get; set; } = string.Empty;
    public string VendorName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
