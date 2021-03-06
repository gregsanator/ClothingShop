using ClothingShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using static ClothingShop.DTO.ClothingItems;

namespace ClothingShop.Controllers
{
    public class ClothingItemsController : ApiController
    {
        [Route("api/clothingshop/clothingItems/details/{id}")]
        [HttpGet]
        public IHttpActionResult Details(Guid id)
        {
            ClothingItemsService service = new ClothingItemsService();
            ClothingItemsForm item = service.Details(id);
            return Ok(item);
        }

        [Route("api/clothingshop/clothingItems/save")]
        [HttpPost]
        public IHttpActionResult Save(ClothingItemsSave model)
        {
            ClothingItemsService service = new ClothingItemsService();
            bool success = service.Save(model);
            return Ok(success);
        }
    }
}