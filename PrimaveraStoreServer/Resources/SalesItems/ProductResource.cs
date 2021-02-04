namespace PrimaveraStoreServer.Resources
{
    public class ProductResource
    {
        public ProductResource(string key, string title)
        {
            this.key = key;
            this.title = title;
        }

        #region Public Properties

        public string key { get; set; }

        public string title { get; set; }

        #endregion
    }
}
