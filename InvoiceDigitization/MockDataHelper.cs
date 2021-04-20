using System;
using System.Collections.Generic;
using InvoiceDigitization.Models;

namespace InvoiceDigitization {
  public class MockDataHelper {

    private static readonly Address _seller = new Address {
      Name = "Swiggy",
      AddressLine1 = "4263  Abner Road",
      City = "Marathon",
      State = "Karnataka",
      ZipCode = "54448",
      PhoneNum = "7156806356"
    };

    private static readonly Address _billTo = new Address {
      Name = "Ramesh",
      AddressLine1 = "463  Kakatiya Road",
      City = "Mandira",
      State = "Karnataka",
      ZipCode = "544482",
      PhoneNum = "9723232322"
    };

    private static readonly Address _shipTo = new Address {
      Name = "Shankar",
      AddressLine1 = "258  Hanuma Temple Road",
      City = "Domlur",
      State = "Karnataka",
      ZipCode = "560008",
      PhoneNum = "9420424341"
    };


    private static readonly Item _item1 = new Item {
      Id = 1,
      Description = "Chicken Biryani",
      Quantity = 2,
      UnitCost = 150,
      Amount = 2 * 150

    };

    private static readonly Item _item2 = new Item {
      Id = 2,
      Description = "Fried Rice",
      Quantity = 1,
      UnitCost = 100,
      Amount = 1 * 100

    };
    private static readonly Item _item3 = new Item {
      Id = 3,
      Description = "Noodles",
      Quantity = 4,
      UnitCost = 100,
      Amount = 4 * 100

    };

    private static readonly List<Item> _items = new List<Item> { _item1, _item2, _item3 };

    private static double GetSubTotal() {
      double subTotal = 0;
      foreach ( var item in _items ) {
        subTotal += item.Amount;
      }
      return subTotal;

    }

    public static Invoice GetInvoiceData() {
      var invoiceData = new Invoice {
        Seller = _seller,
        BillTo = _billTo,
        ShipTo = _shipTo,
        Items = _items,
        SubTotal = GetSubTotal(),
        Tax = 81
      };
      invoiceData.TotalAmount = invoiceData.SubTotal + invoiceData.Tax;
      invoiceData.InvoiceDate = DateTime.Today.AddDays( -10 );
      invoiceData.InvoiceNo = Guid.NewGuid().ToString();
      return invoiceData;
    }


  }
}

