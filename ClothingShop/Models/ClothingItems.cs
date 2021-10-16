using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClothingShop.Models
{
    public class ClothingItems
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }


        [ForeignKey("Subcategory")]
        public Guid SubcategoryId { get; set; }
        public Subcategories Subcategory { get; set; }


        [ForeignKey("Brand")]
        public Guid BrandId { get; set; }
        public Brands Brand { get; set; }


        public string Name { get; set; }
        public int Gender { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int SizeType { get; set; }
        public DateTime DateAdded { get; set; }
        public ICollection<ClothingItemsSizes> ClothingItemsSizes { get; set; }

    }
}