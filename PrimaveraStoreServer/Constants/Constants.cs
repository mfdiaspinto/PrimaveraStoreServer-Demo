namespace PrimaveraStoreServer
{
    /// <summary>
    /// Constants
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// The Jasmin base application URL.
        /// </summary>
        public const string baseAppUrl = "https://st-invoicing-engine.primaverabss.com";

        /// <summary>
        /// The default culture
        /// </summary>
        /// <remarks>Available cultures are "PT-pt, EN-us, ES-es".</remarks>
        public const string DefaultCulture = "PT-pt";

        /// <summary>
        /// Authentication related constants.
        /// </summary>
        internal static class Identity
        {
            internal const string BaseUriKey = "https://stg-identity.primaverabss.com";
            internal const string TokenUriKey = "connect/token";
            internal const string ApplicationScopes = "application lithium-ies lithium-ies-wb";
            internal const string ClientId = "IE-801317-0063";
            internal const string ClientSecret = "3b1340b1-62ea-40c9-b8ff-19aa6d42f23c";
            internal const string Account = "801317";
            internal const string Subscription = "801317-0063";
            internal const string Company = "PRIMAVERASTORE";
        }


        /// <summary>
        /// Requests headers related constants.
        /// </summary>
        internal static class RequestHeaders
        {
            internal const string AcceptLanguageHeaderKey = "Accept-Language";
            internal const string MediaTypeWithQualityHeaderKey = "application/json";
            internal const string AuthenticationHeaderKey = "Bearer";
        }

        internal static class InvoicingEngineRoutes
        {
            internal const string InvoicingEngineUrlBase = "https://st-invoicing-engine.primaverabss.com/api";

            internal const string CompaniesRoute = "/corepatterns/companies";
            internal const string MediaTypeWithQualityHeaderKey = "application/json";
            internal const string AuthenticationHeaderKey = "Bearer";

            internal const string InvoicesPostRoute = "{0}/{1}/{2}/{3}";
            internal const string InvoicesUrlBase = "/billing/invoices";
            internal const string TemplateUrlBase = "BILLING_MATERIALSINVOICEREPORT_PRISTORE";

            internal const string InvoicesPrintUrlBase = "{0}/{1}/{2}/{3}/{4}/print?template={5}";

            internal const string CustomerPostRoute = "{0}/{1}/{2}/{3}";
            internal const string CustomersUrlBase = "/salescore/customerparties";

            internal const string ItemPostRoute = "{0}/{1}/{2}/{3}";
            internal const string ItemUrlBase = "/salescore/salesitems";
            internal const string ItemValidateRoute = "https://st-invoicing-engine.primaverabss.com/api/{0}/{1}/{2}/{3}/odata?$select=Id&$filter= NaturalKey eq '{4}'";
        }

        internal static class MiddlewareRoutes 
        { 
            internal const string MiddlewareUrlBase = "https://st-invoicing-engine.primaverabss.com/middleware/api/v1";

            internal const string InvoicesPostRoute = "{0}/{1}/{2}/{3}";
            internal const string InvoicesUrlBase = "/billing/invoices";
        }
    }
}
