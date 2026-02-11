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
public partial class Login
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LogId { get; set; }

    /// <summary>
    /// User
    /// </summary>
    [Required(ErrorMessage = "User Is Required")]
    [StringLength(30)]
    [DisplayName("User")]
    public string LogUser { get; set; } = null!;

    /// <summary>
    /// Password
    /// </summary>
    [Required(ErrorMessage = "Password Is Required")]
    [StringLength(10)]
    [DisplayName("Password")]
    public string LogPassword { get; set; } = null!;

    public byte[]? Timestamp { get; set; }
}
