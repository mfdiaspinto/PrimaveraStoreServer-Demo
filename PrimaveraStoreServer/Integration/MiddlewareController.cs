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
    public static class MiddlewareController
    {
        #region Public Methods
    
        public static async Task<string> InsertInvoiceToIEAsync(AuthenticationProvider authenticationProvider, InvoiceProcessResource resource)
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
                        MiddlewareRoutes.InvoicesPostRoute,
                        Constants.MiddlewareUrlBase,
                        Identity.Account,
                        Identity.Subscription,
                        MiddlewareRoutes.InvoicesUrlBase);

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

        public static async Task<string> InsertInvoiceToIEAsync(HttpClient client, InvoiceProcessResource resource)
        {
            // Create the HTTP client to perform the request


            var jsonInvoices = new StringContent(
                                JsonSerializer.Serialize(resource),
                                Encoding.UTF8,
                                "application/json");

            string url = string.Format(
                    MiddlewareRoutes.InvoicesPostRoute,
                    Constants.MiddlewareUrlBase,
                    Identity.Account,
                    Identity.Subscription,
                    MiddlewareRoutes.InvoicesUrlBase);

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


        #endregion
    }
}