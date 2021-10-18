using ClothingShop.DTO;
using ClothingShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ClothingShop.Controllers
{
    public class ClubCardsController : ApiController
    {
        [Route("api/clothingshop/clubCards/list")]
        [HttpGet]
        public IHttpActionResult List()
        {
            ClubCardsService service = new ClubCardsService();
            List<ClubCardsListItem> list = service.List();
            return Ok(list);
        }

        [Route("api/clothingshop/clubCards/details/{id}")]
        [HttpGet]
        public IHttpActionResult Details(Guid id)
        {
            ClubCardsService service = new ClubCardsService();
            ClubCardsForm item = service.Details(id);
            return Ok(item);
        }

        [Route("api/clothingshop/clubCards/save")]
        [HttpPost]
        public IHttpActionResult Save(ClubCardsSave model)
        {
            ClubCardsService service = new ClubCardsService();
            bool success = service.Save(model);
            return Ok(success);
        }
    }
}