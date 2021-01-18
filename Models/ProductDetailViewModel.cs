using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaFloral.Models
{
    public class ProductDetailViewModel
    {
        public Product product { get; set; }
        public List<Product> RelativeProduct { get; set; } 
    }
}
