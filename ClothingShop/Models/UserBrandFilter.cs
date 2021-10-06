using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClothingShop.Models
{
    public class UserBrandFilter
    {
        public Guid Id { get; set; }


        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public Users User { get; set; }


        [ForeignKey("Brand")]
        public Guid BrandId { get; set; }
        public Brands Brand{ get; set; }
    }
}