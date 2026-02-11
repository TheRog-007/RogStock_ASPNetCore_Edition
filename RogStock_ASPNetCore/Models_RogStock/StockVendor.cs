using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RogStock_ASPNetCore.Models_RogStock;
/*
   Created 08/01/2025 By Roger Williams

   added formatting/validation to VISIBLE fields (if any)
   added primary key definition and GetDate() for any datetime columns that require it

*/
public partial class StockVendor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StkvId { get; set; }

    /// <summary>
    /// Vendor ID
    /// </summary>
    public int StkvVendorId { get; set; }

    /// <summary>
    /// Item ID
    /// </summary>
    public string StkvItemId { get; set; } = null!;

    /// <summary>
    /// Price
    /// </summary>
    public decimal StkvPrice { get; set; }

    /// <summary>
    /// Preferred Vendor?
    /// </summary>
    public bool StkvPreferred { get; set; }

    public byte[]? Timestamp { get; set; }
}
