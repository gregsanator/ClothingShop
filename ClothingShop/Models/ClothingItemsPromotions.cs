using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClothingShop.Models
{
    public class ClothingItemsPromotions
    {
        public Guid Id { get; set; }


        [ForeignKey("ClothingItem")]
        public Guid ClothingItemId { get; set; }
        public ClothingItems ClothingItem { get; set; }


        [ForeignKey("Promotion")]
        public Guid PromotionId { get; set; }
        public Promotions Promotion { get; set; }
    }
}