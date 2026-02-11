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
public partial class StockLoc
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LocId { get; set; }

    /// <summary>
    /// Item ID
    /// </summary>
    [Required(ErrorMessage = "Item ID Is Required")]
    [StringLength(50)]
    [DisplayName("Item ID")]
    public string LocItemId { get; set; } = null!;

    /// <summary>
    /// Location
    /// </summary>
    [Required(ErrorMessage = "location Is Required")]
    [StringLength(30)]
    [DisplayName("Location")]
    public string LocLocation { get; set; } = null!;

    /// <summary>
    /// Date Updated
    /// </summary>
       //date/time set by SQLServer:
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [DisplayName("Date/Time Updated")]
    public DateTime LocUpdated { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [Required(ErrorMessage = "Quantity Is Required")]
    [DisplayName("Location Quantity")]
    [DefaultValue(0)]
    public decimal LocQty { get; set; }

    /// <summary>
    /// Non Net?
    /// </summary>
    [DisplayName("Non Net?")]
    [DefaultValue(false)]
    public bool LocNonNet { get; set; }

    [DisplayName("Location Description")]
    public string? LocDescription { get; set; }
   
    public byte[]? Timestamp { get; set; }
}
