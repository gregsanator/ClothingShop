using ClothingShop.DTO;
using ClothingShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothingShop.Services
{
    public class PromotionsService
    {
        public List<PromotionsListItem> List() // returns a list of all promotions
        {
            using (var context = new ClothingShopDbContext())
            {
                List<PromotionsListItem> list = context.Promotions.Select(a => new PromotionsListItem
                {
                    Id = a.Id,
                    Name = a.Name,
                    DiscoundPercantage = a.DiscountPercantage,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate
                }).ToList();
                return list;
            }
        }

        public PromotionsForm Details(Guid id) // returns da details of the promotion and all clothesitems that are included in the promotion
        {
            using (var context = new ClothingShopDbContext())
            {
                PromotionsForm details = context.Promotions.Where(a => a.Id == id).Select(a => new PromotionsForm
                {
                    Name = a.Name,
                    DiscoundPercantage = a.DiscountPercantage,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                    ClothingItemsName = a.ClothingItemsPromotions.Select(b => b.ClothingItem.Name).ToList()
                }).FirstOrDefault();
                return details;
            }
        }
        public bool Save(PromotionsSave model) // make a new promotion
        {
            using (var context = new ClothingShopDbContext())
            {
                Promotions promotion = new Promotions
                {
                    Id = model.Id,
                    Name = model.Name,
                    DiscountPercantage = model.DiscoundPercantage,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate
                };

                if (promotion.Id != Guid.Empty)
                {
                    context.Promotions.Attach(promotion);
                    context.Entry(promotion).Property(a => a.Name).IsModified = true;
                    context.Entry(promotion).Property(a => a.DiscountPercantage).IsModified = true;
                    context.Entry(promotion).Property(a => a.StartDate).IsModified = true;
                    context.Entry(promotion).Property(a => a.EndDate).IsModified = true;
                }
                else
                    context.Promotions.Add(promotion);
                context.SaveChanges();
                return true;
            }
        }

        public List<ClothingItemPromotion> ItemsInPromotion(Guid id) // returns all the items and informations which items are in the promotion
        {
            using (var context = new ClothingShopDbContext())
            {
                List<ClothingItemPromotion> itemsInPromotion = (from c in context.ClothingItems
                                                    join cip in context.ClothingItemsPromotions.Where(a => a.PromotionId == id)
                                                    on c.Id equals cip.ClothingItemId into joinedccip
                                                    from ccip in joinedccip.DefaultIfEmpty()
                                                    select new ClothingItemPromotion
                                                    {
                                                        Id = c.Id,
                                                        ProductName = c.Name,
                                                        Enabled = ccip != null
                                                    }).ToList();
                return itemsInPromotion;
            }
        }


        public bool AddItemToPromotion(ClothingItemInPromotionEnabled model) // list with products and checkboxes when click checkbox call this service
        {
            using (var context = new ClothingShopDbContext())
            {
                ClothingItemsPromotions clothingItem = context.ClothingItemsPromotions
                    .Where(a => a.ClothingItemId == model.ClothingItemId && a.PromotionId == model.PromotionId).FirstOrDefault();

                Models.ClothingItems item = context.ClothingItems.Where(a => a.Id == clothingItem.ClothingItemId).FirstOrDefault();

                if (clothingItem != null)
                {
                    item.Price /= (1 - (clothingItem.Promotion.DiscountPercantage / 100));
                    // Changeing the price back to the original one without any discount
                    context.ClothingItemsPromotions.Remove(clothingItem);
                }

                else
                {
                    item.Price *= (1 - (clothingItem.Promotion.DiscountPercantage / 100));
                    // Adding discount to the price
                    context.ClothingItemsPromotions.Add(clothingItem);
                }
                return true;
            }
        }
    }
}