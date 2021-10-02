using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothingShop.DTO
{
    public class ClothingItems
    {
        public class ClothingItemsListItem
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public bool Availability { get; set; }
            public double Price { get; set; }
        }

        public class ClothingItemsForm
        {
            public string Name { get; set; }
            public bool Availability { get; set; }
            public int Gender { get; set; }
            public string BrandName { get; set; }
            public string Description { get; set; }
            public double Price { get; set; }
        }

        public class ClothingItemsSave
        {
            public Guid Id { get; set; }
            public Guid SubcategoryId { get; set; }
            public Guid SizeId { get; set; }
            public string Name { get; set; }
            public int Gender { get; set; }
            public string BrandName { get; set; }
            public string Description { get; set; }
            public double Price { get; set; }
            public int SizeType { get; set; }

        }
    }
}