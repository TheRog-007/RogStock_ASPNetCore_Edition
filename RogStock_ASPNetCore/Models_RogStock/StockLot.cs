using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RogStock_ASPNetCore.Models_RogStock;
/*
   Created 08/01/2025 By Roger Williams

   added formatting/validation to VISIBLE fields (if any)
   added primary key definition and GetDate() for any datetime columns that require it

   Made a cockup in the SQL design as lot nbr should NOT be a separate filed as it is
   the LotID!

*/
public partial class StockLot
{
    /// <summary>
    /// Lot Number
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LotId { get; set; }

    /// <summary>
    /// Item ID
    /// </summary>
    [Required(ErrorMessage = "Item ID Is Required")]
    [StringLength(50)]
    [DisplayName("Item ID")]
    public string LotItemId { get; set; } = null!;
    
    //[DisplayName("Lot Nbr")]
    //public int? LotNbr { get; set; }

    /// <summary>
    /// Date Updated
    /// </summary>
    //date/time set by SQLServer:
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [DisplayName("Date/Time Updated")]
    public DateTime LotUpdated { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [Required(ErrorMessage = "Lot Quantity Is Required")]
    [DisplayName("Lot Qty")]

    public decimal LotQty { get; set; }

    /// <summary>
    /// Non Net?
    /// </summary>
    [DisplayName("Non Net?")]
    [DefaultValue(false)]
    public bool LotNonNet { get; set; }

    /// <summary>
    /// Location
    /// </summary>
    [Required(ErrorMessage = "Location Is Required")]
    [StringLength(30)]
    [DisplayName("Location")]
    public string LotLocation { get; set; } = null!;

    public byte[]? Timestamp { get; set; }
}
