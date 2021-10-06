using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothingShop.DTO
{
    public class PurchasesListItem
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ClothingItemSizeId { get; set; }
        public double Quantity { get; set; }
        public double TotalPrice { get; set; }
        public DateTime DateOrdered { get; set; }
        public double? DiscountPercantage { get; set; }
    }
}