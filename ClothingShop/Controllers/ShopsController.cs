using ClothingShop.DTO;
using ClothingShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ClothingShop.Controllers
{
    public class ShopsController : ApiController
    {
        [HttpGet]
        [Route("api/clothingshop/shops/list")]
        public IHttpActionResult List()
        {
            ShopsService service = new ShopsService();
            List<ShopsListItem> list = service.List();
            return Ok(list);
        }

        [HttpPost]
        [Route("api/clothingshop/shops/save")]
        public IHttpActionResult Save(ShopsSave model)
        {
            ShopsService service = new ShopsService();
            bool success = service.Save(model);
            return Ok(success);
        }
    }
}