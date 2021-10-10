using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClothingShop.Models
{
    public class ClothingItemsSizes
    {
        public Guid Id { get; set; }


        [ForeignKey("ClothingItem")]
        public Guid ClothingItemId { get; set; }
        public ClothingItems ClothingItem { get; set; }


        [ForeignKey("Size")]
        public Guid SizeId { get; set; }
        public Sizes Size { get; set; }


        [ForeignKey("Shop")]
        public Guid ShopId { get; set; }
        public Shops Shop { get; set; }

        public double Quantity { get; set; }
    }
}