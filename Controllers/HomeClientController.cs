using JavaFloral.Data;
using JavaFloral.Models;
using JavaFloral.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using System;

namespace JavaFloral.Controllers
{
    public class HomeClientController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        // GET: HomeClientController
        public HomeClientController(UserManager<IdentityUser> userManager, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public ActionResult Index(int id)
        {
            var vm = new ProductListViewModel();
            vm.Categories = _context.Categories.ToList();
            vm.Products = _context.Products.Where(p => p.CategoryID == id).OrderByDescending(b => b.Created_at).ToList();

            return View(vm);
        }

        // GET: HomeClientController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeClientController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeClientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeClientController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeClientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeClientController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeClientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Product(int id, string color, string soft)
        {

            var pvm = new ProductListViewModel();
            pvm.Categories = _context.Categories.ToList();
            pvm.Products = _context.Products.ToList();

            if (id != 0)
            {
                pvm.Products = _context.Products.Where(c => c.CategoryID == id).ToList();
            }
            if (!string.IsNullOrEmpty(color))
            {
                pvm.Products = _context.Products.Where(b => b.Color.ToLower().Contains(color.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(soft) && soft.Equals("Name"))
            {
                pvm.Products = _context.Products.OrderByDescending(p => p.Name).ToList();

            }
            if (!string.IsNullOrEmpty(soft) && soft.Equals("Price"))
            {
                pvm.Products = _context.Products.OrderByDescending(p => p.Price).ToList();

            }
            return View(pvm);
        }
        public async Task<IActionResult> Blog()
        {

            return View(await _context.Blogs.ToListAsync());
        }
        public ActionResult BlogDetails(int id)
        {
            var blog = _context.Blogs.Where(b => b.BlogID == id).FirstOrDefault();
            return View(blog);

        }
        public ActionResult About()
        {

            return View();
        }
        public ActionResult Contact()
        {

            return View();
        }
        public IActionResult RemoveCart(int id)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.ProductID == id);
            if (cartitem != null)
            {

                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(CartPage));
        }
        public IActionResult UpdateCart(int productid, int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.ProductID == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity = quantity;
            }
            SaveCartSession(cart);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return Ok();
        }
        public const string CARTKEY = "cart";

        // Lấy cart từ Session (danh sách CartItem)
        List<Cart> GetCartItems()
        {

            var session = _httpContextAccessor.HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<Cart>>(jsoncart);
            }

