using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimaveraStoreServer.Resources.Middleware
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Address
    {
        public string city { get; set; }
        public string country { get; set; }
        public string number { get; set; }
        public string street { get; set; }
        public string zipCode { get; set; }
    }

    public class Customer
    {
        public string accountingPartyTaxId { get; set; }
        public Address address { get; set; }
        public string customerGroup { get; set; }
        public string deliveryTerm { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string paymentMethod { get; set; }
        public string paymentTerm { get; set; }
        public string taxSchema { get; set; }
    }

    public class Item
    {
        public string itemWithholdingTaxSchema { get; set; }
        public string complementaryDescription { get; set; }
        public string description { get; set; }
        public int discount { get; set; }
        public string item { get; set; }
        public string itemTaxSchema { get; set; }
        public string itemType { get; set; }
        public int quantity { get; set; }
        public string unit { get; set; }
        public double unitPrice { get; set; }
        public string warehouse { get; set; }
    }

    public class LoadingAddress
    {
        public string city { get; set; }
        public string country { get; set; }
        public string number { get; set; }
        public string street { get; set; }
        public string zipCode { get; set; }
    }

    public class UnloadingAddress
    {
        public string city { get; set; }
        public string country { get; set; }
        public string number { get; set; }
        public string street { get; set; }
        public string zipCode { get; set; }
    }

    public class InvoiceProcessResource
    {
        public string cashFlowItem { get; set; }
        public DateTime? checkDate { get; set; }
        public string checkNumber { get; set; }
        public string company { get; set; }
        public string currency { get; set; }
        public Customer customer { get; set; }
        public int discount { get; set; }
        public DateTime? documentDate { get; set; }
        public string documentType { get; set; }
        public string financialAccount { get; set; }
        public List<Item> items { get; set; }
        public LoadingAddress loadingAddress { get; set; }
        public string note { get; set; }
        public string noteToRecipient { get; set; }
        public string serie { get; set; }
        public UnloadingAddress unloadingAddress { get; set; }
        public string id { get; set; }
    }


}
