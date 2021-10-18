using ClothingShop.DTO;
using ClothingShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothingShop.Services
{
    public class ClubCardsService
    {
        public List<ClubCardsListItem> List()
        {
            using (var context = new ClothingShopDbContext())
            {
                List<ClubCardsListItem> list = context.ClubCards.Select(a => new ClubCardsListItem
                {
                    Id = a.Id,
                    UserName = a.User.Name + " " + a.User.Surname,
                    AdministratorName = a.Administrator.Name + " " + a.Administrator.Surname,
                    Enabled = a.Enabled
                }).ToList();
                return list;
            }
        }

        public ClubCardsForm Details(Guid id)
        {
            using (var context = new ClothingShopDbContext())
            {
                ClubCardsForm cardDetails = context.ClubCards.Where(a => a.Id == id).Select(a => new ClubCardsForm
                {
                    UserName = a.User.Name + " " + a.User.Surname,
                    AdministratorName = a.Administrator.Name + " " + a.Administrator.Surname,
                    Enabled = a.Enabled,
                    DateCreated = a.DateCreated,
                    DiscountPercantage = a.DiscountPercantage,
                    Points = a.Points
                }).FirstOrDefault();
                return cardDetails;
            }
        }

        public bool Save(ClubCardsSave model)
        {
            using (var context = new ClothingShopDbContext())
            {
                ClubCards card = new ClubCards
                {
                    Id = model.Id,
                    AdministratorId = model.AdministratorId,
                    UserId = model.UserId,
                    Points = model.Points,
                    DiscountPercantage = model.DiscountPercantage,
                    DateCreated = model.DateCreated
                };

                if (card.Id != Guid.Empty)
                {
                    context.ClubCards.Attach(card);
                    context.Entry(card).Property(a => a.Points).IsModified = true;
                    context.Entry(card).Property(a => a.DiscountPercantage).IsModified = true;
                }
                else
                {
                    card.Enabled = true;
                    context.ClubCards.Add(card);
                }
                context.SaveChanges();
                return true;
            }
        }
    }
}