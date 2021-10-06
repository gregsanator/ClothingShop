using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothingShop.DTO
{
    public class Brands
    {
        public class BrandsListItem
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class BrandsSave
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class UserBrandsFilter
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public bool Enabled { get; set; }
        }

        public class UserBrandsEnable
        {
            public Guid BrandId { get; set; }
            public Guid UserId { get; set; }
        }
    }
}