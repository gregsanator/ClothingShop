using ClothingShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using static ClothingShop.DTO.Sizes;

namespace ClothingShop.Controllers
{
    public class SizesController : ApiController
    {
        [Route("api/clothingshop/sizes/list")]
        [HttpGet]
        public IHttpActionResult List()
        {
            SizesService service = new SizesService();
            List<SizesListItem> list = service.List();
            return Ok(list);
        }

        [Route("api/clothingshop/sizes/save")]
        [HttpPost]
        public IHttpActionResult Save(SizesSave save)
        {
            SizesService service = new SizesService();
            bool success = service.Save(save);
            return Ok(success);
        }

        [Route("api/clothingshop/sizes/permissions/{id}")]
        [HttpGet]
        public IHttpActionResult Sizes(Guid id)
        {
            SizesService service = new SizesService();
            List<ClothingItemSizes> sizes = service.Sizes(id);
            return Ok(sizes);
        }
    }
}