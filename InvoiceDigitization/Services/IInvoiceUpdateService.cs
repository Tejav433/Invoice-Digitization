using System.Collections.Generic;
using InvoiceDigitization.Models;

namespace InvoiceDigitization.Services {
  public interface IInvoiceUpdateService {

    void AddOrUpdateInvoiceData( string invoiceNo, Invoice invoiceData );

    void UpdateInvoiceStatus( IEnumerable<string> invoiceNo, Status status );
  }
}
