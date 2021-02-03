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
        /// Gets or sets the products.
        /// </summary>
        public static List<ProductResource> Products { get; set; } = new List<ProductResource>()
        {
            new ProductResource() { key = "FA-0001", price = 4.6M, sync = false, title = "Abacate", category= "Fruta"},
            new ProductResource() { key = "FL-0002", price = 1.5M, sync = false, title = "Laranja", category= "Fruta"},
            new ProductResource() { key = "FM-0003", price = 1.5M, sync = false, title = "Maça", category= "Fruta"}
        };

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

        public static async Task<Dictionary<string, string>> CreateBulkInvoiceMiddlewareAsync(AuthenticationProvider authenticationProvider, List<OrderResource> orders)
        {
            try
            {
                Dictionary<string, string> result = new Dictionary<string, string>();

                // Create the HTTP client to perform the request

                using (HttpClient client = new HttpClient())
                {
                    await authenticationProvider.SetAccessTokenAsync(client);

                    foreach (var order in orders)
                    {
                        InvoiceProcessResource resource = Mappers.ToInvoiceProcess(order);

                        string res = await MiddlewareController.InsertInvoiceToIEAsync(client, resource);

                        MiddlewareResponse r = JsonSerializer.Deserialize<MiddlewareResponse>(res);

                        if (!result.ContainsKey(order.key))
                        {
                            result.Add(order.key, r.id);
                        }
                        else
                        {
                            result[order.key] = r.id;
                        }
                    }
                }

                return result;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static async Task<Dictionary<string, string>> CreateValidateInvoiceMiddlewareAsync(AuthenticationProvider authenticationProvider, List<OrderResource> orders)
        {
            try
            {
                Dictionary<string, string> result = new Dictionary<string, string>();

                // Create the HTTP client to perform the request

                using (HttpClient client = new HttpClient())
                {
                    await authenticationProvider.SetAccessTokenAsync(client);

                    foreach (var order in orders)
                    {
                        var response = await client.GetAsync("https://invoicing-engine.primaverabss.com/middleware/api/v1/247504/247504-0001/billing/invoices/" + order.invoiceProcId + "/output").ConfigureAwait(false);

                        if (response.IsSuccessStatusCode)
                        {
                            var jsonString = await response.Content.ReadAsStringAsync();

                            if (!string.IsNullOrEmpty(jsonString))
                            {
                                MiddlewareOutpuResponse r = JsonSerializer.Deserialize<MiddlewareOutpuResponse>(jsonString);

                                if (!string.IsNullOrEmpty(r.invoiceId))
                                {
                                    if (!result.ContainsKey(order.key))
                                    {
                                        result.Add(order.key, r.invoiceId);
                                    }
                                    else
                                    {
                                        result[order.key] = r.invoiceId;
                                    }
                                }

                            }
                            else
                            {
                                if (!result.ContainsKey(order.key))
                                {
                                    result.Add(order.key, "");
                                }
                            }
                        }
                        else
                        {
                            string a = await response.Content.ReadAsStringAsync();
                            throw new Exception(a);
                        }
                    }
                }

                return result;
            }
            catch (Exception exception)
            {
                throw new Exception("Error geting the current company.");
            }

            return null;
        }

        #endregion
    }
}