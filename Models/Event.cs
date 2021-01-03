using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JavaFloral.Models
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }
        [Required, Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; }
        [Required, Column(TypeName = "ntext")]
        public string Description { get; set; }
        [Required, Column(TypeName = "nvarchar(255)")]
        public string Content { get; set; }

        public string Image { get; set; }
        [Required]
        public DateTime StartAt { get; set; }
        [Required]
        public DateTime EndAt { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }

    }
}
