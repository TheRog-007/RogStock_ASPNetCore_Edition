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
public partial class StockProductFamily
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StkpId { get; set; }

    /// <summary>
    /// Product Family
    /// </summary>

    [Required(ErrorMessage = "Product Family Is Required")]
    [StringLength(30)]
    [DisplayName("Product Family")]
    public string StkpProductFamily { get; set; } = null!;

    /// <summary>
    /// Description
    /// </summary>
    [Required(ErrorMessage = "Description Is Required")]
    [StringLength(50)]
    [DisplayName("Description")]
    public string StkpDesc { get; set; } = null!;

    public byte[]? Timestamp { get; set; }
}
