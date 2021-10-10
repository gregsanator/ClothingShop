using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothingShop.DTO
{
    public class CartsListItem
    {
        public List<CartsList> CartList { get; set; }
        public double TotalPrice { get; set; }
    }

    public class CartsList
    {
        public Guid Id { get; set; }
        public string Size { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double PriceAfterDiscount { get; set; }
    }

    public class CartsSave
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ClothingItemSizeId { get; set; }
        public double Quantity { get; set; }
    }

    public class CartsPrice
    {

        public double Price { get; set; }
    }

    public class CartsOrder
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public Guid ClothingItemSizeId { get; set; }
        public double Quantity { get; set; }
        public double TotalPrice { get; set; }
        public DateTime DateOrdered { get; set; }
        public double DiscountPercantage { get; set; }
    }
}