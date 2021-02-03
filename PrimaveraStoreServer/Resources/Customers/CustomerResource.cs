namespace PrimaveraStoreServer.Resources
{
    public class CustomerResource
    {
        #region Internal Properties

        public string customerGroup { get; set; }

        public string paymentMethod { get; set; }

        public string paymentTerm { get; set; }

        public string partyTaxSchema { get; set; }

        public bool locked { get; set; }

        public bool oneTimeCustomer { get; set; }

        public bool endCustomer { get; set; }

        public string baseEntityKey { get; set; }

        public string partyKey { get; set; }

        public string name { get; set; }

        public string cityName { get; set; }

        public string postalZone { get; set; }

        public string streetName { get; set; }

        public string searchTerm { get; set; }





        #endregion
    }
}
