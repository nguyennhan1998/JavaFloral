using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JavaFloral.Data;
using JavaFloral.Models;
using JavaFloral.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JavaFloral.Controllers
{
    public class HomeClientController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        // GET: HomeClientController
        public HomeClientController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public ActionResult Index()
        {

            return View();
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
        public ActionResult Product(int id)
        {
            Console.WriteLine(id);
            var pvm = new ProductListViewModel();
            pvm.Categories = _context.Categories.ToList();

            if (id == 0)
            {
                pvm.Products = _context.Products.ToList();
            }
            else
            {
                pvm.Products = _context.Products.Where(c => c.CategoryID == id).ToList();
            }

            return View(pvm);
        }
        public ActionResult Blog()
        {
            return View();
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
        public IActionResult UpdateCart(int productid,int quantity)
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
            Console.WriteLine(session.GetString(CARTKEY));
        }
        public IActionResult CartPage()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            session.GetString(CARTKEY);
            return View(GetCartItems());
        }
        public IActionResult AddToCart(int id) { 
   
     
            var product = _context.Products.Where(p => p.ProductID == id).FirstOrDefault();
            if (product == null)
                return NotFound("Không có sản phẩm");

            // Xử lý đưa vào Cart ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.ProductID == id );
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity++;
            }
            else
            {
                //  Thêm mới
                cart.Add(new Cart() { quantity = 1, product = product });
            }

            // Lưu cart vào Session
            SaveCartSession(cart);
            // Chuyển đến trang hiện thị Cart

            return Json(new { status = "true"});

        } 
        public ActionResult CheckOut()
        {
           
            return View();
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
            var product = _context.Products.Where(c => c.ProductID == id).FirstOrDefault();
            return View(product);
        }
        public ActionResult WishList()
        {

            return View();
        }



    }
}
