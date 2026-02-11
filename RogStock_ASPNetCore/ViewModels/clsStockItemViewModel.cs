using Microsoft.AspNetCore.Mvc.Rendering;   //ADD
using Microsoft.Identity.Client;
using RogStock_ASPNetCore.Models;
using RogStock_ASPNetCore.Models_RogStock;  //ADD


namespace RogStock_ASPNetCore.ViewModels
{
    /*
    
       Created 09/01/2025 By Roger Williams

       used to bind stockitems, stockdescriptions, stockmedia models with uom and product family as lists
       so data can be passed to stockitems view so uom/product family
       comboboxes can be populated with the list data AND have the stocm items
       data for the selected item also available to view

    */
    public class clsStockItemViewModel
    {
        public StockItem tblStockItem {  get; set; }
        public StockDescription tblStockDescription { get; set; }
        public StockMedium tblStockMedia { get; set; }
        public SelectList lstStockProductFamilies { get; set; }
        public SelectList lstStockUoms { get; set; }

        public clsStockItemViewModel() 
        {
            //initalise the data models
            tblStockItem = new StockItem();
            tblStockDescription = new StockDescription();
            tblStockMedia = new StockMedium();
        }
    }
}
