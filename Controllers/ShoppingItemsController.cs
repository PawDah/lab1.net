using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab1.net.Data;
using lab1.net.Models;

namespace lab1.net.Controllers
{
    public class ShoppingItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ShoppingItems
        public async Task<IActionResult> Index()
        {
            List<ShoppingItem> objUserList = _context.ShoppingItem.ToList();
            return View(objUserList);
        }
        public IActionResult Create()
        {
            return View();
        }
        // GET: ShoppingItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingItem = await _context.ShoppingItem
                .Include(s => s.ShoppingList)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingItem == null)
            {
                return NotFound();
            }

            return View(shoppingItem);
        }

  

        // POST: ShoppingItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create([Bind("Id,Description")] ShoppingItem shoppingItem)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage); // Lub użyj loggera zamiast Console.WriteLine
                }
            }
            if (ModelState.IsValid)
            {
                _context.ShoppingItem.Add(shoppingItem);
                _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: ShoppingItems/Edit/5
        public IActionResult Edit(int? id)
        {
            ShoppingItem shoppingItem = _context.ShoppingItem.FirstOrDefault(c => c.Id == id);
            return View(shoppingItem);
        }

        // POST: ShoppingItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ShoppingItem shoppingItem)
        {
            if (ModelState.IsValid)
            {
                _context.ShoppingItem.Update(shoppingItem);
                _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult CheckItem(int id)
        {
            var obj = _context.ShoppingItem.FirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                ModelState.AddModelError("Name", "A category with the same name already exists.");
            }
            if (ModelState.IsValid)
            {
                obj.IsChecked = true;
                _context.ShoppingItem.Update(obj);
                _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

  


        // POST: ShoppingItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var obj = _context.ShoppingItem.FirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                ModelState.AddModelError("Name", "A category with the same name already exists.");
            }
            if (ModelState.IsValid)
            {
                _context.ShoppingItem.Remove(obj);
                _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        private bool ShoppingItemExists(int id)
        {
            return _context.ShoppingItem.Any(e => e.Id == id);
        }
    }
}
