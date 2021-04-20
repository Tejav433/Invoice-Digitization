using System.Collections.Generic;
using InvoiceDigitization.Models;

namespace InvoiceDigitization.Services {
  public interface IInvoiceService {

    bool ProcessInvoice( IEnumerable<string> invoiceFiles );
    string GetInvoiceStatus( string invoiceNo );
    Invoice GetInvoiceData( string invoiceNo );
    IEnumerable<Invoice> GetAllInvoicesData();

    IEnumerable<string> GetAllInvoiceNums();
  }
}
