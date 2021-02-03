using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimaveraStoreServer.Resources.Middleware
{
    public class MiddlewareResponse
    {
        public string state { get; set; }
        public string accountKey { get; set; }
        public string id { get; set; }
        public string inputType { get; set; }
        public DateTime registrationDate { get; set; }
        public int retryCount { get; set; }
        public string subscriptionKey { get; set; }
    }

    public class MiddlewareOutpuResponse
    {
        public string invoiceId { get; set; }
    }
}
