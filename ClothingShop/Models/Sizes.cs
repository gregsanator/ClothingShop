using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClothingShop.Models
{
    public class Sizes
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }


        [ForeignKey("SizeType")]
        public Guid SizeTypeId { get; set; }
        public SizeTypes SizeType { get; set; }


        public int Size { get; set; }
        public int StockNumber { get; set; }
    }
}