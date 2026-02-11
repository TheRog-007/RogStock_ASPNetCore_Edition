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
public partial class FindRelation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int FfrId { get; set; }

    /// <summary>
    /// Search Type
    /// </summary>
    public string FfrSearchType { get; set; } = null!;

    /// <summary>
    /// Table name
    /// </summary>
    public string? FfrTableName { get; set; }

    /// <summary>
    /// Qualified Join
    /// </summary>
    public string? FfrQualifiedJoin { get; set; }

    /// <summary>
    /// Search Condition
    /// </summary>
    public string? FfrSearchCondition { get; set; }

    /// <summary>
    /// Is Default?
    /// </summary>
    public bool FfrIsDefault { get; set; }

    /// <summary>
    /// Fields From Other Tables
    /// </summary>
    public string? FfrOtherTableFields { get; set; }

    /// <summary>
    /// Distinct Search?
    /// </summary>
    public bool FfrDistinct { get; set; }

    /// <summary>
    /// Group By
    /// </summary>
    public string? FfrGroupBy { get; set; }

    /// <summary>
    /// Order By
    /// </summary>
    public string? FfrOrderBy { get; set; }

    public byte[] Timestamp { get; set; } = null!;
}
