using System;
using System.Collections.Concurrent;
using System.Linq;
using InvoiceDigitization;
using InvoiceDigitization.Models;
using NUnit.Framework;

namespace InvoiceDigitizationTests {
  class AddOrUpdateTests {

    private ConcurrentDictionary<string, Invoice> _invoiceStore;
    //private IInvoiceService _invoiceService;
    //private IInvoiceUpdateService _invoiceUpdateService;

    [OneTimeSetUp]
    [SetUp]
    public void Setup() {
      //_invoiceService = new InvoiceService();
      _invoiceStore = InvoiceDataStore.InvoiceData;
      //_invoiceUpdateService = new InvoiceUpdateService();
      Assert.NotNull( _invoiceStore );
      //Assert.NotNull( _invoiceService );
      //Assert.NotNull( _invoiceUpdateService );
    }

    [Test]
    public void Test1() {
      var mockData = MockDataHelper.GetInvoiceData();
      Assert.That( !string.IsNullOrEmpty( mockData.InvoiceNo ) );
      Assert.That(  mockData.InvoiceDate != null ) ;
      Assert.That( mockData.Seller != null && !string.IsNullOrEmpty( mockData.Seller.Name ) );
      Assert.That( mockData.BillTo != null && !string.IsNullOrEmpty(mockData.BillTo.Name) );
      Assert.That( mockData.ShipTo != null && !string.IsNullOrEmpty( mockData.ShipTo.Name ) );
      Assert.That( mockData.Items != null &&  mockData.Items.Count() >0 ) ;
      Assert.That( mockData.SubTotal != 0 );
      Assert.That( mockData.TotalAmount != 0 );

    }


    [Test]
    public void Test2() {
      var mockData = MockDataHelper.GetInvoiceData();
      InvoiceDataStore.Add( mockData );
      Assert.That( _invoiceStore.ContainsKey( mockData.InvoiceNo ) );
      Assert.That( _invoiceStore[mockData.InvoiceNo].TotalAmount == mockData.TotalAmount );

    }

    [Test]
    public void Test3() {
      var mockData = MockDataHelper.GetInvoiceData();
      InvoiceDataStore.Add( mockData );
      Assert.That( _invoiceStore.ContainsKey( mockData.InvoiceNo ) );
      mockData.Seller.Name = "Zomato";
      mockData.ShipTo.ZipCode = "234323";
      InvoiceDataStore.Update( mockData.InvoiceNo, mockData );
      Assert.That( _invoiceStore.ContainsKey( mockData.InvoiceNo ) );
      Assert.That( _invoiceStore[mockData.InvoiceNo].Seller.Name.Equals( "Zomato" ) );
      Assert.That( _invoiceStore[mockData.InvoiceNo].ShipTo.ZipCode.Equals( "234323" ) );

    }

    [Test]
    public void Test4() {
      var mockData = MockDataHelper.GetInvoiceData();
      InvoiceDataStore.Add( mockData );
      var oldInvoiceNo = mockData.InvoiceNo;
      var newInvoiceNo = Guid.NewGuid().ToString();
      Assert.That( _invoiceStore.ContainsKey( mockData.InvoiceNo ) );
      mockData.InvoiceNo = newInvoiceNo;
      mockData.Seller.Name = "Zomato";
      mockData.ShipTo.ZipCode = "234323";
      InvoiceDataStore.Update( mockData.InvoiceNo, mockData );
      Assert.That( _invoiceStore.ContainsKey( newInvoiceNo ) );
      Assert.That( _invoiceStore[newInvoiceNo].Seller.Name.Equals( "Zomato" ) );
      Assert.That( _invoiceStore[newInvoiceNo].ShipTo.ZipCode.Equals( "234323" ) );
      Assert.That( _invoiceStore.ContainsKey( oldInvoiceNo ) );
      

    }

    [Test]
    public void Test5() {
      var mockData = MockDataHelper.GetInvoiceData();
      InvoiceDataStore.Update( mockData.InvoiceNo, mockData );
      Assert.That( _invoiceStore.ContainsKey( mockData.InvoiceNo ) );
      Assert.That( _invoiceStore[mockData.InvoiceNo].Status == Status.NotProcessed );
      InvoiceDataStore.UpdateStatus( mockData.InvoiceNo, Status.Digitized );
      Assert.That( _invoiceStore.ContainsKey( mockData.InvoiceNo ) );
      Assert.That( _invoiceStore[mockData.InvoiceNo].Status == Status.Digitized );
    }
  }
}
