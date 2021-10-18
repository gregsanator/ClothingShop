using ClothingShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ClothingShop.DTO.ClothingItemsSizes;

namespace ClothingShop.Services
{
    public class ClothingItemsSizesService
    {
        //This service is for the head administrators to see the total stock number of a ClothingItemSIze
        public List<ShopItem> ListStockInfo(Guid id)
        {
            using (var context = new ClothingShopDbContext())
            {
                IQueryable<ClothingItems> items = context.ClothingItems.Where(a => a.Id == id);
                
               //group all items where clothingItemId is equal to passed id(admin clicks on air max 7 item)
               List<ShopItem> list = (from l in context.ClothingItemsSizes.Where(a => a.ClothingItemId == id)
                                      group l by new { l.ShopId, l.Shop.Name } into cisGroup
                                      select new ShopItem
                                      {
                                          Id = cisGroup.Key.ShopId,
                                          Name = cisGroup.Key.Name,
                                          StockItems = cisGroup.GroupBy(a => a.InStock).Select(a => new StockItem
                                          {
                                              InStock = a.Key,
                                              StockInformation = a.Select(b => new StockInformation
                                              {
                                                  Quantity = b.Quantity,
                                                  Size = b.Size.Size
                                              }).ToList()
                                          }).ToList()
                                      }).ToList();

                return list;
            }
        }

        public List<ClothingItemsSizesListItem> List(string brandName) //global management of the shop
        {
            using (var context = new ClothingShopDbContext())
            {
                IQueryable<ClothingItemsSizes> itemsSizes = context.ClothingItemsSizes;
                if (!string.IsNullOrEmpty(brandName))
                    itemsSizes = itemsSizes.Where(a => a.ClothingItem.Brand.Name == brandName);

                itemsSizes.GroupBy(a => a.ClothingItemId).Select(x => x.First());// eliminate all duplicate clothingItems so that we are left only with one sample of the item

                List<ClothingItemsSizesListItem> list = itemsSizes.Select(a => new ClothingItemsSizesListItem
                {
                    ItemName = a.ClothingItem.Name,
                    ItemId = a.ClothingItemId
                }).ToList();

                return list;
            }
        }


        public bool Save(ClothingItemSizesSave model)
        // Add the quantity of stock of a certain product with a certain size
        {
            using (var context = new ClothingShopDbContext())
            {
                ClothingItemsSizes ciss = new ClothingItemsSizes
                {
                    Id = model.Id,
                    ClothingItemId = model.ClothingItemId,
                    SizeId = model.SizeId,
                    Quantity = model.Quantity,
                    ShopId = model.ShopId,
                    InStock = model.Quantity > 0
                };

                if (ciss.Id != Guid.Empty)
                {
                    context.ClothingItemsSizes.Attach(ciss);
                    context.Entry(ciss).Property(a => a.Quantity).IsModified = true;
                    context.Entry(ciss).Property(a => a.SizeId).IsModified = true;
                    context.Entry(ciss).Property(a => a.ClothingItemId).IsModified = true;
                    context.Entry(ciss).Property(a => a.ShopId).IsModified = true;
                }

                else
                    context.ClothingItemsSizes.Add(ciss);
                context.SaveChanges();
                return true;
            }
        }
    }
}