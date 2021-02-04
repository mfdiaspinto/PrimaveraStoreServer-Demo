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
            string clientKey = order.shipping.key;

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
                    SalesItem = l.key,
                    Description = l.title,
                    UnitPrice = new MoneyResource()
                    {
                        Amount = l.price
                    }
                });
            }

            if (order.shipping.key.Equals("Indif", StringComparison.OrdinalIgnoreCase))
            {
                invoice.BuyerCustomerPartyName = order.shipping.name;
                invoice.BuyerCustomerPartyAddress = $"{order.shipping.addressLine1} {Environment.NewLine} {order.shipping.postalzone} {order.shipping.city}";
                invoice.AccountingPartyName = order.shipping.name;
                invoice.AccountingPartyAddress = $"{order.shipping.addressLine1} {Environment.NewLine} {order.shipping.postalzone} {order.shipping.city}";
            }

            return invoice;
        }

        public static  CustomerResource ToCustomer(Client customer)
        {
            CustomerResource resource = new CustomerResource()
            {
                oneTimeCustomer = false,
                partyKey = customer.key,
                name = customer.name,
                searchTerm = customer.key,
                cityName = customer.city,
                streetName = customer.addressLine1,
                postalZone = customer.postalzone
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
            string clientKey = order.shipping.key;

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
                        zipCode = order.shipping.postalzone,
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
                    item = l.key,
                    description = l.title,
                    unitPrice = l.price
                });
            }

            return resource;
        }

    }
}
