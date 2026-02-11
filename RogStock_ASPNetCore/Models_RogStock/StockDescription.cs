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
public partial class StockDescription
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StkdId { get; set; }

    /// <summary>
    /// Item ID
    /// </summary>
    [Required(ErrorMessage = "Item ID Is Required")]
    [StringLength(50)]
    [DisplayName("Item ID")]
    public string StkdItemId { get; set; } = null!;

    /// <summary>
    /// Description
    /// </summary>
    [Required(ErrorMessage = "Description Is Required")]
    [StringLength(512)]
    [DisplayName("Description")]
    public string StkdDesc { get; set; } = null!;

    /// <summary>
    /// Long Description
    /// </summary>
    [DisplayName("Long Description")]
    public string? StkdLongDesc { get; set; }

    public byte[]? Timestamp { get; set; }
}
