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

   Note: in SQl Server this table is called: StockMedia - why does .Net rename it StockMedium??

*/
public partial class StockMedium
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StkmId { get; set; }

    /// <summary>
    /// Item ID
    /// </summary>
    [Required(ErrorMessage = "Item ID Is Required")]
    [StringLength(50)]
    [DisplayName("Item ID")]
    public string StkmItemId { get; set; } = null!;

    /// <summary>
    /// Path To File
    /// </summary>
    [Required(ErrorMessage = "File Path Is Required")]
    [StringLength(512)]
    [DisplayName("Path To File")]
    public string StkmPath { get; set; } = null!;

    /// <summary>
    /// Type
    /// </summary>
    [StringLength(10)]
    [DisplayName("Type")]
    public string? StkmType { get; set; }

    public byte[]? Timestamp { get; set; }
}