            return new List<Cart>();
        }

        // Xóa cart khỏi session
        void ClearCart()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            session.Remove(CARTKEY);
        }

        public IActionResult ClearAllCart()
        {
            ClearCart();
            return RedirectToAction(nameof(CartPage));
        }

        // Lưu Cart (Danh sách CartItem) vào session
        void SaveCartSession(List<Cart> ls)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);

        }
        public IActionResult CartPage()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            session.GetString(CARTKEY);
            return View(GetCartItems());
        }
        public IActionResult AddToCart(int id,int quantity) {
         

            var product = _context.Products.Where(p => p.ProductID == id).FirstOrDefault();
            if(quantity == 0)
            {
                quantity = 1;
            }
            if(product.Qty < quantity)
            {
                return Json(new { status = "outofstock" });
            }
            
            if (product == null)
                return NotFound("Không có sản phẩm");

            // Xử lý đưa vào Cart ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.ProductID == id);
            if (cartitem != null)
            {
                if(quantity != 0)
                {
                    cartitem.quantity += quantity;
                }
              
             
            }
            else
            {
                //  Thêm mới
                cart.Add(new Cart() { quantity = quantity, product = product });
            }

            // Lưu cart vào Session
            SaveCartSession(cart);

            Console.WriteLine(JsonConvert.SerializeObject(cart));
            // Chuyển đến trang hiện thị Cart

            return Json(new { status = "true" });

        }
        [Authorize]
        public async Task<ActionResult> CheckOut()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var cvm = new CheckoutViewModel();
            cvm.Users = currentUser;
            cvm.Carts = GetCartItems();


            return View(cvm);
        }

        public async Task<ActionResult> SaveOrder(string receiveddate, string name, string address, string message, int paymenttype, string telephone, string emailto)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            var cart = GetCartItems();
        
            decimal grandTotal = 0;
            foreach (var item in cart)
            {
                var total = item.quantity * item.product.Price;
                grandTotal += total;
            }


            Orders order = new Orders
            {
                OrderName = currentUser.UserName,
                Name = !string.IsNullOrEmpty(name) ? name : "",
                Status = 3,
                GrandTotal = grandTotal,
                CreateAt = DateTime.Now,
                UserID = currentUser.UserName,
                ReceivedDate = receiveddate,
                address = !string.IsNullOrEmpty(address) ? address : "",
                message = !string.IsNullOrEmpty(message) ? message : "",
                telephone = !string.IsNullOrEmpty(telephone) ? telephone : "",
                paymenttype = paymenttype != 0 ? paymenttype : 2

            };
            _context.Add(order);
            _context.SaveChanges();

            foreach (var item in cart)
            {
                OrderProducts orderProducts = new OrderProducts();
                orderProducts.OrderID = order.ID;
                orderProducts.ProductID = item.product.ProductID;
                orderProducts.UserID = currentUser.UserName;
                orderProducts.quantity = item.quantity;

                _context.Add(orderProducts);
                _context.SaveChanges();
                var product = _context.Products.Where(p => p.ProductID == item.product.ProductID).FirstOrDefault();
                product.Qty -= item.quantity;
                _context.Update(product);
                await _context.SaveChangesAsync();

            }
            if (!string.IsNullOrEmpty(emailto))
            {
                MimeMessage ms = new MimeMessage();
                MailboxAddress from = new MailboxAddress("Admin", currentUser.Email);
                ms.From.Add(from);
                MailboxAddress to = new MailboxAddress("User", emailto);
                ms.To.Add(to);
                ms.Subject = "Message From : " + currentUser.Email;
                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = message;
                ms.Body = bodyBuilder.ToMessageBody();
                SmtpClient client = new SmtpClient();
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("nguyennhanlovemyself@gmail.com", "lpnfwtgayamgywyd");
                client.Send(ms);
                client.Disconnect(true);
                client.Dispose();
                
            }
            var session = _httpContextAccessor.HttpContext.Session;
            session.Remove(CARTKEY);
            
            return Json(new { status = "true" });
           
        }
        public ActionResult LoginRegister()
        {

            return View();
        }
        public ActionResult MyAccount()
        {

            return View();
        }
        public ActionResult ProductDetail(int id)
        {
            var product = _context.Products
            .Include(c => c.Category)
            .Include(cp => cp.CommentProducts)
            .ThenInclude(cp => cp.Comment)
            .ThenInclude(ca => ca.CommentAnswers)
            .ThenInclude(ca => ca.Answer)
            .Where(c => c.ProductID == id).FirstOrDefault();  
                var vm = new ProductDetailViewModel();
                vm.product = product;
                vm.RelativeProduct = _context.Products.Where(p => p.CategoryID == product.CategoryID).Where(p => p.ProductID != product.ProductID).ToList();
                
            return View(vm);
        }
        [HttpPost]
        public async Task<ActionResult> AddWishList(int id)
        {
            Console.WriteLine(id);
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            if(id != 0)
            {
                if (_context.WishLists.Any(w => w.Product.ProductID == id && w.UserID.Equals(currentUser.Id)))
                {
                    return Json(new { status = "exists" });

                }
                var product = _context.Products.Where(p => p.ProductID == id).FirstOrDefault();

                var wishlist = new WishList();
                wishlist.Product = product;
                wishlist.UserID = currentUser.Id;
                _context.Add(wishlist);
                await _context.SaveChangesAsync();
            }
            

            return Json(new { status = "true" });
        }
        public async Task<IActionResult> DeleteWishList(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            if (id != 0)
            {
                var wishlist = _context.WishLists.Where(w => w.WishListID == id && w.UserID.Equals(currentUser.Id)).FirstOrDefault();
                _context.WishLists.Remove(wishlist);
                await _context.SaveChangesAsync();
            }
            return Json(new { status = "true" });
        }
        public async Task<ActionResult> WishList()
        {
            var currentUser =  await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            var wishlist = await _context.WishLists.Include(p => p.Product).ThenInclude(c=>c.Category)
                .Where(w => w.UserID.Equals(currentUser.Id)).ToListAsync();
            var wvm = new WishListViewModel();
            wvm.WishList = wishlist;
               
              
                
            return View(wvm);
        }
        [HttpPost]
        
        public async Task<ActionResult> Comment(int productID,string message,int rating)
        {

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            if (!string.IsNullOrEmpty(message))
            {
                Comment comment = new Comment()
                {
                    Message = message,
                    Status = 1,
                    UserID = currentUser.UserName,
                    CommentTime = DateTime.Now,
                    rating = rating != 0 ? rating : 0,

                };
                _context.Add(comment);
                _context.SaveChanges();
                CommentProduct commentProduct = new CommentProduct()
                {
                    CommentID = comment.CommentID,
                    ProductID = productID,

                };
                _context.Add(commentProduct);
                _context.SaveChanges();
                await _context.SaveChangesAsync();

            }
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<ActionResult> Answer(int CommentID, string message)
        {

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            if (!string.IsNullOrEmpty(message))
            {
                Answer answer = new Answer()
                {
                    Message = message,
                    Status = 1,
                    UserID = currentUser.UserName,
                    AnswerTime = DateTime.Now,
                    

                };
                _context.Add(answer);
                _context.SaveChanges();
                CommentAnswer commentAnswer = new CommentAnswer()
                {
                    AnswerID = answer.AnswerID,
                    CommentID = CommentID,

                };
                _context.Add(commentAnswer);
                _context.SaveChanges();
                await _context.SaveChangesAsync();

            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
       






    }
}
