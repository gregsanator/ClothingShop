using ClothingShop.DTO;
using ClothingShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ClothingShop.Controllers
{
    public class CartsController : ApiController
    {
        [Route("api/clothingshop/carts/list/{id}")]
        [HttpGet]
        public IHttpActionResult List(Guid id)
        {
            CartsService service = new CartsService();
            CartsListItem list = service.List(id);
            return Ok(list);
        }


        [Route("api/clothingshop/carts/additemtocart")]
        [HttpPost]
        public IHttpActionResult AddItemToCart(CartsSave save)
        {
            CartsService service = new CartsService();
            bool success = service.AddItemToCart(save);
            return Ok(success);
        }

        [Route("api/clothingshop/carts/delete/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteItemFromCart(Guid id)
        {
            CartsService service = new CartsService();
            bool success = service.DeleteItemFromCart(id);
            return Ok(success);
        }

        [Route("api/clothingshop/carts/purchaseitems")]
        [HttpPost]
        public IHttpActionResult PurchaseItems(Guid id)
        {
            CartsService service = new CartsService();
            bool success = service.PurchaseItems(id);
            return Ok(success);
        }
    }
}