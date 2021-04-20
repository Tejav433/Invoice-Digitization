namespace InvoiceDigitization.Models {
  public class Item {

    public int Id { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public double UnitCost { get; set; }
    public double Amount { get; set; }
  }
}
