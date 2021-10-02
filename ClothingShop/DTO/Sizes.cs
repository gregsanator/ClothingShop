using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothingShop.DTO
{
    public class Sizes
    {
        public class SizesListItem
        {
            public Guid Id { get; set; }
            public string Size { get; set; }
            public int SizeType { get; set; }
        }

        public class SizesSave
        {
            public Guid Id { get; set; }
            public string Size { get; set; }
            public int SizeType { get; set; }

        }

        public class ClothingItemSizes
        {
            public Guid SizeId { get; set; }
            public string Size { get; set; }
            public bool Enabled { get; set; }
        }
    }
}