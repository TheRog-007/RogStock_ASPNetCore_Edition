using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RogStock_ASPNetCore.Models_RogStock;
/*
   Created 08/01/2025 By Roger Williams

   added formatting/validation to VISIBLE fields (if any)
   added primary key definition and GetDate() for any datetime columns that require it

*/
public partial class FindSearchFieldDelete
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int FndId { get; set; }

    /// <summary>
    /// Search Type
    /// </summary>
    public string FndSearchType { get; set; } = null!;

    /// <summary>
    /// Field Name
    /// </summary>
    public string FndFieldName { get; set; } = null!;

    /// <summary>
    /// Table Name
    /// </summary>
    public string FndTableName { get; set; } = null!;

    /// <summary>
    /// Search Order
    /// </summary>
    public int FndOrder { get; set; }

    public byte[] Timestamp { get; set; } = null!;
}
