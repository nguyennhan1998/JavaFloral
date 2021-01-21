using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JavaFloral.Data;
using JavaFloral.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JavaFloral.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "ADMIN")]
    public class HomeController : Controller
    {
    
        private readonly ApplicationDbContext _context;
    
        // GET: HomeController
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        

        }

        public  ActionResult<DashboardViewModel> Index()
        {
            var dbm = new DashboardViewModel();
            var totalOrder = _context.Orders.Count();
            var totalOrderInProcess = _context.Orders.Where(o=>o.Status == 3 ).Count();
            var totalOrderInSuccess = _context.Orders.Where(o=>o.Status == 1 ).Count();
            var totalOrderInCancel = _context.Orders.Where(o=>o.Status == 2 ).Count();
            var totalOrder1 = _context.Orders.ToList();
            var totalOrderInCurrentDay = _context.Orders.Where(o => o.CreateAt == DateTime.Now).Count();
/*            var totalOrderInCurrentMonth = _context.Orders.Where(o => o.CreateAt.ToString().Split("-")[1] == DateTime.Now.ToString("MM")).Count();
*/            decimal totalRevenue = 0;
            foreach (var item in totalOrder1)
            {
                totalRevenue += item.GrandTotal;
            }
            dbm.totalRevenue = totalRevenue;
            dbm.orderCount = totalOrder;
            dbm.orderInProcess = totalOrderInProcess;
            dbm.orderInComplete = totalOrderInSuccess;
            dbm.orderInCancel = totalOrderInCancel;
            dbm.orderCountByDay = totalOrderInCurrentDay;

            Console.WriteLine(totalOrder);

            return View(dbm);
        }
       

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
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

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
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

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
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
    }
}
