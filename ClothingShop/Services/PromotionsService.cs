using ClothingShop.DTO;
using ClothingShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClothingItems = ClothingShop.Models.ClothingItems;

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

        public bool AddSelected(SelectedItemsEnable model) // add all the products of a given subcategory to a promotion(example. all hoodies have 30% discount)
        {
            using (var context = new ClothingShopDbContext())
            {
                IQueryable <ClothingItems> cItems = context.ClothingItems;

                if (model.SubcategoryId != null)
                    cItems = cItems.Where(a => a.SubcategoryId == model.SubcategoryId); 
                // see if he has selected subcategory chebox to add all items in subcateg

                else if (model.ClothingItemsId.Count != 0)
                    cItems = cItems.Where(a => model.ClothingItemsId.Contains(a.Id));

                else
                    return false;

                double discountPercantage = context.Promotions.Find(model.PromotionId).DiscountPercantage;

                    foreach (var item in cItems)
                    {
                        ClothingItemsPromotions cip = new ClothingItemsPromotions
                        {
                            ClothingItemId = item.Id,
                            PromotionId = model.PromotionId
                        };

                        ClothingItemsPromotions any = context.ClothingItemsPromotions.Where(a => a.ClothingItemId == item.Id).FirstOrDefault();
                        // check if there is existing promotion on item

                        if (any == null)
                        {
                            item.Price *= (1 - (discountPercantage / 100)); // calculate new price of item
                        }
                        else
                        {
                            item.Price /= (1 - (any.Promotion.DiscountPercantage / 100)); // bring back the original price
                            item.Price *= (1 - (discountPercantage / 100)); // calculate the new price
                            context.ClothingItemsPromotions.Remove(any); // remove old promotion
                        }
                        context.ClothingItemsPromotions.Add(cip); // add the item to the promotion
                    }
                    context.SaveChanges();
                return true;
            }
        }
    }
}