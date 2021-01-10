using JavaFloral.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavaFloral.ViewModels
{
    public class CheckoutViewModel
    {
        public List<Cart> Carts { get; set; }
        public IdentityUser Users { get; set; }
    }
}
