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

*/
public partial class LotTrn
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LottId { get; set; }

    /// <summary>
    /// Item ID
    /// </summary>
    [Required(ErrorMessage = "Item ID Is Required")]
    [StringLength(50)]
    [DisplayName("Item ID")]
    public string LottItemId { get; set; } = null!;

    [DisplayName("Lot Nbr")]
    public int LottNbr { get; set; }

    /// <summary>
    /// Date/Time
    /// </summary>
    //date/time set by SQLServer:
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [DisplayName("Date/Time")]
    public DateTime LottDateTime { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [DisplayName("Quantity")]
    public decimal LottQty { get; set; }

    /// <summary>
    /// Location
    /// </summary>
    [DisplayName("Location")]
    public string LottLocation { get; set; } = null!;

    /// <summary>
    /// Operation
    /// </summary>
    [DisplayName("Operation")]
    public string LottOperation { get; set; } = null!;

    public byte[]? Timestamp { get; set; }
}
