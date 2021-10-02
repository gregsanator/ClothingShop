using ClothingShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using static ClothingShop.DTO.Categories;

namespace ClothingShop.Controllers
{
    public class CategoriesController : ApiController
    {
        [Route("api/clothingshop/categories/list")]
        [HttpGet]
        public IHttpActionResult List()
        {
            CategoriesService service = new CategoriesService();
            List<CategoriesListItem> list = service.List();
            return Ok(list);
        }


        [Route("api/clothingshop/categories/save")]
        [HttpPost]
        public IHttpActionResult Save(CategoriesSave save)
        {
            CategoriesService service = new CategoriesService();
            bool success = service.Save(save);
            return Ok(success);
        }
    }
}