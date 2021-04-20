using System;
using System.Collections.Concurrent;
using System.Linq;
using InvoiceDigitization.Models;

namespace InvoiceDigitization {
  public sealed class InvoiceDataStore {

    private static ConcurrentDictionary<string, Invoice> _invoiceData;

    private InvoiceDataStore() { }


    /// <summary>
    /// Concurrent Dictionary to handle Multiple Threads;
    /// </summary>
    public static ConcurrentDictionary<string, Invoice> InvoiceData {
      get {
        return _invoiceData ??= new ConcurrentDictionary<string, Invoice>();
      }
    }


    /// <summary>
    /// To Add the new extracted invoice data
    /// </summary>
    /// <param name="invoiceData"></param>
    public static void Add( Invoice invoiceData ) {
      if ( _invoiceData.ContainsKey( invoiceData.InvoiceNo ) ) {
        _invoiceData[invoiceData.InvoiceNo] = invoiceData;
      }
      else {
        _invoiceData.TryAdd( invoiceData.InvoiceNo, invoiceData );
      }
    }


    /// <summary>
    /// To Update the invoice data;
    /// </summary>
    /// <param name="invoiceNo"></param>
    /// <param name="newInvoiceData"></param>
    public static void Update( string invoiceNo, Invoice newInvoiceData ) {
      if ( _invoiceData.ContainsKey( invoiceNo ) ) {
        UpdateHelper<Invoice>( _invoiceData[invoiceNo], newInvoiceData );
        if ( newInvoiceData.Items != null && newInvoiceData.Items.Count() > 0 )
          _invoiceData[invoiceNo].Items = newInvoiceData.Items;
        UpdateTotalAmount( _invoiceData[invoiceNo] );
      }
      else {
        Add( newInvoiceData );
      }
    }



    /// <summary>
    /// To Update the status of Invoice parsing
    /// </summary>
    /// <param name="invoiceNo"></param>
    /// <param name="status"></param>
    public static void UpdateStatus( string invoiceNo, Status status ) {
      if ( _invoiceData.ContainsKey( invoiceNo ) )
        _invoiceData[invoiceNo].Status = status;
    }


    /// <summary>
    /// To Update Invoice data helper
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="oldData"></param>
    /// <param name="newData"></param>
    private static void UpdateHelper<T>( T oldData, T newData ) {

      var oldProps = oldData.GetType().GetProperties();
      var newProps = newData.GetType().GetProperties();

      for ( int i = 0; i < newProps.Length; i++ ) {

        var value = newProps[i].GetValue( newData );
        if ( newProps[i].PropertyType == typeof( string ) && !string.IsNullOrEmpty( (string) value ) ) {
          oldProps[i].SetValue( oldData, value );
        }
        else if ( newProps[i].PropertyType == typeof( int ) && (int) value != 0 ) {
          oldProps[i].SetValue( oldData, value );
        }
        else if ( newProps[i].PropertyType == typeof( double ) && (double) value != 0 ) {
          oldProps[i].SetValue( oldData, value );
        }
        else if ( newProps[i].PropertyType == typeof( DateTime ) && (DateTime) value != null ) {
          oldProps[i].SetValue( oldData, value );
        }
        else if ( newProps[i].PropertyType == typeof( Address ) ) {
          var oldValue = oldProps[i].GetValue( oldData );
          UpdateHelper<Address>( (Address) oldValue, (Address) value );
          oldProps[i].SetValue( oldData, oldValue );
        }

      }
    }



    /// <summary>
    /// Update Total Amount and SubTotal
    /// </summary>
    /// <param name="data"></param>
    private static void UpdateTotalAmount( Invoice data ) {
      foreach ( var item in data.Items ) {
        data.SubTotal += item.Amount;
      }
      data.TotalAmount = data.SubTotal + data.Tax;
    }
  }

}

