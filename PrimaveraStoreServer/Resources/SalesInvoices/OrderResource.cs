using System;
using System.Collections.Generic;

namespace PrimaveraStoreServer.Resources
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Product
    {
        public double price { get; set; }
        public string title { get; set; }
        public string key { get; set; }
    }

    public class ItemLine
    {
        public Product product { get; set; }
        public int quantity { get; set; }
        public double totalPrice { get; set; }
    }

    public class Shipping
    {
        public string addressLine1 { get; set; }
        public string code { get; set; }
        public string city { get; set; }
        public string name { get; set; }

        public bool isIndif { get; set; }

        public string key { get; set; }
    }

    public partial class OrderResource
    {
        public List<ItemLine> items { get; set; }
        public string key { get; set; }
        public Shipping shipping { get; set; }
        public double totalDocument { get; set; }
        public string userId { get; set; }
        public string userName { get; set; }

        public string invoiceProcId { get; set; }
    }


}
