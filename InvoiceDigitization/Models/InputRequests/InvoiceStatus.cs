using System.Collections.Generic;

namespace InvoiceDigitization.Models.InputRequests {
  public class InvoiceStatus {
    public IEnumerable<string> InvoiceNo { get; set; }
    public Status Status { get; set; }
  }
}
