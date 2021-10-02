using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothingShop.DTO
{
    public class Administrators
    {
        public class AdministratorsListItem
        {
            public Guid Id { get; set; }
            public string EMail { get; set; }
            public bool Enabled { get; set; }
        }

        public class AdministratorsForm
        {
            public string EMail { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public bool Enabled { get; set; }
            public DateTime Birthday { get; set; }
        }

        public class AdministratorsSave
        {
            public Guid Id { get; set; }
            public string EMail { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime Birthday { get; set; }
            public bool Enabled { get; set; }
        }

        public class AdministratorPermissions
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public bool Enabled { get; set; }
        }

        public class AdministratorsPermissionEnabled
        {
            public Guid AdministratorId { get; set; }
            public Guid PermissionId { get; set; }
        }
    }
}