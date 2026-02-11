
using Microsoft.AspNetCore.Mvc.Rendering;   //ADD
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RogStock_ASPNetCore.Models;
using RogStock_ASPNetCore.Models_RogStock;  //ADD

/*

   Created 21/01/2025 By Roger Williams

   used to edit stock lots  also contains a list of stock locations and stock items

*/
namespace RogStock_ASPNetCore.ViewModels
{
    public class clsStockLotsViewModel
    {
        public StockLot tblStockLot { get; set; }
        public SelectList lstStockLocations {  get; set; }
        public SelectList lstStockItems { get; set; }

        public clsStockLotsViewModel()
        {
            tblStockLot = new StockLot();
        }
    }
}
