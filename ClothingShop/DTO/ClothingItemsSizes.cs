using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothingShop.DTO
{
    public class ClothingItemsSizes
    {
        public class ClothingItemsSizesListItem
        {
            public Guid ItemId { get; set; }
            public string ItemName { get; set; }
        }

        public class StockInformation
        {
            public string Size { get; set; }
            public double Quantity { get; set; }
        }

        public class ShopItem
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public List<StockItem> StockItems { get; set; }
        }

        public class StockItem
        {
            public bool InStock { get; set; }
            public List<StockInformation> StockInformation { get; set; }
        }

        public class ClothingItemSizesSave
        {
            public Guid Id { get; set; }
            public Guid SizeId { get; set; }
            public Guid ClothingItemId { get; set; }
            public Guid ShopId { get; set; }
            public double Quantity { get; set; }
        }

        public class ShopsStockFilter
        {
            public string Shop { get; set; }
            public Guid ItemId { get; set; }
            public bool Stock { get; set; }
        }

        public class ShopsInAndOutOfStock
        {
            public List<string> ShopsStock { get; set; }
            public Guid ItemName { get; set; }
            public bool HasStock { get; set; }
        }

        public class SizesInAndOutOfStock
        {
            public List<string> SizesInStock { get; set; }
            public double? Quantity { get; set; }
            public List<string> SizesOutOfStock { get; set; }
        }

        public class SizesStockFilter
        {
            public string Size { get; set; }
            public Guid ItemId { get; set; }
            public bool Stock { get; set; }
            public double Quantity { get; set; }
        }
    }
}