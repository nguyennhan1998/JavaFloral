using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaFloral.Models
{
    public class CommentProduct
    {
        public int CommentID { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public Comment Comment { get; set; }
    }
}
