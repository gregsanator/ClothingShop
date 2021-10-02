using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClothingShop.Models
{
    public class ClubCards // 1 - to - 1 relationship between user and clubcards
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public Users User { get; set; }


        [ForeignKey("Administrator")]
        public Guid AdministratorId { get; set; }
        public Administrators Administrator { get; set; }


        public DateTime DateCreated { get; set; }
        public double Points { get; set; }
        public double DiscountPercantage { get; set; }
        public bool Enabled { get; set; }

    }
}