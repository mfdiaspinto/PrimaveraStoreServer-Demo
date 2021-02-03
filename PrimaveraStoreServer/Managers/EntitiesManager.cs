using PrimaveraStoreServer.Integration;
using PrimaveraStoreServer.IntegrationSample;
using PrimaveraStoreServer.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PrimaveraStoreServer.Managers
{
    /// <summary>
    /// Defines a controller capable of managing companies.
    /// </summary>
    public static class EntitiesManager
    {
        #region Public Methods

        /// <summary>
        /// Gets the company key asynchronous.
        /// </summary>
        /// <returns>The company key (if any).</returns>
        public static async Task<bool> ValidateOdataAsync(AuthenticationProvider authenticationProvider, string module, string service, string key)
        {
            try
            {
                return await ItemsController.ValidateIfExistsIEAsync(authenticationProvider, module, service, key).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static  async Task<bool> CreateItemsAsync(AuthenticationProvider authenticationProvider, ProductResource product)
        {
            try
            {
                SalesItemResource resource = Mappers.ToItem(product);

                 var res = await ItemsController.InsertItemToIEAsync(authenticationProvider, resource).ConfigureAwait(false);
           
                if (!string.IsNullOrEmpty(res))
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

            return false;
        }

        public static async Task<string> CreateCustomersAsync(AuthenticationProvider authenticationProvider, Shipping customer)
        {
            try
            {
                // Converter objeto order para invoice
                CustomerResource resource = Mappers.ToCustomer(customer);

                // Inserir invoice do IE

                return await CustomerController.InsertCustomerToIEAsync(authenticationProvider, resource).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                throw new ExecutionEngineException(exception.Message);
            }
        }
        #endregion
    }
}