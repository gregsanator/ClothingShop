using ClothingShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using static ClothingShop.DTO.Brands;

namespace ClothingShop.Controllers
{
    public class BrandsController : ApiController
    {
        [HttpGet]
        [Route("api/clothingshop/brands/list")]
        public IHttpActionResult List()
        {
            BrandsService service = new BrandsService();
            List<BrandsListItem> list = service.List();
            return Ok(list);
        }

        [HttpPost]
        [Route("api/clothingshop/brands/save")]
        public IHttpActionResult Save(BrandsSave model)
        {
            BrandsService service = new BrandsService();
            bool success = service.Save(model);
            return Ok(success);
        }
    }
}