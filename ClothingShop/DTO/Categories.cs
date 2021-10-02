using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothingShop.DTO
{
    public class Categories
    {
        public class CategoriesListItem
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class CategoriesSave
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
    }
}