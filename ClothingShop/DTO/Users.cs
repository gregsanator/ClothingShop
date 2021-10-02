using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothingShop.DTO
{
    public class UsersListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public class UsersForm
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EMail { get; set; }
        public DateTime Birthday { get; set; }
    }

    public class UsersSave
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        public Guid AdministratorId { get; set; }
    }
}