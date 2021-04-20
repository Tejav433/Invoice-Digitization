using System.Collections.Generic;
using InvoiceDigitization.Models;

namespace InvoiceDigitization.Services {
  public class InvoiceUpdateService: IInvoiceUpdateService {

    public InvoiceUpdateService() {

    }

    /// <summary>
    /// Update the invoice data
    /// </summary>
    /// <param name="invoiceNo"></param>
    /// <param name="invoiceData"></param>
    public void AddOrUpdateInvoiceData( string invoiceNo, Invoice invoiceData ) {
      InvoiceDataStore.Update( invoiceNo, invoiceData );
    }



    /// <summary>
    /// Update the status of invoice
    /// </summary>
    /// <param name="invoiceNo"></param>
    /// <param name="status"></param>
    public void UpdateInvoiceStatus( IEnumerable<string> invoiceNums, Status status ) {
      foreach ( var invoice in invoiceNums ) {
        InvoiceDataStore.UpdateStatus( invoice, status );
      }
    }
  }
}
