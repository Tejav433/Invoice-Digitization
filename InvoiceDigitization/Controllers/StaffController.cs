using System;
using InvoiceDigitization.Models;
using InvoiceDigitization.Models.InputRequests;
using InvoiceDigitization.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceDigitization.Controllers {
  [Route( "api/[controller]" )]
  [ApiController]
  public class StaffController: ControllerBase {

    private readonly IInvoiceUpdateService _invoiceUpdateService;

    public StaffController( IInvoiceUpdateService invoiceUpdateService ) {
      _invoiceUpdateService = invoiceUpdateService;
    }

    [HttpPut]
    [Route( "/api/invoices" )]
    public IActionResult AddUpdateInvoiceData( [FromBody] Invoice data ) {
      try {
        _invoiceUpdateService.AddOrUpdateInvoiceData( data.InvoiceNo, data );
        return Ok("Successfully Added or Updated");

      }
      catch ( Exception e ) {
        return BadRequest( e.Message );
      }
    }


    [HttpPut]
    [Route( "/api/status" )]
    public IActionResult UpdateInvoiceStatus( [FromBody] InvoiceStatus data ) {
      try {
        _invoiceUpdateService.UpdateInvoiceStatus( data.InvoiceNo, data.Status );
        return Ok("Successfully Updated the status");
      }
      catch ( Exception e ) {
        return BadRequest( e.Message );
      }

    }
  }
}