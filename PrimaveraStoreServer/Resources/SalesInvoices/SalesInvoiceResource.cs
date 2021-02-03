using System;
using System.Collections.Generic;

namespace PrimaveraStoreServer.Resources
{
    public class SalesInvoiceResource
    {
        #region Public Properties

        public string BuyerCustomerParty { get; set; }

        public string BuyerCustomerPartyName { get; set; }
        public string BuyerCustomerPartyAddress { get; set; }

        public string AccountingPartyName { get; set; }
        public string AccountingPartyAddress { get; set; }

        public string Company { get; set; }

        public string Note { get; set; }

        public double? Discount { get; set; }


        public string Currency { get; set; }

        public string Remarks { get; set; }

        public List<SalesInvoiceLineResource> DocumentLines { get; set; }

        #endregion
    }
}
