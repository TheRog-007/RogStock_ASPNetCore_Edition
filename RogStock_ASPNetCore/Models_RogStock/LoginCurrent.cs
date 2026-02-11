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
public partial class LoginCurrent
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LogcId { get; set; }

    /// <summary>
    /// User
    /// </summary>
    [Required(ErrorMessage = "User Is Required")]
    [StringLength(30)]
    [DisplayName("User")]
    public string LogcUser { get; set; } = null!;

    /// <summary>
    /// Date/Time Logged In
    /// </summary>
    [DisplayName("Date/Time Logged In")]
    //date/time set by SQLServer:
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime LogcDateTime { get; set; }

    /// <summary>
    /// IP Address of PC
    /// </summary>
    [DisplayName("IP Address of PC")]
    public string LogcPcip { get; set; } = null!;

    public byte[]? Timestamp { get; set; }
}
