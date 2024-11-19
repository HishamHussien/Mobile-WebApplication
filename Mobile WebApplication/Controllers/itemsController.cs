using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mobile_WebApplication.Data;
using Mobile_WebApplication.Models;

namespace Mobile_WebApplication.Controllers
{
    public class itemsController : Controller
    {
        private readonly Mobile_WebApplicationContext _context;

        public itemsController(Mobile_WebApplicationContext context)
        {
            _context = context;
        }

        // GET: items
        public async Task<IActionResult> Index()
        {
            return View(await _context.items.ToListAsync());
        }

        public async Task<IActionResult> catalogue()
        {
            return View(await _context.items.ToListAsync());
        }


        // GET: items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = await _context.items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (items == null)
            {
                return NotFound();
            }

            return View(items);
        }

        // GET: items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file,[Bind("Id,itemname,info,itemsquantity,price,cataid,company")] items items)
        {
            if (file != null)
            {
                string filename = file.FileName;
                //  string  ext = Path.GetExtension(file.FileName);
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"));
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                { await file.CopyToAsync(filestream); }

                items.imgfile = filename;
                   }

                _context.Add(items);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
        }

        // GET: items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = await _context.items.FindAsync(id);
            if (items == null)
            {
                return NotFound();
            }
            return View(items);
        }

        // POST: items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile file, int id, [Bind("Id,itemname,info,itemsquantity,price,cataid,company")] items items)
        {
            if (file != null)
            {
                string filename = file.FileName;
                //  string  ext = Path.GetExtension(file.FileName);
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"));
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                { await file.CopyToAsync(filestream); }

                items.imgfile = filename;
            }


            _context.Update(items);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = await _context.items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (items == null)
            {
                return NotFound();
            }

            return View(items);
        }

        // POST: items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var items = await _context.items.FindAsync(id);
            if (items != null)
            {
                _context.items.Remove(items);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool itemsExists(int id)
        {
            return _context.items.Any(e => e.Id == id);
        }
    }
}
