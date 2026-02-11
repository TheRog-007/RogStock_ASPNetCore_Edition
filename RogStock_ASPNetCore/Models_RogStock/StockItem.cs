using Microsoft.Build.Tasks.Deployment.Bootstrapper;
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
   added default values
*/
public partial class StockItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StkiId { get; set; }

    /// <summary>
    /// Item ID
    /// </summary>
    [Required(ErrorMessage = "Item ID Is Required")]
    [StringLength(50)]
    [DisplayName("Item ID")]
    public string StkiItemId { get; set; } = null!;

    /// <summary>
    /// Product Family
    /// </summary>
    [Required(ErrorMessage = "Product Family Is Required")]
    [StringLength(20)]
    [DisplayName("Product Family")]
    public string StkiProductFamily { get; set; } = null!;

    /// <summary>
    /// Loc/Lot Tracking?
    /// </summary>
    [DefaultValue(true)]
    [DisplayName("Loc/Lot Tracking?")]
    public bool StkiLocLot { get; set; }

    /// <summary>
    /// Unit of Measure
    /// </summary>
    [Required(ErrorMessage = "UOM Is Required")]
    [StringLength(20)]
    [DisplayName("UOM")]
    public string StkiUom { get; set; } = null!;

    /// <summary>
    /// Price
    /// </summary>
    [Required(ErrorMessage = "Price Is Required")]
    [DisplayName("Price")]
    [DefaultValue(0)]
    public decimal StkiPrice { get; set; }

    public byte[]? Timestamp { get; set; }
}
