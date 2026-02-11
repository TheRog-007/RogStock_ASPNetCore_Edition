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
public partial class FindFieldInfo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int FfiId { get; set; }

    public string FfiSearchType { get; set; } = null!;

    /// <summary>
    /// Table Name
    /// </summary>
    public string? FfiTableName { get; set; }

    /// <summary>
    /// Field Name
    /// </summary>
    public string? FfiFieldname { get; set; }

    /// <summary>
    /// Order In List
    /// </summary>
    public int FfiOrder { get; set; }

    /// <summary>
    /// Filed Data Type
    /// </summary>
    public string? FfiFieldDataType { get; set; }

    /// <summary>
    /// Label Text
    /// </summary>
    public string? FfiLabelText { get; set; }

    /// <summary>
    /// Field Length
    /// </summary>
    public int? FfiFieldLength { get; set; }

    /// <summary>
    /// Decimal Places
    /// </summary>
    public int? FfiDecimalPlaces { get; set; }

    public byte[] Timestamp { get; set; } = null!;
}
