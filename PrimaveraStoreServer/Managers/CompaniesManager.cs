using PrimaveraStoreServer.IntegrationSample;
using PrimaveraStoreServer.Resources;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using static PrimaveraStoreServer.Constants;

namespace PrimaveraStoreServer.Managers
{
    /// <summary>
    /// Defines a controller capable of managing companies.
    /// </summary>
    public static class CompaniesManager
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the account key.
        /// </summary>
        public static string AccountKey { get; set; }

        /// <summary>
        /// Gets or sets the subscription key.
        /// </summary>
        public static string SubscriptionKey { get; set; }

        /// <summary>
        /// Gets or sets the culture key.
        /// </summary>
        public static string CultureKey { get; set; }

        /// <summary>
        /// Gets or sets the authentication provider.
        /// </summary>
        internal static AuthenticationProvider AuthenticationProvider { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the company key asynchronous.
        /// </summary>
        /// <returns>The company key (if any).</returns>
        public static async Task<IEnumerable<CompanyResource>> GetCompaniesAsync(AuthenticationProvider provider)
        {
            try
            {
                // Create the HTTP client to perform the request

                using (HttpClient client = new HttpClient())
                {
                    await provider.SetAccessTokenAsync(client);

                    var response = await client.GetAsync(
                        string.Concat(Constants.baseAppUrl, "/api/", 
                        Identity.Account , "/", 
                        Identity.Subscription, 
                        Constants.InvoicingEngineRoutes.CompaniesRoute)).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        using var responseStream = await response.Content.ReadAsStreamAsync();
                        return await JsonSerializer.DeserializeAsync
                            <IEnumerable<CompanyResource>>(responseStream);
                    }    
                    else
                    {
                        throw new Exception(await response.Content.ReadAsStringAsync());
                    }
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