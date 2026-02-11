using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Mono.TextTemplating;
using RogStock_ASPNetCore.Models_RogStock;
using RogStock_ASPNetCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RogStock_ASPNetCore.Controllers
{
    public class StockItemsController : Controller
    {
        private readonly RogStockDbContext _context;

        public StockItemsController(RogStockDbContext context)
        {
            _context = context;
        }


        //***custom funcs/procs
        private string GetFileType(string strPath)
        {
            /*
              Created 14/01/2026 By Roger Williams

              returns file type for storage in stockitemmedia table

              simply extracts the file extension from the passed path and converts to uppercase

              VARS


              strPath - filename to process

            */

            if (string.IsNullOrEmpty(strPath))
            {
                return String.Empty;
            }

            return Path.GetExtension(strPath).ToUpper();
        }

        //***end custom funcs/procs


        // GET: StockItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.StockItems.ToListAsync());
        }

        // GET: StockItems/Details/5
        //        public async Task<IActionResult> Details(int? id)
        public async Task<IActionResult> Details(int? id)
        {
            clsStockItemViewModel clsstockItemCombinedViewModel = new clsStockItemViewModel();

            if (id == null)
            {
                return NotFound();
            }

            var stockItem = await _context.StockItems
                .FirstOrDefaultAsync(m => m.StkiId == id);
            if (stockItem == null)
            {
                return NotFound();
            }

            //find description and media records
            StockDescription? mdlStockItemDescription = _context.StockDescriptions.SingleOrDefault(x => x.StkdItemId == stockItem.StkiItemId);
            StockMedium? mdlStockItemMedia = _context.StockMedia.SingleOrDefault(x => x.StkmItemId == stockItem.StkiItemId);

            if (mdlStockItemDescription != null)
            {
                clsstockItemCombinedViewModel.tblStockDescription = mdlStockItemDescription;
            }

            if (mdlStockItemMedia != null)
            {
                clsstockItemCombinedViewModel.tblStockMedia = mdlStockItemMedia;
            }

            clsstockItemCombinedViewModel.tblStockItem = stockItem;

            return View(clsstockItemCombinedViewModel);
        }

        // GET: StockItems/Create
        public IActionResult Create()
        {
            //create new combined view class and pass database connection to it
            clsStockItemViewModel clsstockItemCombinedViewModel = new clsStockItemViewModel(); 
            //populate lists
            var varProductFamilies = _context.StockProductFamilies.ToList();
            var varUOM = _context.StockUoms.ToList();
            //specify WHICH field / fields to USE in the view
            SelectList selProductFamilies = new SelectList(varProductFamilies.Select(x => x.StkpProductFamily));
            SelectList selUOM = new SelectList(varUOM.Select(x => x.StkuDesc), "StkuDesc");
            clsstockItemCombinedViewModel.lstStockProductFamilies = selProductFamilies;
            clsstockItemCombinedViewModel.lstStockUoms = selUOM;

            return View(clsstockItemCombinedViewModel);
        }

        // POST: StockItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(clsStockItemViewModel clsstockItemCombinedViewModel)
        {
            /*

               Writes to:

               StockItems
               StockDescriptions
               StockMedium (supposed to be Stock_Media but VS renamed it!)

            */
          
            ModelState.Remove("tblStockMedia.StkmPath"); //remove when media implemented
            
            //remove fields populated manually later on 
            ModelState.Remove("tblStockMedia.StkmItemId");
            ModelState.Remove("tblStockDescription.StkdItemId");

            //remove lists
            ModelState.Remove("lstStockUoms");
            ModelState.Remove("lstStockProductFamilies");
            

            if (clsstockItemCombinedViewModel.tblStockItem.StkiItemId != String.Empty)
            {
                //if stockmedia.StkmPath is NOT null update itemid
                if (clsstockItemCombinedViewModel.tblStockMedia.StkmPath != null)
                {
                    //populating both required fields should set modelstate.isvalid to TRUE
                    clsstockItemCombinedViewModel.tblStockMedia.StkmItemId = clsstockItemCombinedViewModel.tblStockItem.StkiItemId;
                }
                else
                {
                    //remove as unused so modelstate.isvalid becomes true
                //    ModelState.Remove("tblStockMedia.StkmPath");  //enable when implemented
                }

                //update stockdescription
                clsstockItemCombinedViewModel.tblStockDescription.StkdItemId = clsstockItemCombinedViewModel.tblStockItem.StkiItemId;
            }

            //with 
            if (ModelState.IsValid)
            {
                try 
                { 
                    _context.Add(clsstockItemCombinedViewModel.tblStockItem);
                    _context.Add(clsstockItemCombinedViewModel.tblStockDescription);
              //  _context.Add(stockItemViewModel.tblStockMedia);  //enable when implemented

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                    catch (Exception ex)
                    {
                    ex = ex;
                }
            }
          
            return View(clsstockItemCombinedViewModel);
        }

        // GET: StockItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            clsStockItemViewModel clsstockItemCombinedViewModel = new clsStockItemViewModel();

            if (id == null)
            {
                return NotFound();
            }

            var stockItem = await _context.StockItems.FindAsync(id);

            if (stockItem == null)
            {
                return NotFound();
            }

            //find item id
            StockItem? mdlStockItem = _context.StockItems.SingleOrDefault(x => x.StkiId == id);

            if (mdlStockItem != null)
            {
                //populate lists
                var varProductFamilies = _context.StockProductFamilies.ToList();
                var varUOM = _context.StockUoms.ToList();
                //specify WHICH field / fields to USE in the view
                SelectList selProductFamilies = new SelectList(varProductFamilies.Select(x => x.StkpProductFamily));
                SelectList selUOM = new SelectList(varUOM.Select(x => x.StkuDesc), "StkuDesc");
                clsstockItemCombinedViewModel.lstStockProductFamilies = selProductFamilies;
                clsstockItemCombinedViewModel.lstStockUoms = selUOM;

                //find description and media records
                StockDescription? mdlStockItemDescription = _context.StockDescriptions.SingleOrDefault(x => x.StkdItemId == mdlStockItem.StkiItemId);
                StockMedium? mdlStockItemMedia = null; // = _context.StockMedia.Select(x => x.StkmItemId == mdlStockItem.StkiItemId);

                if (mdlStockItemDescription != null)
                {
                    clsstockItemCombinedViewModel.tblStockDescription = mdlStockItemDescription;
                }

                if (mdlStockItemMedia != null)
                {
                    clsstockItemCombinedViewModel.tblStockMedia = mdlStockItemMedia;
                }

                clsstockItemCombinedViewModel.tblStockItem = mdlStockItem;

                return View(clsstockItemCombinedViewModel);
            }
            else
            {
                return NotFound();
            }
           // return View(stockItem);
        }

        // POST: StockItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("StkiId,StkiItemId,StkiProductFamily,StkiLocLot,StkiUom,StkiPrice,Timestamp")] StockItem stockItem)
        public async Task<IActionResult> Edit(int id, clsStockItemViewModel clsstockItemCombinedViewModel)
        {
            /*

               Writes to:

               StockItems
               StockDescriptions
               StockMedium (supposed to be Stock_Media but VS renamed it!)
               StockLot if loc/lot tracking changed to OFF - write lot TRN to Lot_TRN and delete StockLot record
             

            */

            LotTrn mdlLotTRN;
            LocTrn mdlLocTRN;

            if (id != clsstockItemCombinedViewModel.tblStockItem.StkiId)
            {
                return NotFound();
            }

            ModelState.Remove("tblStockMedia.StkmPath"); //remove when media implemented

            //remove fields not user modifyable 
            ModelState.Remove("tblStockMedia.StkmItemId");
            ModelState.Remove("tblStockDescription.StkdItemId");

            //remove lists
            ModelState.Remove("lstStockUoms");
            ModelState.Remove("lstStockProductFamilies");

            if (ModelState.IsValid)
            {
                try
                {
                    //find item id
                    StockItem? mdlStockItem = _context.StockItems.SingleOrDefault(x => x.StkiId == id);

                    if (mdlStockItem != null) 
                    {
                        //find description and media records
                        StockDescription? mdlStockItemDescription = _context.StockDescriptions.SingleOrDefault(x => x.StkdItemId == clsstockItemCombinedViewModel.tblStockItem.StkiItemId);
                        StockMedium? mdlStockItemMedia = _context.StockMedia.SingleOrDefault(x => x.StkmItemId == clsstockItemCombinedViewModel.tblStockItem.StkiItemId);

                        if (mdlStockItemDescription != null)
                        {
                            //store changes
                            mdlStockItemDescription.StkdDesc = clsstockItemCombinedViewModel.tblStockDescription.StkdDesc;
                            mdlStockItemDescription.StkdLongDesc = clsstockItemCombinedViewModel.tblStockDescription.StkdLongDesc;
                        }

                        if (mdlStockItemMedia != null)
                        {
                            mdlStockItemMedia.StkmPath = clsstockItemCombinedViewModel.tblStockMedia.StkmPath;

                            if (clsstockItemCombinedViewModel.tblStockMedia.StkmPath != null)
                            { 
                                //set type
                                mdlStockItemMedia.StkmType = GetFileType(clsstockItemCombinedViewModel.tblStockMedia.StkmPath);
                            }
                        }

                        //store stock item changes
                        mdlStockItem.StkiUom = clsstockItemCombinedViewModel.tblStockItem.StkiUom;
                        mdlStockItem.StkiLocLot = clsstockItemCombinedViewModel.tblStockItem.StkiLocLot;
                        mdlStockItem.StkiPrice = clsstockItemCombinedViewModel.tblStockItem.StkiPrice;
                        mdlStockItem.StkiProductFamily = clsstockItemCombinedViewModel.tblStockItem.StkiProductFamily;

                        //save to model
                        _context.StockDescriptions.Update(mdlStockItemDescription);
                        _context.StockItems.Update(mdlStockItem);

                        //if loc/lot tracking OFF check if any existing LOTS for item
                        if (mdlStockItem.StkiLocLot == false)
                        {
                            //find any lots
                           List<StockLot> lstStockLots_Edit = _context.StockLots.Where(x => x.LotItemId == mdlStockItem.StkiItemId).ToList();  //.FirstOrDefault(x => x.LotItemId == mdlStockItem.StkiItemId);

                            //if found create lot_TRN record
                            if (lstStockLots_Edit.Count != 0)
                            {
                                //iterate through the records creating a Lot_TRN record for each

                                foreach (var mdlStockLot in lstStockLots_Edit)
                                {
                                    mdlLotTRN = new LotTrn();
                                    mdlLotTRN.LottLocation = mdlStockLot.LotLocation;
                                    mdlLotTRN.LottQty = mdlStockLot.LotQty;
                                    mdlLotTRN.LottOperation = "Delete";
                                    mdlLotTRN.LottItemId = mdlStockLot.LotItemId;
                                    mdlLotTRN.LottNbr = mdlStockLot.LotId;
                                    _context.Add(mdlLotTRN);
                                }
                            }

                            //find any locations
                            List<StockLoc> lstStockLocs_Delete = _context.StockLocs.Where(x => x.LocItemId == mdlStockItem.StkiItemId).ToList();

                            //if found create loc_TRN record
                            if (lstStockLocs_Delete.Count != 0)
                            {
                                //iterate through the records creating a Loc_TRN record for each

                                foreach (var mdlStockLoc in lstStockLocs_Delete)
                                {
                                    mdlLocTRN = new LocTrn();
                                    mdlLocTRN.LoctLocation = mdlStockLoc.LocLocation;
                                    mdlLocTRN.LoctQty = mdlStockLoc.LocQty;
                                    mdlLocTRN.LoctOperation = "Delete";
                                    mdlLocTRN.LoctItemId = mdlStockLoc.LocItemId;
                                    _context.Add(mdlLocTRN);
                                }
                            }
                        }

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
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!StockItemExists(clsstockItemCombinedViewModel.tblStockItem.StkiId))
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
            return View(clsstockItemCombinedViewModel);
        }

        // GET: StockItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            clsStockItemViewModel clsstockItemCombinedViewModel = new clsStockItemViewModel();


            if (id == null)
            {
                return NotFound();
            }

            var stockItem = await _context.StockItems
                .FirstOrDefaultAsync(m => m.StkiId == id);
            if (stockItem == null)
            {
                return NotFound();
            }

            //find item id
            StockItem? mdlStockItem = _context.StockItems.SingleOrDefault(x => x.StkiId == id);

            if (mdlStockItem != null)
            {
                //populate lists
                var varProductFamilies = _context.StockProductFamilies.ToList();
                var varUOM = _context.StockUoms.ToList();
                //specify WHICH field / fields to USE in the view
                SelectList selProductFamilies = new SelectList(varProductFamilies.Select(x => x.StkpProductFamily));
                SelectList selUOM = new SelectList(varUOM.Select(x => x.StkuDesc), "StkuDesc");
                clsstockItemCombinedViewModel.lstStockProductFamilies = selProductFamilies;
                clsstockItemCombinedViewModel.lstStockUoms = selUOM;

                //find description and media records
                StockDescription? mdlStockItemDescription = _context.StockDescriptions.SingleOrDefault(x => x.StkdItemId == mdlStockItem.StkiItemId);
                StockMedium? mdlStockItemMedia = _context.StockMedia.SingleOrDefault(x => x.StkmItemId == mdlStockItem.StkiItemId);

                if (mdlStockItemDescription != null)
                {
                    clsstockItemCombinedViewModel.tblStockDescription = mdlStockItemDescription;
                }

                if (mdlStockItemMedia != null)
                {
                    clsstockItemCombinedViewModel.tblStockMedia = mdlStockItemMedia;
                }

                clsstockItemCombinedViewModel.tblStockItem = mdlStockItem;

                return View(clsstockItemCombinedViewModel);
            }
            else
            {
                return NotFound();
            }

        }

        // POST: StockItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
//        public async Task<IActionResult> DeleteConfirmed(clsStockItemViewModel stockItemViewModel)
        {
            /*

               Deletes from:

               StockItems

               Trigger in StockItems deletes:

               StockDescriptions
               StockMedium (supposed to be Stock_Media but VS renamed it!)
               StockLoc - writes loc TRN to Loc_TRN and delete StockLoc record (done in this code) 

               Trigger in StockLoc deletes:

               StockLot - writes lot TRN to Lot_TRN and delete StockLot record (done in this code)              

            */
            LocTrn mdlLocTRN;
            LotTrn mdlLotTRN;
            var mdlStockItem = await _context.StockItems.FindAsync(id);

            if (mdlStockItem != null)
            {
                //find any lots
                List<StockLot> lstStockLots_Delete = _context.StockLots.Where(x => x.LotItemId == mdlStockItem.StkiItemId).ToList();  //.FirstOrDefault(x => x.LotItemId == mdlStockItem.StkiItemId);

                //if found create lot_TRN record
                if (lstStockLots_Delete.Count != 0)
                {
                    //iterate through the records creating a Lot_TRN record for each

                    foreach (var mdlStockLot in lstStockLots_Delete)
                    {
                        mdlLotTRN = new LotTrn();
                        mdlLotTRN.LottLocation = mdlStockLot.LotLocation;
                        mdlLotTRN.LottQty = mdlStockLot.LotQty;
                        mdlLotTRN.LottOperation = "Delete";
                        mdlLotTRN.LottItemId = mdlStockLot.LotItemId;
                        mdlLotTRN.LottNbr = mdlStockLot.LotId;
                        _context.Add(mdlLotTRN);
                    }
                }

                //find any locations
                List<StockLoc> lstStockLocs_Delete = _context.StockLocs.Where(x => x.LocItemId == mdlStockItem.StkiItemId).ToList();

                //if found create loc_TRN record
                if (lstStockLocs_Delete.Count != 0)
                {
                    //iterate through the records creating a Loc_TRN record for each
                    foreach (var mdlStockLoc in lstStockLocs_Delete)
                    {
                        mdlLocTRN = new LocTrn();
                        mdlLocTRN.LoctLocation = mdlStockLoc.LocLocation;
                        mdlLocTRN.LoctQty = mdlStockLoc.LocQty;
                        mdlLocTRN.LoctOperation = "Delete";
                        mdlLocTRN.LoctItemId = mdlStockLoc.LocItemId;
                        _context.Add(mdlLocTRN);
                    }
                }

                //removing stock item fires triggers
                _context.StockItems.Remove(mdlStockItem);
            }

            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockItemExists(int id)
        {
            return _context.StockItems.Any(e => e.StkiId == id);
        }
    }
}
