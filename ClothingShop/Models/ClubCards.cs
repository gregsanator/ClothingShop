using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClothingShop.Models
{
    public class ClubCards // 1 - to - 1 relationship between user and clubcards
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }


        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public Users User { get; set; }


        [ForeignKey("Administrator")]
        public Guid AdministratorId { get; set; }
        public Administrators Administrator { get; set; }


        public DateTime DateCreated { get; set; }
        public double Points { get; set; }
        public int DiscountPercantage { get; set; }
        public bool Enabled { get; set; }

    }
}