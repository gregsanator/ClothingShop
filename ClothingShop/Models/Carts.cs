using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClothingShop.Models
{
    public class Carts
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }


        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public Users User { get; set; }


        [ForeignKey("ClothingItemSize")]
        public Guid ClothingItemSizeId { get; set; }
        public ClothingItemsSizes ClothingItemSize { get; set; }

        public double Quantity { get; set; }
    }
}