using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JavaFloral.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JavaFloral.Controllers
{
    public class HomeClientController : Controller
    {
        private readonly ApplicationDbContext _context;
        // GET: HomeClientController
        public HomeClientController(ApplicationDbContext context)
        {
            _context = context;
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
        public ActionResult Product()
        {
            var product = _context.Products.ToList();
            return View(product);
        }
    }
}
