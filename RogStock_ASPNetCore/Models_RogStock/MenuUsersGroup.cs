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
public partial class MenuUsersGroup
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UsrgrpId { get; set; }

    /// <summary>
    /// User
    /// </summary>
    [Required(ErrorMessage = "User Is Required")]
    [StringLength(20)]
    [DisplayName("User")]
    public string UsrgrpUser { get; set; } = null!;

    /// <summary>
    /// Group
    /// </summary>
    [Required(ErrorMessage = "Group Is Required")]
    [StringLength(20)]
    [DisplayName("Group")]
    public string UsrgrpGroup { get; set; } = null!;

    public byte[]? Timestamp { get; set; }
}
