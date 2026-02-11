/*

  Created 09/02/2026 By Roger Williams

  used by save function in adjustquantitycontroller

  stores values read from webpage for locations

*/
namespace RogStock_ASPNetCore.OtherClasses
{
    public class clsAdjustQuantity_Loc
    {
        public int LocId { get; set; }
        public decimal Qty { get; set; }
        public decimal OrgQty { get; set; }

        public clsAdjustQuantity_Loc()
        {
            Qty = 0;
            OrgQty = 0;
            LocId = 0;
        }
    }
}
