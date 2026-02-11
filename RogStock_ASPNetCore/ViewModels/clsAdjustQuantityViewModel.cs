using Microsoft.AspNetCore.Mvc.Rendering; //add
using RogStock_ASPNetCore.Models_RogStock;  //ADD

namespace RogStock_ASPNetCore.ViewModels
{
    public class clsAdjustQuantityViewModel
    {
        public SelectList lstStockItems { get; set; }

        public string strItemID { get; set; }

        public List<clsLocationsForEdit> lstLocationsForEdit;
        public List<clsLotsForEdit> lstLotsForEdit;

        public clsAdjustQuantityViewModel() 
        {
            lstLocationsForEdit = new List<clsLocationsForEdit>();
            lstLotsForEdit = new List<clsLotsForEdit>();
        }

        public class clsLocationsForEdit
        {
            public int LocId { get; set; }
            public string Location { get; set; } = null!;

            public decimal LocQty { get; set; }

            //hidden used to check for changes
            public decimal LocOriginalQty { get; set; }
            public string LocationDesc { get; set; }

            public clsLocationsForEdit() 
            {
                Location = String.Empty;
                LocQty = 0;
                LocOriginalQty = 0;
                LocationDesc = String.Empty;         
                LocId = 0;
            }
        }


        public class clsLotsForEdit
        {
            public int LotId { get; set; }
            public string LotLocation { get; set; }

            public decimal LotQty { get; set; }

            //hidden used to check for changes
            public decimal LotOriginalQty { get; set; }
            public string LotDesc { get; set; }

            public clsLotsForEdit()
            {
                LotQty = 0;
                LotOriginalQty = 0;
                LotDesc = String.Empty;
                LotId = 0;
                LotLocation = String.Empty;
            }
        }
    }
}
