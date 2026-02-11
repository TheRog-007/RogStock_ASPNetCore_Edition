
using Microsoft.AspNetCore.Mvc.Rendering;   //ADD
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RogStock_ASPNetCore.Models;
using RogStock_ASPNetCore.Models_RogStock;  //ADD

/*

   Created 21/01/2025 By Roger Williams

   used to edit stock locations also contains a list of stock item IDs

*/
namespace RogStock_ASPNetCore.ViewModels
{
    public class clsStockLocsViewModel
    {
        public SelectList lstStockItems { get; set; }
        public SelectList lstStockLocations { get; set; }

        public StockLoc tblStockLoc {  get; set; }
        

        public clsStockLocsViewModel()
        {
            //initalise the data model
            tblStockLoc = new StockLoc();
        }
    }
}
