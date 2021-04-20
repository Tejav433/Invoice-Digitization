using System;
using System.Collections.Generic;

namespace InvoiceDigitization.Models {
  public class Invoice {

    public string InvoiceNo { get; set; }
    public DateTime InvoiceDate { get; set; }
    public Address Seller { get; set; }
    public Address BillTo { get; set; }
    public Address ShipTo { get; set; }
    public IEnumerable<Item> Items { get; set; }
    public double SubTotal { get; set; }
    public double Tax { get; set; }
    public double TotalAmount { get; set; }
    public Status Status { get; set; }




  }
}
