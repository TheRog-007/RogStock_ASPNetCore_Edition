using Microsoft.Build.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RogStock_ASPNetCore.Models_RogStock;
/*
   Created 08/01/2025 By Roger Williams

   added formatting/validation oto VISIBLE fields

*/
public partial class LocTrn
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LoctId { get; set; }

    /// <summary>
    /// Item ID
    /// </summary>
    [Required(ErrorMessage = "Item ID Is Required")]
    [StringLength(50)]
    [DisplayName("Item ID")]
    public string? LoctItemId { get; set; }

    /// <summary>
    /// Location
    /// </summary>
    [Required(ErrorMessage = "Location Is Required")]
    [StringLength(30)]
    [DisplayName("Location")]

    public string LoctLocation { get; set; } = null!;

    /// <summary>
    /// Date/Time
    /// </summary>

    //next set of columns are HIDDEN from the user
    //date/time set by SQLServer:
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime LoctDateTime { get; set; }

    /// <summary>
    /// Previous Name
    /// </summary>
    public string? LoctOldLocation { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [DisplayName("Qty")]
    public decimal LoctQty { get; set; }

    /// <summary>
    /// Operation
    /// </summary>
    public string LoctOperation { get; set; } = null!;

    public byte[]? Timestamp { get; set; }
}
