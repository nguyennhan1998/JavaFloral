using JavaFloral.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JavaFloral.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public int ProductID { get; set; }
        public string Name { get; set; }
        [Required,NotMapped]
        public IFormFile ProductPicture { get; set; }
        [Required, Column(TypeName = "nvarchar(255)")]

        public string Description { get; set; }
        [Required, Column(TypeName = "nvarchar(50)")]
        public string Color { get; set; }
        [Required]
        public int Qty { get; set; }
        public string Content { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Smell { get; set; }
        public int CategoryID { get; set; }

        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public virtual Category Category { get; set; }
        public int Discount { get; set; }
    }
}
