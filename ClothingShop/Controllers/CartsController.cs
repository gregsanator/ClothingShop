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
        [Route("api/clothingshop/carts/list")]
        [HttpGet]
        public IHttpActionResult List(Guid id)
        {
            CartsService service = new CartsService();
            List<CartsListItem> list = service.List(id);
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

        [Route("api/clothingshop/carts/totalprice/{id}")]
        [HttpGet]
        public IHttpActionResult TotalPrice(Guid id)
        {
            CartsService service = new CartsService();
            double totalPrice = service.TotalPrice(id);
            return Ok(totalPrice);
        }

        [Route("api/clothingshop/carts/totalprice/{id}")]
        [HttpGet]
        public IHttpActionResult CartsCheckOut(Guid id)
        {
            CartsService service = new CartsService();
            bool success = service.CartsCheckOut(id);
            return Ok(success);
        }
    }
}