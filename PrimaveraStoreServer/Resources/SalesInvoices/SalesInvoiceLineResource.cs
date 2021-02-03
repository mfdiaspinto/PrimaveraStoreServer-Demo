namespace PrimaveraStoreServer.Resources
{
    public class SalesInvoiceLineResource
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the line identifier.
        /// </summary>
        public string Id { get; set; }

        public string SalesItem { get; set; }

        public string Warehouse { get; set; }

        public string Description { get; set; }

        public double? Quantity { get; set; }

        public MoneyResource UnitPrice { get; set; }

        #endregion
    }
}
