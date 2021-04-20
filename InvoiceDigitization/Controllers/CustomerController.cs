using System;
using System.Collections.Generic;
using InvoiceDigitization.Models;
using InvoiceDigitization.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceDigitization.Controllers {
  [Route( "api/[controller]" )]
  [ApiController]
  public class CustomerController: ControllerBase {

    private readonly IInvoiceService _invoiceService;

    public CustomerController( IInvoiceService invoiceService ) {
      _invoiceService = invoiceService;
    }

    [HttpGet]
    [Route( "/api/invoices/{num}" )]
    public Invoice GetInvoiceData( string num ) {
      try {
        var res = _invoiceService.GetInvoiceData( num );
        return res ?? new Invoice();
      }
      catch ( Exception ) {
        return new Invoice();
      }

    }


    [HttpGet]
    [Route( "/api/status/invoices/{num}" )]
    public string GetInvoiceStatus( string num ) {
      try {
        return _invoiceService.GetInvoiceStatus( num );
      }
      catch ( Exception ) {
        return Status.NotProcessed.ToString();
      }
    }


    [HttpGet]
    [Route( "/api/invoices" )]
    public IEnumerable<Invoice> GetAllInvoiceData() {
      try {
        return _invoiceService.GetAllInvoicesData();
      }
      catch ( Exception ) {
        return new List<Invoice>();
      }
    }


    [HttpGet]
    [Route( "/api/invoice-numbers" )]
    public IEnumerable<string> GetAllInvoiceNumbers() {
      try {
        return _invoiceService.GetAllInvoiceNums();
      }
      catch ( Exception ) {
        return new List<string>();
      }
    }

    [HttpPost]
    [Route( "/api/invoices" )]
    public IActionResult ProcesInvoice( [FromBody]  InvoiceFile input ) {
      try {
        _invoiceService.ProcessInvoice( input.InvoiceFiles );
        return Ok("Successfully Processed the Invoice");
      }
      catch ( Exception e ) {
        return BadRequest( e.Message );
      }
    }

  }
}