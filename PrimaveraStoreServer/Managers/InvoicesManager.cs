using PrimaveraStoreServer.Integration;
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

namespace PrimaveraStoreServer.Managers
{
    /// <summary>
    /// Defines a controller capable of managing companies.
    /// </summary>
    public static class InvoicesManager
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the subscription key.
        /// </summary>
        public static string SubscriptionKey { get; set; }

        /// <summary>
        /// Gets or sets the culture key.
        /// </summary>
        public static string CultureKey { get; set; }

        #endregion

        #region Public Methods

        public static  async Task<string> CreateInvoiceAsync(AuthenticationProvider authenticationProvider, OrderResource order)
        {
            try
            {
                // Converter objeto order para invoice
                SalesInvoiceResource resource = Mappers.ToInvoice(order);

                // Inserir invoice do IE

                return await InvoicesController.InsertInvoiceToIEAsync(authenticationProvider, resource).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }


        public static async Task<string> GetInvoiceAsync(AuthenticationProvider authenticationProvider, Guid id)
        {
            try
            {
                // Inserir invoice do IE

                return await InvoicesController.GetInvoiceFromIEAsync(authenticationProvider, id).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }


        public static async Task<Stream> PrintInvoiceAsync(AuthenticationProvider authenticationProvider, string id)
        {
            try
            {
                return await InvoicesController.PrintInvoiceFromIEAsync(authenticationProvider, id).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        #endregion

        #region Middleware

        public static async Task<string> CreateInvoiceMiddlewareAsync(AuthenticationProvider authenticationProvider, OrderResource order)
        {
            try
            {
                // Converter objeto order para invoice
                InvoiceProcessResource resource = Mappers.ToInvoiceProcess(order);

                // Inserir invoice do IE

                return await MiddlewareController.InsertInvoiceToIEAsync(authenticationProvider, resource).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static async Task<List<MiddlewareResponse>> CreateBulkInvoiceMiddlewareAsync(AuthenticationProvider authenticationProvider, List<OrderResource> orders)
        {
            try
            {
                List<MiddlewareResponse> result = new List<MiddlewareResponse>();

                // Create the HTTP client to perform the request

                using (HttpClient client = new HttpClient())
                {
                    await authenticationProvider.SetAccessTokenAsync(client);

                    foreach (var order in orders)
                    {
                        InvoiceProcessResource resource = Mappers.ToInvoiceProcess(order);

                        string res = await MiddlewareController.InsertInvoiceToIEAsync(client, resource);

                        MiddlewareResponse r = JsonSerializer.Deserialize<MiddlewareResponse>(res);

                        result.Add(r);
                    }
                }

                return result;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static async Task<MiddlewareResponse> GetStateInvoiceMiddlewareAsync(AuthenticationProvider authenticationProvider, Guid id)
        {
            try
            {
                // Create the HTTP client to perform the request

                using (HttpClient client = new HttpClient())
                {
                    await authenticationProvider.SetAccessTokenAsync(client);

                    MiddlewareResponse res = await MiddlewareController.GetInvoiceProcessAsync(client, id);

                    if (res.state.Equals("Succeeded", StringComparison.OrdinalIgnoreCase))
                    {
                        res.details = await MiddlewareController.GetInvoiceOutputProcessAsync(client, id);
                    }

                    return res;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        #endregion
    }
}