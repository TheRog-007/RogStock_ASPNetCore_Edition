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
public partial class MenuMenuItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MnuId { get; set; }

    /// <summary>
    /// Menu Item Name
    /// </summary>
    [Required(ErrorMessage = "Menu Item name Is Required")]
    [StringLength(50)]
    [DisplayName("Menu Item Name")]
    public string MnuMenuItemName { get; set; } = null!;

    /// <summary>
    /// Menu Item Object
    /// </summary>
    [Required(ErrorMessage = "Menu Item Object Is Required")]
    [StringLength(50)]
    [DisplayName("Menu Item Object")]
    public string MnuMenuItemObject { get; set; } = null!;

    [Required(ErrorMessage = "Menu Type Is Required")]
    [StringLength(10)]
    [DisplayName("Menu Type")]
    public string MnuType { get; set; } = null!;

    /// <summary>
    /// Display Where
    /// </summary>
    [Required(ErrorMessage = "Display Where Is Required")]
    [StringLength(50)]
    [DisplayName("Display Where")]
    public string MnuDisplayWhere { get; set; } = null!;

    public byte[]? Timestamp { get; set; }
}
