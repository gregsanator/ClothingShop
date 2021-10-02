using ClothingShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using static ClothingShop.DTO.Administrators;

namespace ClothingShop.Controllers
{
    public class AdministratorsController : ApiController
    {
        [Route("api/clothingshop/administrators/list")]
        [HttpGet]
        public IHttpActionResult List()
        {
            AdministratorsService service = new AdministratorsService();
            List<AdministratorsListItem> list = service.List();
            return Ok(list);
        }

        [Route("api/clothingshop/administrators/details/{id}")]
        [HttpGet]
        public IHttpActionResult Details(Guid id)
        {
            AdministratorsService service = new AdministratorsService();
            AdministratorsForm details = service.Details(id);
            return Ok(details);
        }

        [Route("api/clothingshop/administrators/save")]
        [HttpPost]
        public IHttpActionResult Save(AdministratorsSave save)
        {
            AdministratorsService service = new AdministratorsService();
            bool success = service.Save(save);
            return Ok(success);
        }

        [Route("api/clothingshop/administrators/enabled/{id}")]
        [HttpGet]
        public IHttpActionResult Enabled(Guid id)
        {
            AdministratorsService service = new AdministratorsService();
            bool enabled = service.Enabled(id);
            return Ok(enabled);
        }

        [Route("api/clothingshop/administrators/permissions/{id}")]
        [HttpGet]
        public IHttpActionResult Permissions(Guid id)
        {
            AdministratorsService service = new AdministratorsService();
            List<AdministratorPermissions> permissions = service.Permissions(id);
            return Ok(permissions);
        }

        [Route("api/clothingshop/administrators/enablepermission")]
        [HttpPost]
        public IHttpActionResult EnablePermission(AdministratorsPermissionEnabled model)
        {
            AdministratorsService service = new AdministratorsService();
            bool aPermission = service.AdministratorsPermissionEnabled(model);
            return Ok(aPermission);
        }
    }
}