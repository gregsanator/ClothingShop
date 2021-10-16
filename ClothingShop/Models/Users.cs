using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClothingShop.Models
{
    public class Users
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }


        [ForeignKey("ClubCards")]
        public Guid? ClubCardId { get; set; }
        public virtual ClubCards ClubCards { get; set; }
    }
}