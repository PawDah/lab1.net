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
    public class ShoppingListsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ShoppingLists
        public async Task<IActionResult> Index()
        {
            var shoppingLists = await _context.ShoppingList
        .Include(s => s.ShoppingItems)
        .ToListAsync(); // Upewnij się, że używasz await tutaj
            var allItems = await _context.ShoppingItem.ToListAsync();
            ViewBag.AllItems = allItems;
            return View(shoppingLists);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Planned")] ShoppingList shoppingList)
        {

            if(shoppingList.Planned.Date>= DateTime.Now.Date)
            {
                if (ModelState.IsValid)
                {
              
                    _context.Add(shoppingList);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(shoppingList);
            }
          
            return View(shoppingList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(int shoppingListId, int selectedItemId)
        {
            if (selectedItemId > 0)
            {
                var shoppingList = await _context.ShoppingList
                    .Include(s => s.ShoppingItems)
                    .FirstOrDefaultAsync(s => s.Id == shoppingListId);

                if (shoppingList != null)
                {
                    var shoppingItem = await _context.ShoppingItem.FirstOrDefaultAsync(i => i.Id == selectedItemId);
                    if (shoppingItem != null && !shoppingList.ShoppingItems.Contains(shoppingItem))
                    {
                        shoppingList.ShoppingItems.Add(shoppingItem);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> CheckItem(int id)
        {
            var shoppingItem = await _context.ShoppingItem.FindAsync(id);
            if (shoppingItem == null)
            {
                return NotFound();
            }

            shoppingItem.IsChecked = !shoppingItem.IsChecked;
            _context.Update(shoppingItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "ShoppingLists");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shoppingList = await _context.ShoppingList
                .Include(s => s.ShoppingItems)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (shoppingList != null)
            {
                // Usuń wszystkie produkty powiązane z listą
                _context.ShoppingItem.RemoveRange(shoppingList.ShoppingItems);

                // Usuń listę zakupów
                _context.ShoppingList.Remove(shoppingList);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteItem(int id, int shoppingListId)
        {
            var shoppingList = await _context.ShoppingList
                .Include(s => s.ShoppingItems)
                .FirstOrDefaultAsync(s => s.Id == shoppingListId);

            if (shoppingList == null)
            {
                return NotFound();
            }

            var shoppingItem = shoppingList.ShoppingItems.FirstOrDefault(i => i.Id == id);
            if (shoppingItem == null)
            {
                return NotFound();
            }

            shoppingList.ShoppingItems.Remove(shoppingItem);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


    }
}
