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
        public List<StockInformation> ListStockInfo(Guid id)
        {
            using (var context = new ClothingShopDbContext())
            {
                //group all items where clothingItemId is equal to passed id(admin clicks on air max 7 item)
               List<StockInformation> list = (from l in context.ClothingItemsSizes.Where(a => a.ClothingItemId == id)
                        group l by new { l.ClothingItemId, l.Shop.Name, l.InStock} into cisGroup
                        select new StockInformation
                        {
                            ItemId = cisGroup.Key.ClothingItemId, //item id // it gives error because of both prop in clothingItem and shop have prop Name
                            Shop = cisGroup.Key.Name, // shop name
                            InStock = cisGroup.Key.InStock, // is itemSize in stock
                            ItemSizes = cisGroup.Select(a => a.Size.Size).ToList(),
                            Quantity = cisGroup.Sum(a => a.Quantity)
                            // all the sizes that are/aren't in stock for that item in a shop
                        }).ToList();
                return list;
            }
        }

        public List<ClothingItemsSizesListItem> List(string brandName) //global management of the shop
        {
            using (var context = new ClothingShopDbContext())
            {
                IQueryable<ClothingItemsSizes> itemsSizes = context.ClothingItemsSizes;
                if (brandName != null)
                    itemsSizes = itemsSizes.Where(a => a.ClothingItem.Brand.Name == brandName);

                itemsSizes.Select(a => a.ClothingItemId).Distinct(); // eliminate all duplicate clothingItems so that we are left only with one sample of the item

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
                    ShopId = model.ShopId
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
                    context.ClothingItemsSizes.Attach(ciss);
                context.SaveChanges();
                return true;
            }
        }
    }
}