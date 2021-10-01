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
        public Guid CategoryId { get; set; }
        public Subcategories Subcategory { get; set; }


        public string Name { get; set; }
        public bool Availability { get; set; }
        public int Gender { get; set; }
        public string BrandName { get; set; }
    }
}