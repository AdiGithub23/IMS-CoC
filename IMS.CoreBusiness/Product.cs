using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.CoreBusiness
{
    public class Product
    {
        public int ProductID { get; set; }
        [Required]
        [StringLength(150)]
        public string ProductName { get; set; } = string.Empty;
        [Range(0, int.MaxValue, ErrorMessage = "Must be greater than or equal to 0")]
        public int Quantity { get; set; }
        [Range(5, int.MaxValue, ErrorMessage = "Must be greater than or equal to 10")]
        public double Price { get; set; }

        [Required]
        public string? ImgUrl { get; set; }

        public int BranchQty { get; set; }

        [NotMapped]
        public int NewQuantity { get; set; } // This property is not mapped to the database
    }
}
