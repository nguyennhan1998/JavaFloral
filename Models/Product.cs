using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace JavaFloral.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required, Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        [Required]
        public string ProductImage { get; set; }
        [Required, Column(TypeName = "nvarchar(255)")]

        public string Description { get; set; }
        [Required, Column(TypeName = "nvarchar(50)")]
        public string Color { get; set; }
        [Required]
        public int Qty { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Smell { get; set; }
        public int CategoryID { get; set; }

        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public virtual Category Category { get; set; }
        public ICollection<OrderProducts> OrderProducts { get; set; }
    }
}
