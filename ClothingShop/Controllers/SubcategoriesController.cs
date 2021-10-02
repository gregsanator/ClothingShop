using ClothingShop.DTO;
using ClothingShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ClothingShop.Controllers
{
    public class SubcategoriesController : ApiController
    {
        [Route("api/clothingshop/Subcategories/list")]
        [HttpGet]
        public IHttpActionResult List(Guid id)
        {
            SubcategoriesService service = new SubcategoriesService();
            List<SubcategoriesListItem> list = service.List(id);
            return Ok(list);
        }


        [Route("api/clothingshop/Subcategories/save")]
        [HttpPost]
        public IHttpActionResult Save(SubcategoriesSave save)
        {
            SubcategoriesService service = new SubcategoriesService();
            bool success = service.Save(save);
            return Ok(success);
        }
    }
}