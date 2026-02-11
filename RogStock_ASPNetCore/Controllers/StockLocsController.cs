using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RogStock_ASPNetCore.Models_RogStock;
using RogStock_ASPNetCore.ViewModels;

namespace RogStock_ASPNetCore.Controllers
{
    public class StockLocsController : Controller
    {
        private readonly RogStockDbContext _context;
  
        public StockLocsController(RogStockDbContext context)
        {
            _context = context;
        }

        // GET: StockLocs
        public async Task<IActionResult> Index()
        {
            return View(await _context.StockLocs.ToListAsync());
        }

        // GET: StockLocs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockLoc = await _context.StockLocs
                .FirstOrDefaultAsync(m => m.LocId == id);
            if (stockLoc == null)
            {
                return NotFound();
            }

            return View(stockLoc);
        }

        // GET: StockLocs/Create
        public IActionResult Create()
        {
            //create new combined view class and pass database connection to it
            clsStockLocsViewModel clsStockLocsCombinedViewModel = new clsStockLocsViewModel(); //added
            //get stock item list
            var varStockItems = _context.StockItems.ToList();
            //convert to select list
            SelectList selStockItems = new SelectList(varStockItems.Where(x => x.StkiLocLot == true ).Select(x => x.StkiItemId));
            clsStockLocsCombinedViewModel.lstStockItems = selStockItems;

            return View(clsStockLocsCombinedViewModel);
        }

        // POST: StockLocs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("LocId,LocItemId,LocLocation,LocUpdated,LocQty,LocNonNet,LocDescription,Timestamp")] StockLoc stockLoc)
        public async Task<IActionResult> Create(clsStockLocsViewModel clsStockLocsCombinedViewModel)
        {
            LocTrn mdlLocTRN;

            //remove list
            ModelState.Remove("lstStockItems");
            ModelState.Remove("lstStockLocations");

            if (ModelState.IsValid)
            {
             try
                { 
                    //create loc_TRN record for location deletion
                    mdlLocTRN = new LocTrn();
                    mdlLocTRN.LoctItemId = clsStockLocsCombinedViewModel.tblStockLoc.LocItemId;
                    mdlLocTRN.LoctQty = clsStockLocsCombinedViewModel.tblStockLoc.LocQty;
                    mdlLocTRN.LoctLocation = clsStockLocsCombinedViewModel.tblStockLoc.LocLocation;
                    mdlLocTRN.LoctOperation = "Create";
                    _context.LocTrns.Add(mdlLocTRN);
                    _context.Add(clsStockLocsCombinedViewModel.tblStockLoc);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                    catch (Exception ex)
                    {
                    ex = ex;
                }
        }
            return View(clsStockLocsCombinedViewModel);
        }

        // GET: StockLocs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            clsStockLocsViewModel clsStockLocsCombinedViewModel = new clsStockLocsViewModel();

            if (id == null)
            {
                return NotFound();
            }

            var stockLoc = await _context.StockLocs.FindAsync(id);

            if (stockLoc == null)
            {
                return NotFound();
            }

            //store location data in viewmodel
            clsStockLocsCombinedViewModel.tblStockLoc = stockLoc;

            //get stock Location list from stockitems where 
            var varStockLocations = _context.StockLocs.ToList();
            //convert to select list
            SelectList selStockLocations = new SelectList(varStockLocations.Select(x => x.LocLocation));
            clsStockLocsCombinedViewModel.lstStockLocations = selStockLocations;

            return View(clsStockLocsCombinedViewModel);
        }

        // POST: StockLocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("LocId,LocItemId,LocLocation,LocUpdated,LocQty,LocNonNet,LocDescription,Timestamp")] StockLoc stockLoc)
        public async Task<IActionResult> Edit(int id, clsStockLocsViewModel clsStockLocsCombinedViewModel)
        {
            LocTrn mdlLocTRN;

            if (id != clsStockLocsCombinedViewModel.tblStockLoc.LocId)
            {
                return NotFound();
            }

            //remove list
            ModelState.Remove("lstStockItems");
            ModelState.Remove("lstStockLocations");

            if (ModelState.IsValid)
            {
                //find item id
                StockLoc mdlStockLoc = _context.StockLocs.SingleOrDefault(x => x.LocId == id);

                if (mdlStockLoc != null)
                {
                    try
                    {
                        //create loc_TRN record for location deletion
                        mdlLocTRN = new LocTrn();
                        mdlLocTRN.LoctQty = clsStockLocsCombinedViewModel.tblStockLoc.LocQty;
                        mdlLocTRN.LoctLocation = clsStockLocsCombinedViewModel.tblStockLoc.LocLocation;
                        mdlLocTRN.LoctItemId = clsStockLocsCombinedViewModel.tblStockLoc.LocItemId;

                        //check if location edited - disabled 21/01/2026 put this code in stock location rename view
                        //var mdlStockLocs = _context.StockLocs.Select(x => x.LocItemId == clsStockLocsCombinedViewModel.tblStockLoc.LocItemId &&
                        //                                            x.LocLocation != clsStockLocsCombinedViewModel.tblStockLoc.LocLocation);
                        //if (mdlStockLocs != null)
                        //{ 
                        //    mdlLocTRN.LoctOldLocation = mdlStockLocs.LocLocation;
                        //}

                        mdlLocTRN.LoctOperation = "Edit";
                        _context.LocTrns.Add(mdlLocTRN);

                        //get changes from view
                        mdlStockLoc.LocLocation = clsStockLocsCombinedViewModel.tblStockLoc.LocLocation;
                        mdlStockLoc.LocQty = clsStockLocsCombinedViewModel.tblStockLoc.LocQty;
                        mdlStockLoc.LocDescription = clsStockLocsCombinedViewModel.tblStockLoc.LocDescription;
                        _context.Update(mdlStockLoc);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!StockLocExists(clsStockLocsCombinedViewModel.tblStockLoc.LocId))
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
            }

            return View(clsStockLocsCombinedViewModel);
        }

        // GET: StockLocs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockLoc = await _context.StockLocs
                .FirstOrDefaultAsync(m => m.LocId == id);
            if (stockLoc == null)
            {
                return NotFound();
            }

            return View(stockLoc);
        }

        // POST: StockLocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, clsStockLocsViewModel clsStockLocsCombinedViewModel)
        {
            LocTrn mdlLocTRN;
            var stockLoc = await _context.StockLocs.FindAsync(id);

            if (stockLoc != null)
            {
                try
                {
                    //create loc_TRN record for location deletion
                    mdlLocTRN = new LocTrn();
                    mdlLocTRN.LoctQty = clsStockLocsCombinedViewModel.tblStockLoc.LocQty;
                    mdlLocTRN.LoctLocation = clsStockLocsCombinedViewModel.tblStockLoc.LocLocation;
                    mdlLocTRN.LoctOperation = "Delete";
                    _context.LocTrns.Add(mdlLocTRN);
                    _context.StockLocs.Remove(stockLoc);

                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    ex = ex;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool StockLocExists(int id)
        {
            return _context.StockLocs.Any(e => e.LocId == id);
        }
    }
}
