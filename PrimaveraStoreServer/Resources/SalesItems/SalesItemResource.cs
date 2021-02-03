using System.Collections.Generic;

namespace PrimaveraStoreServer.Resources
{
    public class SalesItemResource
    {
        #region Members

        private ICollection<SalesItemPriceListLineResource> pricelistLines = new System.Collections.ObjectModel.Collection<SalesItemPriceListLineResource>();

        #endregion

        #region Public Properties

        public string BaseEntityKey { get; set; }

        public string ItemKey { get; set; }
        
        public string IncomeAccount { get; set; }
        
        public string ItemTaxSchema { get; set; }
        
        public string Unit { get; set; }

        public string Description { get; set; }

        #endregion
    }
}
