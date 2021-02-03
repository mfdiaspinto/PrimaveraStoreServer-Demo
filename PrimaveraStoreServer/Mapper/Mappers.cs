using PrimaveraStoreServer.Resources.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimaveraStoreServer.Resources
{
    public partial class Mappers
    {
        public static SalesInvoiceResource ToInvoice(OrderResource order)
        {
            string clientKey = order.shipping.isIndif ? "INDIF" : order.shipping.key;

            SalesInvoiceResource invoice = new SalesInvoiceResource()
            {
                Company = Constants.Identity.Company,
                BuyerCustomerParty = clientKey,
                Note = order.key,
                DocumentLines = new List<SalesInvoiceLineResource>()
            };

            foreach (var l in order.items)
            {
                invoice.DocumentLines.Add(new SalesInvoiceLineResource
                {
                    Quantity = l.quantity,
                    SalesItem = l.product.key,
                    Description = l.product.title,
                    UnitPrice = new MoneyResource()
                    {
                        Amount = l.product.price
                    }
                });
            }

            if (order.shipping.isIndif)
            {
                invoice.BuyerCustomerPartyName = order.shipping.name;
                invoice.BuyerCustomerPartyAddress = $"{order.shipping.addressLine1} {Environment.NewLine} {order.shipping.code} {order.shipping.city}";
                invoice.AccountingPartyName = order.shipping.name;
                invoice.AccountingPartyAddress = $"{order.shipping.addressLine1} {Environment.NewLine} {order.shipping.code} {order.shipping.city}";
            }

            return invoice;
        }

        public static  CustomerResource ToCustomer(Client customer)
        {
            CustomerResource resource = new CustomerResource()
            {
                oneTimeCustomer = customer.isIndif,
                partyKey = customer.key,
                name = customer.name,
                searchTerm = customer.key,
                cityName = customer.city,
                streetName = customer.addressLine1,
                postalZone = customer.code
            };

            return resource;
        }

        public static SalesItemResource ToItem(ProductResource item)
        {
            SalesItemResource resource = new SalesItemResource()
            {
                ItemKey = item.key,
                Description = item.title
            };
           
            return resource;
        }


        public static InvoiceProcessResource ToInvoiceProcess(OrderResource order)
        {
            string clientKey = order.shipping.isIndif ? "INDIF" : order.shipping.key;

            InvoiceProcessResource resource = new InvoiceProcessResource()
            {
                company = Constants.Identity.Company,
                note = order.key,
                customer = new Customer()
                {
                    key = clientKey,
                    name = order.shipping.name,
                    address = new Address()
                    {
                        city = order.shipping.city,
                        street = order.shipping.addressLine1,
                        zipCode = order.shipping.code,
                        country = "PT"
                    }
                },

                items = new List<Resources.Middleware.Item>()
            };

            foreach (var l in order.items)
            {
                resource.items.Add(new Resources.Middleware.Item()
                {
                    quantity = l.quantity,
                    item = l.product.key,
                    description = l.product.title,
                    unitPrice = l.product.price
                });
            }

            return resource;
        }

    }
}
