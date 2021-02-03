using PrimaveraStoreServer.IntegrationSample;
using PrimaveraStoreServer.Resources;
using PrimaveraStoreServer.Resources.Middleware;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static PrimaveraStoreServer.Constants;

namespace PrimaveraStoreServer.Integration
{
    /// <summary>
    /// Defines a controller capable of managing invoices.
    /// </summary>
    public static class ItemsController
    {
        #region Public Methods
    
        public static async Task<string> InsertItemToIEAsync(AuthenticationProvider authenticationProvider, SalesItemResource resource)
        {
            // Create the HTTP client to perform the request

            using (HttpClient client = new HttpClient())
            {
                await authenticationProvider.SetAccessTokenAsync(client);

               var jsonInvoices = new StringContent(
                                   JsonSerializer.Serialize(resource),
                                   Encoding.UTF8,
                                   "application/json");

                string url = string.Format(
                        InvoicingEngineRoutes.ItemPostRoute,
                        Constants.baseAppUrl,
                        Identity.Account,
                        Identity.Subscription,
                        InvoicingEngineRoutes.ItemUrlBase);

                var response = await client.PostAsync(url, jsonInvoices).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }
            }            
        }

        public static async Task<bool> ValidateIfExistsIEAsync(AuthenticationProvider authenticationProvider, string module, string service, string key)
        {
            // Create the HTTP client to perform the request

            using (HttpClient client = new HttpClient())
            {
                await authenticationProvider.SetAccessTokenAsync(client);

                string url = string.Format(
                        InvoicingEngineRoutes.ItemValidateRoute,
                        Constants.baseAppUrl,
                        Identity.Account,
                        Identity.Subscription,
                        module,
                        service, 
                        key);

                var response = await client.GetAsync(url).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    RootOdata listIds = JsonSerializer.Deserialize<RootOdata>(jsonString);

                    return listIds.items != null && listIds.items.Any();
                }
                else
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }
            }
        }

    #endregion
}
}