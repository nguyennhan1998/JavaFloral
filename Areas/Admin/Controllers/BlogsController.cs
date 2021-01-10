using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JavaFloral.Data;
using JavaFloral.Models;
using Microsoft.AspNetCore.Hosting;
using JavaFloral.ViewModels;
using System.IO;

namespace JavaFloral.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;


        public BlogsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Admin/Blogs
        public async Task<IActionResult> Index()
        {
            return View(_context.Blogs.ToListAsync());
        }

        // GET: Admin/Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.BlogID == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }
        private string UploadedFile(BlogViewModel model)
        {
            string uniqueFileName = null;

            if (model.BlogImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.BlogImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.BlogImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        // GET: Admin/Blogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);
                Blog blog = new Blog
                {
                  
                    Title = model.Title,
                    BlogImage = uniqueFileName,
                    Description = model.Description,
                    Content = model.Content,
                    ViewCount = model.ViewCount,
                    Status = model.Status,
                };
                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Admin/Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FindAsync(id);
            BlogViewModel model = new BlogViewModel
            {
                BlogID = blog.BlogID,
                Title = blog.Title,
                Description = blog.Description,
                Content = blog.Content,
                ViewCount = blog.ViewCount,
                Status = blog.Status,

            };

            if (blog == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Admin/Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlogViewModel model)
        {
            if (id != model.BlogID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var blogs = await _context.Blogs.FindAsync(id);
                _context.Blogs.Remove(blogs);
                await _context.SaveChangesAsync();
                try
                {
                    string uniqueFileName = UploadedFile(model);
                    Console.WriteLine(uniqueFileName);
                    Blog blog = new Blog
                    {
                        Title = model.Title,
                        BlogImage = uniqueFileName,
                        Description = model.Description,
                        Content = model.Content,
                        ViewCount = model.ViewCount,
                        Status = model.Status,
                    };
                    _context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(model.BlogID))
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
            return View();
        }

        // GET: Admin/Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.BlogID == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Admin/Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.BlogID == id);
        }
    }
}
