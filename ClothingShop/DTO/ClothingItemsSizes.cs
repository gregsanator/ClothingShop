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
            public Guid ClothingItemId { get; set; }
            public string Name { get; set; }
            public List<string> SizesInStock { get; set; }
            public List<string> SizesNotInStock { get; set; }
            public List<string> LocationsInStock { get; set; }
            public List<string> LocationsOutOfStock { get; set; }
            public double Quantity { get; set; }
        }

        public class ClothingItemSizesSave
        {
            public Guid Id { get; set; }
            public Guid SizeId { get; set; }
            public Guid ClothingItemId { get; set; }
            public Guid ShopId { get; set; }
            public double Quantity { get; set; }
        }
    }
}