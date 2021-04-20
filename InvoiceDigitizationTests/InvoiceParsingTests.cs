using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using InvoiceDigitization;
using InvoiceDigitization.Models;
using InvoiceDigitization.Services;
using NUnit.Framework;

namespace InvoiceDigitizationTests {
  public class Tests {

    private ConcurrentDictionary<string, Invoice> _invoiceStore;
    private IInvoiceService _invoiceService;

    [OneTimeSetUp]
    [SetUp]
    public void Setup() {
      _invoiceService = new InvoiceService();
      _invoiceStore = InvoiceDataStore.InvoiceData;
      Assert.NotNull( _invoiceStore );
      Assert.NotNull( _invoiceService );
    }

    [Test]
    public void Test1() {
      _invoiceService.ProcessInvoice( new List<string> { "C:\\users\\invoice1.pdf" } );
      Assert.That( _invoiceStore.Count > 0 );

    }

    [Test]
    public void Test2() {
      _invoiceService.ProcessInvoice( new List<string> { "C:\\users\\invoice1.pdf", "C:\\users\\invoice2.pdf" } );
      Assert.That( _invoiceStore.Count > 1 );

    }


    [Test]
    public void Test3() {
      var res = _invoiceService.GetAllInvoiceNums();
      if ( res.Count() > 0) {
        var data = _invoiceService.GetInvoiceData( res.First() );
        Assert.That( data.InvoiceNo == res.First() );
      }
    }
  }
}