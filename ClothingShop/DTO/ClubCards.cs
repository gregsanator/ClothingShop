using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothingShop.DTO
{
    public class ClubCardsListItem
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }
        public string AdministratorName { get; set; }
        public bool Enabled { get; set; }
    }

    public class ClubCardsForm
    {
        public string UserName { get; set; }
        public string AdministratorName { get; set; }
        public DateTime DateCreated { get; set; }
        public double Points { get; set; }
        public int DiscountPercantage { get; set; }
        public bool Enabled { get; set; }
    }

    public class ClubCardsSave
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid AdministratorId { get; set; }
        public DateTime DateCreated { get; set; }
        public double Points { get; set; }
        public int DiscountPercantage { get; set; }
        public bool Enabled { get; set; }
    }
}