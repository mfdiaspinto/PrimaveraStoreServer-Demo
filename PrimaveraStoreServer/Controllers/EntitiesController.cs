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
    public class EntitiesController : ControllerBase, IDisposable
    {
        public AuthenticationProvider AuthenticationProvider;

        public EntitiesController(AuthenticationProvider provider)
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
        [Route("items")]
        public async Task<ActionResult<string>> PostItem([FromBody] ProductResource product)
        {
            try
            {
                return new ActionResult<string>(await EntitiesManager.CreateItemsAsync(this.AuthenticationProvider, product));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("items/{id}")]
        public async Task<ActionResult<string>> PostItem(Guid id)
        {
            try
            {
                return new ActionResult<string>(await EntitiesManager.GeiItemsAsync(this.AuthenticationProvider, id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("customers")]
        public async Task<ActionResult<string>> PostCustomerItem([FromBody] Client customer)
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
        [Route("customers/{id}")]
        public async Task<ActionResult<string>> GetCustomer(Guid id)
        {
            try
            {
                return new ActionResult<string>(await EntitiesManager.GetCustomersAsync(this.AuthenticationProvider, id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public void Dispose()
        {
            // do nothing
        }

        #endregion
    }
}
