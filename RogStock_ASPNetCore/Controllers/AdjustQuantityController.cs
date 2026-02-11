using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using RogStock_ASPNetCore.Models_RogStock;
using RogStock_ASPNetCore.OtherClasses;
using RogStock_ASPNetCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json; //added
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RogStock_ASPNetCore.Controllers
{
    public class AdjustQuantityController : Controller
    {
        private readonly RogStockDbContext _context;

        public AdjustQuantityController(RogStockDbContext context)
        {
            _context = context;
        }

        // GET: AdjustQuantity
        public async Task<IActionResult> Index()
        {
            return View(await _context.StockLocs.ToListAsync());
        }

        // GET: AdjustQuantity/Details/5
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

        // GET: AdjustQuantity/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdjustQuantity/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocId,LocItemId,LocLocation,LocUpdated,LocQty,LocNonNet,LocDescription,Timestamp")] StockLoc stockLoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockLoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stockLoc);
        }

        // GET: AdjustQuantity/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            /*
              Created 28/10/2026 By Roger Williams

              technically only edit exists as only one form!

              creates lstStockItems 

            */

            clsAdjustQuantityViewModel mdlAdjustQuantityViewModel = new clsAdjustQuantityViewModel();
            clsAdjustQuantityViewModel.clsLocationsForEdit clsAddLocationData;

            //get stock item list
            var varStockItems = _context.StockItems.ToList();
            SelectList selStockItems = new SelectList(varStockItems.Select(x => x.StkiItemId));
            mdlAdjustQuantityViewModel.lstStockItems = selStockItems;

            return View(mdlAdjustQuantityViewModel);
        }

        // POST: AdjustQuantity/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, clsAdjustQuantityViewModel mdlAdjustQuantityViewModel)
        {

            //CORRECT -->
            //if (id != stockLoc.LocId)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    //    _context.Update(stockLoc);
                    //    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    //CORRECT --->
                    //if (!StockLocExists(mdlAdjustQuantityViewModel.strItemID)
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mdlAdjustQuantityViewModel);
        }

        // GET: AdjustQuantity/Delete/5
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

        // POST: AdjustQuantity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stockLoc = await _context.StockLocs.FindAsync(id);
            if (stockLoc != null)
            {
                _context.StockLocs.Remove(stockLoc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockLocExists(int id)
        {
            return _context.StockLocs.Any(e => e.LocId == id);
        }


        //***************custom subs/funcs*******


        public IActionResult StockItemSelected(string? strItemID)
        {
            /*
                 Created 29/01/2026 By Roger Williams

                 IF view combobox returns a value to this then populate loc/lot lists with records!


            */

            clsAdjustQuantityViewModel mdlAdjustQuantityViewModel = new clsAdjustQuantityViewModel();
            clsAdjustQuantityViewModel.clsLocationsForEdit clsAddLocationData;
            clsAdjustQuantityViewModel.clsLotsForEdit clsAddLotData;

            //find stock item
            var varStockItemsFind = _context.StockItems.Where(x => x.StkiItemId == strItemID);
            if (varStockItemsFind == null)
            {
                return NotFound();
            }

            //set item ID in viewmodel
            mdlAdjustQuantityViewModel.strItemID = strItemID;
            //populate loc/lot tables based on itemID
            //get locations for item id               
            var varLocations = _context.StockLocs.Where(x => x.LocItemId == strItemID).Select(x => x).ToArray();

            //itemIDs can have NO location so make sure only progressing if passed one does!
            if (varLocations.Length != 0)
            {
                clsAddLocationData = new clsAdjustQuantityViewModel.clsLocationsForEdit();

                foreach (StockLoc location in varLocations)
                {
                    //populate class for adding to list
                    clsAddLocationData = new clsAdjustQuantityViewModel.clsLocationsForEdit();
                    clsAddLocationData.LocQty = location.LocQty;
                    clsAddLocationData.LocOriginalQty = location.LocQty;
                    clsAddLocationData.Location = location.LocLocation;
                    clsAddLocationData.LocationDesc = location.LocDescription;
                    clsAddLocationData.LocId = location.LocId; //use for saving later makes it quicker!
                                                               //add to view list
                    mdlAdjustQuantityViewModel.lstLocationsForEdit.Add(clsAddLocationData);
                }

                var varLots = _context.StockLots.Where(x => x.LotItemId == strItemID).Select(x => x).ToArray();
                clsAddLotData = new clsAdjustQuantityViewModel.clsLotsForEdit();

                if (varLots.Length != 0)
                {
                    foreach (StockLot Lot in varLots)
                    {
                        //populate class for adding to list
                        clsAddLotData = new clsAdjustQuantityViewModel.clsLotsForEdit();
                        clsAddLotData.LotQty = Lot.LotQty;
                        clsAddLotData.LotOriginalQty = Lot.LotQty;
                        clsAddLotData.LotLocation = Lot.LotLocation;
                        clsAddLotData.LotId = Lot.LotId; //use for saving later makes it quicker!
                                                         //add to view list
                        mdlAdjustQuantityViewModel.lstLotsForEdit.Add(clsAddLotData);
                    }
                }
            }

            //get stock item list
            var varStockItems = _context.StockItems.ToList();
            SelectList selStockItems = new SelectList(varStockItems.Select(x => x.StkiItemId));
            mdlAdjustQuantityViewModel.lstStockItems = selStockItems;

            return View("viewAdjustQuantity", mdlAdjustQuantityViewModel);
        }


        //viewAdjustQuantity
        // GET: AdjustQuantity/Edit/5
        public async Task<IActionResult> ShowAdjustStockQuantity()
        {
            /*
              Created 28/10/2026 By Roger Williams

              technically only edit exists as only one form!

              creates lstStockItems

            */
            clsAdjustQuantityViewModel mdlAdjustQuantityViewModel = new clsAdjustQuantityViewModel();
            clsAdjustQuantityViewModel.clsLocationsForEdit clsAddLocationData;

            //get stock item list
            var varStockItems = _context.StockItems.ToList();
            SelectList selStockItems = new SelectList(varStockItems.Select(x => x.StkiItemId));
            mdlAdjustQuantityViewModel.lstStockItems = selStockItems;

            return View("viewAdjustQuantity", mdlAdjustQuantityViewModel);
        }

        // POST: AdjustQuantity/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditStockQuantity([FromBody] string strJSON)
        //{

        //    //CORRECT -->
        //    //if (id != stockLoc.LocId)
        //    //{
        //    //    return NotFound();
        //    //}
        //    ModelState.Remove("lstStockItems");
        //    ModelState.Remove("lstLocationsForEdit");

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            //    _context.Update(stockLoc);
        //            //    await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {

        //            //CORRECT --->
        //            //if (!StockLocExists(mdlAdjustQuantityViewModel.strItemID)
        //            //{
        //            //    return NotFound();
        //            //}
        //            //else
        //            //{
        //            //    throw;
        //            //}
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View("viewAdjustQuantity");
        //}

        //added [frombody] so accepts JSON strings
        public async Task<IActionResult> ChangeStockQuantity([FromBody] string strJSON)
        {
            clsAdjustQuantityViewModel mdlAdjustQuantityViewModel = new clsAdjustQuantityViewModel();
            decimal decQty = 0;
            decimal decOrgQty = 0;
            int intID = 0;


            // Parse JSON into JToken
            JToken token = JToken.Parse(strJSON);

            // Cast the "results" to JArray
            JArray aryLocations = (JArray)token["Locations"];
            JArray aryLots = (JArray)token["Lots"];

            if (aryLocations != null)
            {
            
                //reads properties e.g. itemID into array of clsAdjustQuantity
                foreach (var varLoc in aryLocations)
                {
                    decQty = Convert.ToDecimal(varLoc["Qty"]);
                    decOrgQty = Convert.ToDecimal(varLoc["OrgQty"]);
                    intID = Convert.ToInt32(varLoc["LocID"]);

                    //if quantities differ save
                    if (decQty != decOrgQty)
                    {
                        //save changes
                        StockLoc mdlLocation = _context.StockLocs.FirstOrDefault(x => x.LocId == intID);
                        mdlLocation.LocQty = decQty;

                        //Save to database Note: EF uses a transaction as standard
                        try
                        {
                            await _context.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {
                            ex = ex;
                        }
                    }
                }
            }

            if (aryLots != null)
            {
                //reads properties e.g. itemID into array of clsAdjustQuantity
                foreach (var varLot in aryLots)
                {
                    decQty = Convert.ToDecimal(varLot["Qty"]);
                    decOrgQty = Convert.ToDecimal(varLot["OrgQty"]);
                    intID = Convert.ToInt32(varLot["LotID"]);

                    //if quantities differ save
                    if (decQty != decOrgQty)
                    {
                        //save changes
                        StockLot mdlLot = _context.StockLots.FirstOrDefault(x => x.LotId == intID);
                        mdlLot.LotQty = decQty;

                        //Save to database Note: EF uses a transaction as standard
                        try
                        {
                            await _context.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {
                            ex = ex;
                        }
                    }
                }
            }

            //get stock item list
            var varStockItems = _context.StockItems.ToList();
            SelectList selStockItems = new SelectList(varStockItems.Select(x => x.StkiItemId));
            mdlAdjustQuantityViewModel.lstStockItems = selStockItems;

            return View("viewAdjustQuantity",mdlAdjustQuantityViewModel);
        }


    }
}
