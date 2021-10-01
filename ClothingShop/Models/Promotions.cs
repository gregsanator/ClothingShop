using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClothingShop.Models
{
    public class Promotions
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }


        [ForeignKey("ClothingItem")]
        public Guid ClothingItemId { get; set; }
        public ClothingItems ClothingItem { get; set; }


        public double DiscountPercantage { get; set; }
        public string DiscountName { get; set; }
    }
}