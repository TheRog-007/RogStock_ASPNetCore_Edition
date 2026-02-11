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
    public class StockUomsController : Controller
    {
        private readonly RogStockDbContext _context;

        public StockUomsController(RogStockDbContext context)
        {
            _context = context;
        }

        // GET: StockUoms
        public async Task<IActionResult> Index()
        {
            return View(await _context.StockUoms.ToListAsync());
        }

        // GET: StockUoms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockUom = await _context.StockUoms
                .FirstOrDefaultAsync(m => m.StkuId == id);
            if (stockUom == null)
            {
                return NotFound();
            }

            return View(stockUom);
        }

        // GET: StockUoms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StockUoms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StkuId,StkuDesc,Timestamp")] StockUom stockUom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockUom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stockUom);
        }

        // GET: StockUoms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockUom = await _context.StockUoms.FindAsync(id);
            if (stockUom == null)
            {
                return NotFound();
            }
            return View(stockUom);
        }

        // POST: StockUoms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StkuId,StkuDesc,Timestamp")] StockUom stockUom)
        {
            if (id != stockUom.StkuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockUom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockUomExists(stockUom.StkuId))
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
            return View(stockUom);
        }

        // GET: StockUoms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockUom = await _context.StockUoms
                .FirstOrDefaultAsync(m => m.StkuId == id);
            if (stockUom == null)
            {
                return NotFound();
            }

            return View(stockUom);
        }

        // POST: StockUoms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stockUom = await _context.StockUoms.FindAsync(id);
            if (stockUom != null)
            {
                _context.StockUoms.Remove(stockUom);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockUomExists(int id)
        {
            return _context.StockUoms.Any(e => e.StkuId == id);
        }
    }
}
