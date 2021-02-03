using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace PrimaveraStoreServer.IntegrationSample
{
    public class AuthenticationProvider
    {
        #region Members

        private string clientId;
        private string clientSecret;

        private TokenClient tokenClient;

        private string accessToken;
        private DateTime tokenExpirationDate;

        #endregion

        #region Constructors

        public AuthenticationProvider()
        {
            this.RequestAccessTokenAsync().Wait();
        }

        #endregion

        #region Protected Properties

        protected TokenClient TokenClient
        {
            get
            {
                if (this.tokenClient == null)
                {
                    HttpClient client = new HttpClient();
                    this.tokenClient = new TokenClient(client, new TokenClientOptions() { 
                         ClientId = Constants.Identity.ClientId,
                         ClientSecret = Constants.Identity.ClientSecret,
                         Address = string.Concat(Constants.Identity.BaseUriKey, "/connect/token"),
                    });
                }

                return this.tokenClient;
            }
        }

        #endregion

        #region Public Methods

        public async Task SetAccessTokenAsync(HttpClient client)
        {
            if (string.IsNullOrEmpty(this.accessToken) || this.tokenExpirationDate <= DateTime.Now)
            {
                await this.RequestAccessTokenAsync();
            }

            client.SetBearerToken(this.accessToken);
        }

        public async Task RequestAccessTokenAsync()
        {
            TokenResponse tokenResponse = await this.TokenClient.RequestClientCredentialsTokenAsync("application lithium-ies lithium-ies-wh");
            if (tokenResponse.IsError)
            {
                throw new Exception(tokenResponse.Error);
            }

            this.accessToken = tokenResponse.AccessToken;
            if (string.IsNullOrEmpty(tokenResponse.AccessToken))
            {
                throw new Exception("Failed to obtain the INVOICING API access token.");
            }

            this.tokenExpirationDate = DateTime.Now.AddSeconds(tokenResponse.ExpiresIn);
        }

        #endregion
    }
}
