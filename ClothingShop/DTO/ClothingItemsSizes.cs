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
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Size { get; set; }
            public double Quantity { get; set; }
        }

        public class ClothingItemSizesSave
        {
            public Guid Id { get; set; }
            public Guid SizeId { get; set; }
            public Guid ClothingItemId { get; set; }
            public double Quantity { get; set; }
        }
    }
}