using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrimaveraStoreServer.IntegrationSample;
using PrimaveraStoreServer.Managers;
using PrimaveraStoreServer.Resources;
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
    public class StoreController : ControllerBase, IDisposable
    {
        public AuthenticationProvider AuthenticationProvider;

        public StoreController(AuthenticationProvider provider)
        {
            this.AuthenticationProvider = provider;
        }

        #region Items and entities

        [HttpGet]
        [Route("companies")]
        public async Task<IEnumerable<CompanyResource>> GetCompanies()
        {
            return await CompaniesManager.GetCompaniesAsync(this.AuthenticationProvider);
        }

        [HttpPost]
        [Route("products")]
        public async Task<ActionResult<bool>> PostItem([FromBody] ProductResource product)
        {
            try
            {
                return new ActionResult<bool>(await EntitiesManager.CreateItemsAsync(this.AuthenticationProvider, product));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("customers")]
        public async Task<ActionResult<string>> PostCustomerItem([FromBody] Shipping customer)
        { 
            try
            {
                return new ActionResult<string>(await EntitiesManager.CreateCustomersAsync(this.AuthenticationProvider, customer));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("odata/{module}/{service}/{key}")]
        public async Task<bool> ValidateOdata(string module, string service, string key)
        {
            return await EntitiesManager.ValidateOdataAsync(this.AuthenticationProvider,module , service, key);
        }

        #endregion

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
        public async Task<Dictionary<string, string>> PostBulkInvoiceAsync([FromBody] List<OrderResource> orders)
        {
            return await InvoicesManager.CreateBulkInvoiceMiddlewareAsync(this.AuthenticationProvider, orders);
        }

        [HttpPost]
        [Route("async/validate/invoices")]
        public async Task<Dictionary<string, string>> PostValidateInvoiceAsync([FromBody] List<OrderResource> orders)
        {
            return await InvoicesManager.CreateValidateInvoiceMiddlewareAsync(this.AuthenticationProvider, orders);
        }

        public void Dispose()
        {
            // do nothing
        }

        #endregion
    }
}
