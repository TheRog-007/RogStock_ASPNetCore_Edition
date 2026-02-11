using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RogStock_ASPNetCore.Models_RogStock;
using RogStock_ASPNetCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RogStock_ASPNetCore.Controllers
{
    public class StockLotsController : Controller
    {
        private readonly RogStockDbContext _context;

        public StockLotsController(RogStockDbContext context)
        {
            _context = context;
        }

        // GET: StockLots
        public async Task<IActionResult> Index()
        {
            return View(await _context.StockLots.ToListAsync());
        }

        // GET: StockLots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockLot = await _context.StockLots
                .FirstOrDefaultAsync(m => m.LotId == id);
            if (stockLot == null)
            {
                return NotFound();
            }

            return View(stockLot);
        }

        // GET: StockLots/Create
        public IActionResult Create()
        {
            //create new combined view class and pass database connection to it
            clsStockLotsViewModel clsStockLotsCombinedViewModel = new clsStockLotsViewModel(); //added
            //get stock item list
            var varStockItems = _context.StockItems.ToList();
            //convert to select list
            SelectList selStockItems = new SelectList(varStockItems.Where(x => x.StkiLocLot == true).Select(x => x.StkiItemId));
            clsStockLotsCombinedViewModel.lstStockItems = selStockItems;

            //get stock Location list from stockitems where 
            var varStockLocations = _context.StockLocs.ToList();
            //convert to select list
            SelectList selStockLocations = new SelectList(varStockLocations.Select(x => x.LocLocation));
            clsStockLotsCombinedViewModel.lstStockLocations = selStockLocations;

            return View(clsStockLotsCombinedViewModel);
        }

        // POST: StockLots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //        public async Task<IActionResult> Create([Bind("LotId,LotItemId,LotNbr,LotUpdated,LotQty,LotNonNet,LotLocation,Timestamp")] StockLot stockLot)
        public async Task<IActionResult> Create(clsStockLotsViewModel clsStockLotsCombinedViewModel)
        {
            LotTrn mdlLotTRN;

            //remove lists
            ModelState.Remove("lstStockItems");
            ModelState.Remove("lstStockLocations");

            if (ModelState.IsValid)
            {
                _context.Add(clsStockLotsCombinedViewModel.tblStockLot);
                try
                {
                    //DO NOT use async as needs to be serial procesing
                    _context.SaveChanges();
                    //create lot_TRN record for lot creation
                    mdlLotTRN = new LotTrn();
                    mdlLotTRN.LottItemId = clsStockLotsCombinedViewModel.tblStockLot.LotItemId;
                    mdlLotTRN.LottQty = clsStockLotsCombinedViewModel.tblStockLot.LotQty;
                    mdlLotTRN.LottLocation = clsStockLotsCombinedViewModel.tblStockLot.LotLocation;
                    //populated by save
                    mdlLotTRN.LottNbr = clsStockLotsCombinedViewModel.tblStockLot.LotId;
                    mdlLotTRN.LottOperation = "Create";
                    _context.LotTrns.Add(mdlLotTRN);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    ex = ex;
                }
              //  return RedirectToAction(nameof(Index));
            }
            return View(clsStockLotsCombinedViewModel);
        }

        // GET: StockLots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            clsStockLotsViewModel clsStockLotsCombinedViewMode = new clsStockLotsViewModel();
  
            if (id == null)
            {
                return NotFound();
            }

            var stockLot = await _context.StockLots.FindAsync(id);
            if (stockLot == null)
            {
                return NotFound();
            }

            //create new combined view class and pass database connection to it
            clsStockLotsViewModel clsStockLotsCombinedViewModel = new clsStockLotsViewModel(); //added
            //get stock item list
            var varStockItems = _context.StockItems.ToList();
            //convert to select list
            SelectList selStockItems = new SelectList(varStockItems.Where(x => x.StkiLocLot == true).Select(x => x.StkiItemId));
            clsStockLotsCombinedViewModel.lstStockItems = selStockItems;
            
            //get stock Location list from LOCATIONS tanle as every stock active Location has a location
            var varStockLocations = _context.StockLocs.ToList();
            //convert to select list
            SelectList selStockLocations = new SelectList(varStockLocations.Select(x => x.LocLocation));
            clsStockLotsCombinedViewModel.lstStockLocations = selStockLocations;

            clsStockLotsCombinedViewModel.tblStockLot = stockLot;
            return View(clsStockLotsCombinedViewModel);
        }

        // POST: StockLots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, clsStockLotsViewModel clsStockLotsCombinedViewModel)
        {
            LotTrn mdlLotTRN;

            //remove lists
            ModelState.Remove("lstStockItems");
            ModelState.Remove("lstStockLocations");

            if (id != clsStockLotsCombinedViewModel.tblStockLot.LotId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //find lot record
                StockLot mdlStockLot = _context.StockLots.SingleOrDefault(x => x.LotId == id);

                if (mdlStockLot != null)
                {
                    try
                    {
                        //creaate lot_TRN record for lot edit
                        mdlLotTRN = new LotTrn();
                        mdlLotTRN.LottItemId = clsStockLotsCombinedViewModel.tblStockLot.LotItemId;
                        mdlLotTRN.LottQty = clsStockLotsCombinedViewModel.tblStockLot.LotQty;
                        mdlLotTRN.LottLocation = clsStockLotsCombinedViewModel.tblStockLot.LotLocation;
                        mdlLotTRN.LottNbr = clsStockLotsCombinedViewModel.tblStockLot.LotId;
                        mdlLotTRN.LottOperation = "Edit";
                        _context.LotTrns.Add(mdlLotTRN);
                        //update record from vioew
                        mdlStockLot.LotNonNet = clsStockLotsCombinedViewModel.tblStockLot.LotNonNet;
                        mdlStockLot.LotLocation = clsStockLotsCombinedViewModel.tblStockLot.LotLocation;
                        mdlStockLot.LotQty = clsStockLotsCombinedViewModel.tblStockLot.LotQty;
                        _context.Update(mdlStockLot);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!StockLotExists(clsStockLotsCombinedViewModel.tblStockLot.LotId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
             //   return RedirectToAction(nameof(Index));
            }
            return View(clsStockLotsCombinedViewModel);
        }

        // GET: StockLots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockLot = await _context.StockLots
                .FirstOrDefaultAsync(m => m.LotId == id);
            if (stockLot == null)
            {
                return NotFound();
            }

            return View(stockLot);
        }

        // POST: StockLots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, clsStockLotsViewModel clsStockLotsCombinedViewModel)
        {
            LotTrn mdlLotTRN;

            var stockLot = await _context.StockLots.FindAsync(id);
            if (stockLot != null)
            {
                try
                {
                    //creaate lot_TRN record for lot delete
                    mdlLotTRN = new LotTrn();
                    mdlLotTRN.LottItemId = clsStockLotsCombinedViewModel.tblStockLot.LotItemId;
                    mdlLotTRN.LottQty = clsStockLotsCombinedViewModel.tblStockLot.LotQty;
                    mdlLotTRN.LottLocation = clsStockLotsCombinedViewModel.tblStockLot.LotLocation;
                    mdlLotTRN.LottNbr = clsStockLotsCombinedViewModel.tblStockLot.LotId;
                    mdlLotTRN.LottOperation = "Delete";
                    _context.LotTrns.Add(mdlLotTRN);

                    _context.StockLots.Remove(stockLot);
                }
                catch (Exception ex)
                {
                    ex = ex;
                }
    
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool StockLotExists(int id)
        {
            return _context.StockLots.Any(e => e.LotId == id);
        }
    }
}
