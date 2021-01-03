using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JavaFloral.Models
{
    public class Blog
    {

        [Key]
        public int BlogID { get; set; }
        [Required, Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; }
        [Required, Column(TypeName = "nvarchar(50)")]
        public string Description { get; set; }
        [Required, Column(TypeName = "ntext")]
        public string Content { get; set; }
        public int ViewCount { get; set; }
        public int Status { get; set; }

    }
}
