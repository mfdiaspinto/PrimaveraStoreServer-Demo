using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimaveraStoreServer.Controllers;
using PrimaveraStoreServer.IntegrationSample;
using PrimaveraStoreServer.Resources;
using PrimaveraStoreServer.Resources.Middleware;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestIntegrations
{
    [TestClass]
    public class StorageTest
    {
        private static AuthenticationProvider provider;

        private static AuthenticationProvider AuthenticationProvider
        {
            get
            {
                if (provider == null)
                {
                    provider = new AuthenticationProvider();
                }

                return provider;
            }
        }

        [TestMethod]
        public async Task CreateItemAsync()
        {
            using (var controllerStore = new EntitiesController(AuthenticationProvider))
            {
                // Produto para criar no invoicing
                ProductResource product = new ProductResource("Cad-0002", "Descrição da cadeira 2");

                var result = await controllerStore.PostItem(product);

                Assert.IsTrue(!string.IsNullOrEmpty(result.Value));
            }
        }

        [TestMethod]
        public async Task CreateCustomerAsync()
        {
            using (var controllerStore = new EntitiesController(AuthenticationProvider))
            {
                // Produto para criar no invoicing
                Client resource = new Client("C-MD-0002", "Miguel Dias", "Rua dos Bombeiros", "Leiria", "4509-003");

                var result = await controllerStore.PostCustomerItem(resource);

                if (string.IsNullOrEmpty(result.Value))
                {
                    throw new Exception((result.Result as ObjectResult).Value.ToString());
                }

                Assert.IsTrue(!string.IsNullOrEmpty(result.Value));
            }
        }

        [TestMethod]
        public async Task CreateInvoice()
        {
            using (var controllerStore = new InvoicingController(AuthenticationProvider))
            {
                List<ItemLine> items = new List<ItemLine>()
                {
                    new ItemLine("Cad-0001", "Cadeira Branca 0001", 2, 10)
                };

                Client shipping = new Client("INDIF", "Miguel Dias", "Rua dos Bombeiros", "Leiria", "4509-003");

                // order para criar no invoicing
                OrderResource order = new OrderResource("ENC.2021.1", shipping, items);

                var result = await controllerStore.PostInvoice(order);

                if (string.IsNullOrEmpty(result.Value))
                {
                    throw new Exception((result.Result as ObjectResult).Value.ToString());
                }

                Assert.IsTrue(!string.IsNullOrEmpty(result.Value));
            }
        }

        [TestMethod]
        public async Task CreateInvoiceAsync()
        {
            using (var controllerStore = new InvoicingController(AuthenticationProvider))
            {
                // order para criar no invoicing
                List<ItemLine> items = new List<ItemLine>()
                {
                    new ItemLine("Cad-0001", "Cadeira Branca 0001",2, 10)
                };

                Client shipping = new Client("INDIF", "Miguel Dias", "Rua dos Bombeiros", "Leiria", "4509-003");

                // order para criar no invoicing
                OrderResource order = new OrderResource("ENC.2021.1", shipping, items);

                var result = await controllerStore.PostInvoiceAsync(order);

                if (string.IsNullOrEmpty(result.Value))
                {
                    throw new Exception((result.Result as ObjectResult).Value.ToString());
                }

                MiddlewareResponse r = JsonSerializer.Deserialize<MiddlewareResponse>(result.Value);

                Debug.WriteLine(string.Concat("ProcessId: ", r.id));
                Debug.WriteLine(string.Concat("State: ", r.state));

                Assert.IsTrue(!string.IsNullOrEmpty(result.Value));
            }
        }

        [TestMethod]
        public async Task CreateInvoiceBulkAsync()
        {
            using (var controllerStore = new InvoicingController(AuthenticationProvider))
            {
                List<OrderResource> orders = new List<OrderResource>();

                int i = 0;

                while (i < 10)
                {
                    i++;

                    // order para criar no invoicing
                    List<ItemLine> items = new List<ItemLine>()
                    {
                        new ItemLine("Cad-0001", "Cadeira Branca 0001",2, 10)
                    };

                    Client shipping = new Client("INDIF", "Miguel Dias", "Rua dos Bombeiros", "Leiria", "4509-003");

                    // order para criar no invoicing
                    OrderResource order = new OrderResource("ENC.2021." + i, shipping, items);

                    orders.Add(order);
                }

                var result = await controllerStore.PostBulkInvoiceAsync(orders);

                Assert.IsTrue(result.Count() == 10);
            }
        }


        [TestMethod]
        public async Task GetStateInvoiceProcessAsync()
        {
            using (var controllerStore = new InvoicingController(AuthenticationProvider))
            {
                Guid processId = new Guid("8f113710-3c49-458c-b904-08cf600fdcad");

                MiddlewareResponse result = await controllerStore.GetStateInvoiceProcessAsync(processId);

                Debug.WriteLine(string.Concat("ProcessId: ", result.id));
                Debug.WriteLine(string.Concat("State: ", result.state));
                
                if(result.details != null && !string.IsNullOrEmpty(result.details.invoiceId))
                {
                    Debug.WriteLine(string.Concat("Invoice Id: ", result.details.invoiceId));
                }

                Assert.IsTrue(result.details != null && !string.IsNullOrEmpty(result.details.invoiceId));
            }
        }

    }
}
