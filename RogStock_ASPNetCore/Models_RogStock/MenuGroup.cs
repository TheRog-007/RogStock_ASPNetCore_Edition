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
public partial class MenuGroup
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GrpId { get; set; }

    /// <summary>
    /// Group
    /// </summary>
    [Required(ErrorMessage = "Group Is Required")]
    [StringLength(20)]
    [DisplayName("Group")]
    public string GrpGroup { get; set; } = null!;

    /// <summary>
    /// Menu Item Group Hass Access To
    /// </summary>
    [Required(ErrorMessage = "Menu Item Is Required")]
    [StringLength(50)]
    [DisplayName("Menu Item Group Has Access To")]
    public string GrpMenuItem { get; set; } = null!;

    public byte[]? Timestamp { get; set; }
}
