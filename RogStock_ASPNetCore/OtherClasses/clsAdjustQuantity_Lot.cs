namespace RogStock_ASPNetCore.OtherClasses
{
    public class clsAdjustQuantity_Lot
    {
        public int LotId { get; set; }
        public decimal Qty { get; set; }
        public decimal OrgQty { get; set; }

        public clsAdjustQuantity_Lot()
        {
            Qty = 0;
            OrgQty = 0;
            LotId = 0;
        }
    }
}
