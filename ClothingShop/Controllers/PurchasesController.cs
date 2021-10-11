using ClothingShop.DTO;
using ClothingShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ClothingShop.Controllers
{
    public class PurchasesController : ApiController
    {
        [HttpGet]
        [Route("api/clothingshop/purchases/list")]
        public IHttpActionResult List(Guid? id)
        {
            PurchasesService service = new PurchasesService();
            List<PurchasesListItem> list = service.List(id);
            return Ok(list);
        }
    }
}