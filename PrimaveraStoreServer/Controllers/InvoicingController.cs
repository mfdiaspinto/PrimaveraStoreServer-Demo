using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrimaveraStoreServer.IntegrationSample;
using PrimaveraStoreServer.Managers;
using PrimaveraStoreServer.Resources;
using PrimaveraStoreServer.Resources.Middleware;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PrimaveraStoreServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoicingController : ControllerBase, IDisposable
    {
        public AuthenticationProvider AuthenticationProvider;

        public InvoicingController(AuthenticationProvider provider)
        {
            this.AuthenticationProvider = provider;
        }

        #region invoices

        [HttpGet]
        [Route("invoices/print/{id}")]
        public async Task<IActionResult> PrintInvoice(string id)
        {
            Stream stream = await InvoicesManager.PrintInvoiceAsync(this.AuthenticationProvider, id);

            if (stream == null)
                return null; // returns a NotFoundResult with Status404NotFound response.

            return File(stream, "application/octet-stream", "FR." + id + ".pdf"); // returns a FileStreamResult 
        }

        [HttpPost]
        [Route("invoices")]
        public async Task<ActionResult<string>> PostInvoice([FromBody] OrderResource product)
        {
            try
            {
                return new ActionResult<string>(await InvoicesManager.CreateInvoiceAsync(this.AuthenticationProvider, product));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("invoices")]
        public async Task<ActionResult<string>> PostInvoice(Guid id)
        {
            try
            {
                return new ActionResult<string>(await InvoicesManager.GetInvoiceAsync(this.AuthenticationProvider, id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region middleware

        [HttpPost]
        [Route("async/invoices")]
        public async Task<ActionResult<string>> PostInvoiceAsync([FromBody] OrderResource product)
        {
            try
            {
                return new ActionResult<string>(await InvoicesManager.CreateInvoiceMiddlewareAsync(this.AuthenticationProvider, product));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("async/bulk/invoices")]
        public async Task<List<MiddlewareResponse>> PostBulkInvoiceAsync([FromBody] List<OrderResource> orders)
        {
            return await InvoicesManager.CreateBulkInvoiceMiddlewareAsync(this.AuthenticationProvider, orders);
        }
        
        [HttpGet]
        [Route("async/invoices/{id}")]
        public async Task<MiddlewareResponse> GetStateInvoiceProcessAsync(Guid id)
        {
            return await InvoicesManager.GetStateInvoiceMiddlewareAsync(this.AuthenticationProvider, id);
        }

        public void Dispose()
        {
            // do nothing
        }

        #endregion
    }
}
