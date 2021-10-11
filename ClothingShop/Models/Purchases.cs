﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClothingShop.Models
{
    public class Purchases
    {
        public Guid Id { get; set; }


        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public Users User { get; set; }


        [ForeignKey("ClothingItemSize")]
        public Guid ClothingItemSizeId { get; set; }
        public ClothingItemsSizes ClothingItemSize { get; set; }

        [ForeignKey("Promotion")]
        public Guid? PromotionId { get; set; }
        public Promotions Promotion { get; set; }


        public double Quantity { get; set; }
        public double TotalPrice { get; set; }
        public DateTime DateOrdered { get; set; }
        public double? DiscountPercantage { get; set; }
    }
}