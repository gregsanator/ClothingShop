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
        public List<Guid> ClothingItemId { get; set; }
        public Guid PromotionId { get; set; }
    }

    public class SelectedItemsEnable
    {
        public Guid? SubcategoryId { get; set; }
        public List<Guid> ClothingItemsId { get; set; }
        public Guid PromotionId { get; set; }
    }

    public class CategoryPromotionEnabled
    {
        public Guid PromotionId { get; set; }
        public Guid SubcategoryId { get; set; }

    }

    public class SubcategoriesPromotion
    {
        public Guid Id { get; set; }
        public string SubcategoryName { get; set; }
        public bool Enabled { get; set; }
    }
}