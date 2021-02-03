using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimaveraStoreServer.Controllers;
using PrimaveraStoreServer.IntegrationSample;
using PrimaveraStoreServer.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
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
            using (var controllerStore = new StoreController(AuthenticationProvider))
            {
                // Produto para criar no invoicing
                ProductResource product = new ProductResource()
                {
                    key = "Cad-0001",
                    title = "Descri��o da cadeira"
                };

                var result = await controllerStore.PostItem(product);

                if (result.Value == false && result.Result is ObjectResult)
                {
                    throw new Exception((result.Result as ObjectResult).Value.ToString());
                }

                Assert.IsTrue(result.Value);
            }
        }

        [TestMethod]
        public async Task CreateInvoice()
        {
            using (var controllerStore = new StoreController(AuthenticationProvider))
            {
                // Produto para criar no invoicing
                OrderResource order = new OrderResource()
                {
                    key = "ENC.2021.1",
                    items = new List<ItemLine>()
                      {
                          new ItemLine()
                          {
                               product = new Product()
                               {
                                    key = "Cad-0001",
                                    price = 10,
                                    title = "Cadeira Branca 0001"
                               },
                               quantity = 2
                          }
                      },
                    shipping = new Shipping()
                    {
                        name = "Rui Fernandes",
                        addressLine1 = "Rua de testes",
                        city = "Braga"
                              , key = "INDIF"
                    }
                };

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
            using (var controllerStore = new StoreController(AuthenticationProvider))
            {
                // order para criar no invoicing
                OrderResource order = new OrderResource()
                {
                    key = "ENC.2021.1",
                    items = new List<ItemLine>()
                      {
                          new ItemLine()
                          {
                               product = new Product()
                               {
                                    key = "Cad-0001",
                                    price = 10,
                                    title = "Cadeira Branca 0001"
                               },
                               quantity = 2
                          }
                      },
                    shipping = new Shipping()
                    {
                        name = "Rui Fernandes",
                        addressLine1 = "Rua de testes",
                        city = "Braga"
                              ,
                        key = "INDIF"
                    }
                };

                var result = await controllerStore.PostInvoiceAsync(order);

                if (string.IsNullOrEmpty(result.Value))
                {
                    throw new Exception((result.Result as ObjectResult).Value.ToString());
                }

                Assert.IsTrue(!string.IsNullOrEmpty(result.Value));
            }
        }

        [TestMethod]
        public async Task CreateInvoiceBulkAsync()
        {
            using (var controllerStore = new StoreController(AuthenticationProvider))
            {
                List<OrderResource> orders = new List<OrderResource>();

                int i = 0;

                while (i < 10)
                {
                    i++;

                    // order para criar no invoicing
                    OrderResource order = new OrderResource()
                    {
                        key = "ENC.2021."+ i,
                        items = new List<ItemLine>()
                      {
                          new ItemLine()
                          {
                               product = new Product()
                               {
                                    key = "Cad-0001",
                                    price = 10,
                                    title = "Cadeira Branca 0001"
                               },
                               quantity = 2
                          }
                      },
                        shipping = new Shipping()
                        {
                            name = "Rui Fernandes",
                            addressLine1 = "Rua de testes",
                            city = "Braga"
                                  ,
                            key = "INDIF"
                        }
                    };

                    orders.Add(order);
                }

                var result = await controllerStore.PostBulkInvoiceAsync(orders);

                Assert.IsTrue(result.Count() == 10);
            }
        }
    }
}
