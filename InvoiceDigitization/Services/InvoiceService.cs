using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using InvoiceDigitization.Models;

namespace InvoiceDigitization.Services {
  public class InvoiceService: IInvoiceService {

    private readonly ConcurrentDictionary<string, Invoice> _invoiceStore;

    public InvoiceService() {
      _invoiceStore = InvoiceDataStore.InvoiceData;
    }


    /// <summary>
    /// Returns the Invoice data for the given InvoiceNo
    /// </ummary>
    /// <param name="invoiceNo">Invoice No</param>
    /// <returns></returns>
    public Invoice GetInvoiceData( string invoiceNo ) {
      if ( _invoiceStore.ContainsKey( invoiceNo ) ) {
        return _invoiceStore[invoiceNo];
      }
      return null;
    }


    /// <summary>
    /// Returns the Status of the Invoice
    /// </summary>
    /// <param name="invoiceNo"></param>
    /// <returns></returns>
    public String GetInvoiceStatus( string invoiceNo ) {
      if ( _invoiceStore.ContainsKey( invoiceNo ) ) {
        return _invoiceStore[invoiceNo].Status.ToString();
      }
      return Status.NotProcessed.ToString();
    }



    /// <summary>
    /// Parsing the invoice file and extract and stores the data 
    /// Actual Parsing Need to be implemented
    /// </summary>
    /// <param name="filePath"> Invoice file path</param>
    /// <returns></returns>
    public bool ProcessInvoice( IEnumerable<string> invoiceFiles ) {
      try {
        
        foreach ( var file in invoiceFiles ) {
          var data = MockDataHelper.GetInvoiceData();
          data.Status = Status.Digitized;
          InvoiceDataStore.Add( data );
        }
        return true;
      }
      catch ( Exception ) {
        return false;
      }

    }

    /// <summary>
    /// To get all the invoices data
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Invoice> GetAllInvoicesData() {
      var invoicesData = new List<Invoice>();
      foreach ( var d in _invoiceStore.Values ) {
        invoicesData.Add( d );
      }
      return invoicesData;
    }


    /// <summary>
    /// Get all the invoice numbers
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> GetAllInvoiceNums() {
      var invoicesData = new List<string>();
      foreach ( var d in _invoiceStore.Keys ) {
        invoicesData.Add( d );
      }
      return invoicesData;
    }
  }
}
