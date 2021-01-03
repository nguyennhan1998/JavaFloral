using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JavaFloral.Data;
using JavaFloral.Models;
using System.IO;
using JavaFloral.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace JavaFloral.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products.Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        private string UploadedFile(ProductViewModel model)
        {
            string uniqueFileName = null;

            if (model.ProductPicture != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProductPicture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProductPicture.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "Categoryid", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);
                Product product = new Product
                {
                    Name = model.Name,
                    ProductImage = uniqueFileName,
                    Description = model.Description,
                    Color = model.Color,
                    Qty = model.Qty,
                    Price = model.Price,
                    Smell = model.Smell,
                    CategoryID = model.CategoryID,
                    Created_at = model.Created_at,
                    Updated_at = model.Updated_at,
                };
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "Categoryid", "Name", model.CategoryID);
            return View();
        }


        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            ProductViewModel model = new ProductViewModel
            {
                ProductID = product.ProductID,
                Name = product.Name,
                Description = product.Description,
                Color = product.Color,
                Qty = product.Qty,
                Price = product.Price,
                Smell = product.Smell,
                CategoryID = product.CategoryID,
                Created_at = product.Created_at,
                Updated_at = product.Updated_at,

            };

            if (product == null)
            {
                return NotFound();
            }

            ViewData["CategoryID"] = new SelectList(_context.Categories, "Categoryid", "Name", product.CategoryID);
            return View(model);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductViewModel model)
        {
            Console.WriteLine(id);
            if (id != model.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var products = await _context.Products.FindAsync(id);
                _context.Products.Remove(products);
                await _context.SaveChangesAsync();

                try
                {
                    string uniqueFileName = UploadedFile(model);
                    Console.WriteLine(uniqueFileName);
                    Product product = new Product
                    {
                        Name = model.Name,
                        ProductImage = uniqueFileName,
                        Description = model.Description,
                        Color = model.Color,
                        Qty = model.Qty,
                        Price = model.Price,
                        Smell = model.Smell,
                        CategoryID = model.CategoryID,
                        Created_at = model.Created_at,
                        Updated_at = model.Updated_at,
                    };
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(model.ProductID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "Categoryid", "Name", model.CategoryID);
            return View();
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
    }
}
