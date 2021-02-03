namespace PrimaveraStoreServer.Resources
{
    /// <summary>
    /// Describes a money resource.
    /// </summary>
    public class MoneyResource
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        public string Currency { get; set; }

        #endregion
    }
}