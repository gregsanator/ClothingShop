using ClothingShop.DTO;
using ClothingShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ClothingShop.Controllers
{
    public class PromotionsController : ApiController
    {
        [Route("api/clothingshop/promotions/list")]
        [HttpGet]
        public IHttpActionResult List()
        {
            PromotionsService service = new PromotionsService();
            List<PromotionsListItem> list = service.List();
            return Ok(list);
        }

        [Route("api/clothingshop/promotions/details/{id}")]
        [HttpGet]
        public IHttpActionResult Details(Guid id)
        {
            PromotionsService service = new PromotionsService();
            PromotionsForm details = service.Details(id);
            return Ok(details);
        }

        [Route("api/clothingshop/promotions/save")]
        [HttpPost]
        public IHttpActionResult Save(PromotionsSave save)
        {
            PromotionsService service = new PromotionsService();
            bool success = service.Save(save);
            return Ok(success);
        }

        [Route("api/clothingshop/promotions/itemsinpromotion/{id}")]
        [HttpGet]
        public IHttpActionResult Permissions(Guid id)
        {
            PromotionsService service = new PromotionsService();
            List<ClothingItemPromotion> clothingItems = service.ItemsInPromotion(id);
            return Ok(clothingItems);
        }

        [Route("api/clothingshop/promotions/adddiscounttoitems")]
        [HttpPost]
        public IHttpActionResult AddDiscountToItems(SelectedItemsEnable model)
        {
            PromotionsService service = new PromotionsService();
            bool success = service.AddDiscountToItems(model);
            return Ok(success);
        }

        [Route("api/clothingshop/promotions/delete/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            PromotionsService service = new PromotionsService();
            bool success = service.Delete(id);
            return Ok(success);
        }
    }
}