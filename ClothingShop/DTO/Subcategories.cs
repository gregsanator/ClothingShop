using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothingShop.DTO
{
    public class SubcategoriesListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double NumberOfItems { get; set; }
    }

    public class SubcategoriesSave
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
    }

    public class UserSubcategoriesFilter
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }

    public class UserSubcategoriesEnable
    {
        public Guid SubcategoryId { get; set; }
        public Guid UserId { get; set; }
    }
}