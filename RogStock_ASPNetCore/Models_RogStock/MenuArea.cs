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
public partial class MenuArea
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SecId { get; set; }

    /// <summary>
    /// Menu Area
    /// </summary>
    [Required(ErrorMessage = "Menu Area Is Required")]
    [StringLength(20)]
    [DisplayName("Menu Area")]
    public string SecArea { get; set; } = null!;

    public byte[]? Timestamp { get; set; }
}
