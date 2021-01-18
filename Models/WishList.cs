using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaFloral.Models
{
    public class WishList
    {
        public int WishListID { get; set; }
        public Product Product { get; set; }
        public string UserID { get; set; }
    }
}
