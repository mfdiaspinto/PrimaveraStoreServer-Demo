using System;
using System.Collections.Generic;

namespace PrimaveraStoreServer.Resources
{
    public class ItemLine
    {
        public ItemLine(string key, string title, int quantity, double price)
        {
            this.key = key;
            this.title = title;
            this.price = price;
            this.quantity = quantity;
        }

        public double price { get; set; }

        public string title { get; set; }

        public string key { get; set; }

        public int quantity { get; set; }
    }

    public class Client
    {
        public Client()
        {

        }

        public Client(string key, string name, string addressLine1, string city, string code)
        {
            this.key = key;
            this.name = name;
            this.addressLine1 = addressLine1;
            this.city = city;
            this.postalzone = code;
        }

        public string addressLine1 { get; set; }

        public string postalzone { get; set; }
        
        public string city { get; set; }
        
        public string name { get; set; }

        public string key { get; set; }
    }

    public partial class OrderResource
    {
        public OrderResource()
        {

        }

        public OrderResource (string key, Client shipping, List<ItemLine> items)
        {
            this.key = key;
            this.shipping = shipping;
            this.items = items;
        }

        public List<ItemLine> items { get; set; }
        
        public string key { get; set; }
        
        public Client shipping { get; set; }
        
        public string invoiceProcId { get; set; }
    }


}
