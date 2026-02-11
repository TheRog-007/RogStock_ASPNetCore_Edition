using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RogStock_ASPNetCore.Models_RogStock;

namespace RogStock_ASPNetCore.Controllers
{
    public class StockProductFamiliesController : Controller
    {
        private readonly RogStockDbContext _context;

        public StockProductFamiliesController(RogStockDbContext context)
        {
            _context = context;
        }

        // GET: StockProductFamilies
        public async Task<IActionResult> Index()
        {
            return View(await _context.StockProductFamilies.ToListAsync());
        }

        // GET: StockProductFamilies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockProductFamily = await _context.StockProductFamilies
                .FirstOrDefaultAsync(m => m.StkpId == id);
            if (stockProductFamily == null)
            {
                return NotFound();
            }

            return View(stockProductFamily);
        }

        // GET: StockProductFamilies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StockProductFamilies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StkpId,StkpProductFamily,StkpDesc,Timestamp")] StockProductFamily stockProductFamily)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockProductFamily);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stockProductFamily);
        }

        // GET: StockProductFamilies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockProductFamily = await _context.StockProductFamilies.FindAsync(id);
            if (stockProductFamily == null)
            {
                return NotFound();
            }
            return View(stockProductFamily);
        }

        // POST: StockProductFamilies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StkpId,StkpProductFamily,StkpDesc,Timestamp")] StockProductFamily stockProductFamily)
        {
            if (id != stockProductFamily.StkpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockProductFamily);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockProductFamilyExists(stockProductFamily.StkpId))
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
            return View(stockProductFamily);
        }

        // GET: StockProductFamilies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockProductFamily = await _context.StockProductFamilies
                .FirstOrDefaultAsync(m => m.StkpId == id);
            if (stockProductFamily == null)
            {
                return NotFound();
            }

            return View(stockProductFamily);
        }

        // POST: StockProductFamilies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stockProductFamily = await _context.StockProductFamilies.FindAsync(id);
            if (stockProductFamily != null)
            {
                _context.StockProductFamilies.Remove(stockProductFamily);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockProductFamilyExists(int id)
        {
            return _context.StockProductFamilies.Any(e => e.StkpId == id);
        }
    }
}
