using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Mobile_WebApplication.Data;
using Mobile_WebApplication.Models;

namespace Mobile_WebApplication.Controllers
{
    public class ordersController : Controller
    {
        private readonly Mobile_WebApplicationContext _context;

        public ordersController(Mobile_WebApplicationContext context)
        {
            _context = context;
        }


        // GET: orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.orders.ToListAsync());
        }

        // GET: orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: orders/Create
        public async Task<IActionResult> Create(int? id)
        {
            var item = await _context.items.FindAsync(id);
            return View(item);
        }

        // POST: orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int itemId, int quantity)
        {
            orders order = new orders();
            order.itemId = itemId;
            order.quantity = quantity;

            order.userid = Convert.ToInt32(HttpContext.Session.GetString("userid"));
            order.orderdate = DateTime.Today;

            SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\pc\\Documents\\mobileDB.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True");
            string sql;
            sql = "UPDATE items SET itemsquantity  = itemsquantity   - '" + order.quantity + "'  where (id ='" + order.itemId + "' )";
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            comm.ExecuteNonQuery();

        _context.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("myorders");
        }

        public async Task<IActionResult> myorders()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("userid"));
            var orderitems=await _context.orders.FromSqlRaw("select * from orders where userid = '" +id +"'  ").ToListAsync();
            return View(orderitems);

        }
            // GET: orders/Edit/5
            public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);
        }

        // POST: orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,itemId,userid,quantity,orderdate")] orders orders)
        {
            if (id != orders.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ordersExists(orders.Id))
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
            return View(orders);
        }

        // GET: orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orders = await _context.orders.FindAsync(id);
            if (orders != null)
            {
                _context.orders.Remove(orders);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ordersExists(int id)
        {
            return _context.orders.Any(e => e.Id == id);
        }
    }
}
