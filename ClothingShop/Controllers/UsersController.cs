using ClothingShop.DTO;
using ClothingShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ClothingShop.Controllers
{
    public class UsersController : ApiController
    {
        [Route("api/clothingshop/users/list")]
        [HttpGet]
        public IHttpActionResult List()
        {
            UsersService service = new UsersService();
            List<UsersListItem> list = service.List();
            return Ok();
        }

        [Route("api/clothingshop/users/details/{id}")]
        [HttpGet]
        public IHttpActionResult List(Guid id)
        {
            UsersService service = new UsersService();
            UsersForm user = service.Details(id);
            return Ok(user);
        }

        [Route("api/clothingshop/users/save")]
        [HttpPost]
        public IHttpActionResult Save(UsersSave model)
        {
            UsersService service = new UsersService();
            bool user = service.Save(model);
            return Ok(user);
        }
    }
}