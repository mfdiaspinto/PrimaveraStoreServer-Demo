using System;
using System.Collections.Generic;

namespace PrimaveraStoreServer.Resources
{
    internal class ItemResource
    {
        #region Public Properties

        public string ItemKey { get; set; }

        public string Description { get; set; }

        public string BaseUnit { get; set; }
        
        public string ItemType { get; set; }

        public string Assortment { get; set; }

        #endregion
    }

    public class Item
    {
        public string id { get; set; }
    }

    public class RootOdata
    {
        public List<Item> items { get; set; }
        public string nextPageLink { get; set; }
        public int? count { get; set; }
    }

}
