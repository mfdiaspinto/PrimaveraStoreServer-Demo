using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimaveraStoreServer.Resources
{
    public class SalesItemPriceListLineResource
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the line identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the line Index.
        /// </summary>
        public int Index { get; set; }

        public string SalesItem { get; set; }

        public string PriceList { get; set; }

        public string Unit { get; set; }

        public MoneyResource Price { get; set; }

        #endregion

    }
}
