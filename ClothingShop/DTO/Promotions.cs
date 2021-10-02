using ClothingShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothingShop.DTO
{
    public class PromotionsListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double DiscoundPercantage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class PromotionsForm
    {
        public string Name { get; set; }
        public double DiscoundPercantage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<string> ClothingItemsName { get; set; }
    }

    public class PromotionsSave
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double DiscoundPercantage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class ClothingItemPromotion
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public bool Enabled { get; set; }
    }

    public class ClothingItemInPromotionEnabled
    {
        public Guid ClothingItemId { get; set; }
        public Guid PromotionId { get; set; }
    }
}