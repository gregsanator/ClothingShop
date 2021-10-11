using ClothingShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using static ClothingShop.DTO.ClothingItemsSizes;

namespace ClothingShop.Controllers
{
    public class CothingItemsSizesController : ApiController
    {
        [HttpGet]
        [Route("api/clothingshop/clothingitemssizes/list/{brandName}")]
        public IHttpActionResult List(string brandName)
        {
            ClothingItemsSizesService service = new ClothingItemsSizesService();
            List<ClothingItemsSizesListItem> list = service.List(brandName);
            return Ok(list);
        }

        [HttpPost]
        [Route("api/clothingshop/clothingitemssizes/list/{brandName}")]
        public IHttpActionResult Save(ClothingItemSizesSave model)
        {
            ClothingItemsSizesService service = new ClothingItemsSizesService();
            bool success = service.Save(model);
            return Ok(model);
        }
    }
}