using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClothingShop.Models
{
    public class UserSubcategoriesFilter
    {
        public Guid Id { get; set; }


        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public Users User { get; set; }


        [ForeignKey("Subcategory")]
        public Guid SubcategoryId { get; set; }
        public Subcategories Subcategory { get; set; }
    }
}