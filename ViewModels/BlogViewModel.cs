using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JavaFloral.ViewModels
{
    public class BlogViewModel
    {
        [Key]
        public int BlogID { get; set; }
        
        [Required,NotMapped]
        public IFormFile BlogImage { get; set; }
        public string Title { get; set; }
        [Required, Column(TypeName = "nvarchar(255)")]
        public string Description { get; set; }
        [Required, Column(TypeName = "ntext")]
        public string Content { get; set; }
        public int ViewCount { get; set; }
        public int Status { get; set; }
    }
}
