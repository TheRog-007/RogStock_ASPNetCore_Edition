using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RogStock_ASPNetCore.Models_RogStock;

public partial class RogStockDbContext : DbContext
{
    public RogStockDbContext()
    {
    }

    public RogStockDbContext(DbContextOptions<RogStockDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FindFieldInfo> FindFieldInfos { get; set; }

    public virtual DbSet<FindRelation> FindRelations { get; set; }

    public virtual DbSet<FindSearchFieldDelete> FindSearchFieldDeletes { get; set; }

    public virtual DbSet<LocTrn> LocTrns { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<LoginCurrent> LoginCurrents { get; set; }

    public virtual DbSet<LotTrn> LotTrns { get; set; }

    public virtual DbSet<MenuArea> MenuAreas { get; set; }

    public virtual DbSet<MenuGroup> MenuGroups { get; set; }

    public virtual DbSet<MenuMenuItem> MenuMenuItems { get; set; }

    public virtual DbSet<MenuUsersGroup> MenuUsersGroups { get; set; }

    public virtual DbSet<StockDescription> StockDescriptions { get; set; }

    public virtual DbSet<StockItem> StockItems { get; set; }

    public virtual DbSet<StockLoc> StockLocs { get; set; }

    public virtual DbSet<StockLot> StockLots { get; set; }

    public virtual DbSet<StockMedium> StockMedia { get; set; }

    public virtual DbSet<StockProductFamily> StockProductFamilies { get; set; }

    public virtual DbSet<StockUom> StockUoms { get; set; }

    public virtual DbSet<StockVendor> StockVendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-694Q8HR;Initial Catalog=RogStock;User ID=sa;Password=RogServer1;encrypt=false;Trusted_Connection=true");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //added 15/01/2026 By Roger Williams
        //
        //Copied from: https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-7.0/breaking-changes?tabs=data-annotations%2Cv7#sqlserver-tables-with-triggers
        //
        //should allow SQL triggers to work in tables that have them!
        modelBuilder.Entity<StockItem>()
            .ToTable(tb => tb.UseSqlOutputClause(false));
        modelBuilder.Entity<StockLoc>()
            .ToTable(tb => tb.UseSqlOutputClause(false));
        //end added

        modelBuilder.UseCollation("Latin1_General_CI_AS");

        modelBuilder.Entity<FindFieldInfo>(entity =>
        {
            entity.HasKey(e => e.FfiId);

            entity.ToTable("Find_FieldInfo");

            entity.Property(e => e.FfiId).HasColumnName("FFI_ID");
            entity.Property(e => e.FfiDecimalPlaces)
                .HasDefaultValue(0)
                .HasComment("Decimal Places")
                .HasColumnName("FFI_DecimalPlaces");
            entity.Property(e => e.FfiFieldDataType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasComment("Filed Data Type")
                .HasColumnName("FFI_FieldDataType");
            entity.Property(e => e.FfiFieldLength)
                .HasDefaultValue(0)
                .HasComment("Field Length")
                .HasColumnName("FFI_FieldLength");
            entity.Property(e => e.FfiFieldname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Field Name")
                .HasColumnName("FFI_Fieldname");
            entity.Property(e => e.FfiLabelText)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Label Text")
                .HasColumnName("FFI_LabelText");
            entity.Property(e => e.FfiOrder)
                .HasComment("Order In List")
                .HasColumnName("FFI_Order");
            entity.Property(e => e.FfiSearchType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FFI_SearchType");
            entity.Property(e => e.FfiTableName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Table Name")
                .HasColumnName("FFI_TableName");
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("timestamp");
        });

        modelBuilder.Entity<FindRelation>(entity =>
        {
            entity.HasKey(e => e.FfrId);

            entity.ToTable("Find_Relations");

            entity.Property(e => e.FfrId).HasColumnName("FFR_ID");
            entity.Property(e => e.FfrDistinct)
                .HasComment("Distinct Search?")
                .HasColumnName("FFR_Distinct");
            entity.Property(e => e.FfrGroupBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("((0))")
                .HasComment("Group By")
                .HasColumnName("FFR_GroupBy");
            entity.Property(e => e.FfrIsDefault)
                .HasComment("Is Default?")
                .HasColumnName("FFR_IsDefault");
            entity.Property(e => e.FfrOrderBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Order By")
                .HasColumnName("FFR_OrderBy");
            entity.Property(e => e.FfrOtherTableFields)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("Fields From Other Tables")
                .HasColumnName("FFR_OtherTableFields");
            entity.Property(e => e.FfrQualifiedJoin)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasComment("Qualified Join")
                .HasColumnName("FFR_QualifiedJoin");
            entity.Property(e => e.FfrSearchCondition)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Search Condition")
                .HasColumnName("FFR_SearchCondition");
            entity.Property(e => e.FfrSearchType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Search Type")
                .HasColumnName("FFR_SearchType");
            entity.Property(e => e.FfrTableName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Table name")
                .HasColumnName("FFR_TableName");
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("timestamp");
        });

        modelBuilder.Entity<FindSearchFieldDelete>(entity =>
        {
            entity.HasKey(e => e.FndId).HasName("PK_Find_SearchField");

            entity.ToTable("Find_SearchField_Delete?");

            entity.Property(e => e.FndId).HasColumnName("FND_ID");
            entity.Property(e => e.FndFieldName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Field Name")
                .HasColumnName("FND_FieldName");
            entity.Property(e => e.FndOrder)
                .HasComment("Search Order")
                .HasColumnName("FND_Order");
            entity.Property(e => e.FndSearchType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Search Type")
                .HasColumnName("FND_SearchType");
            entity.Property(e => e.FndTableName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Table Name")
                .HasColumnName("FND_TableName");
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("timestamp");
        });

        modelBuilder.Entity<LocTrn>(entity =>
        {
            entity.HasKey(e => e.LoctId);

            entity.ToTable("Loc_TRN");

            entity.Property(e => e.LoctId).HasColumnName("LOCT_ID");
            entity.Property(e => e.LoctDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date/Time")
                .HasColumnType("datetime")
                .HasColumnName("LOCT_DateTime");
            entity.Property(e => e.LoctItemId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Item ID")
                .HasColumnName("LOCT_ItemID");
            entity.Property(e => e.LoctLocation)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasComment("Location")
                .HasColumnName("LOCT_Location");
            entity.Property(e => e.LoctOldLocation)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasComment("Previous Name")
                .HasColumnName("LOCT_OldLocation");
            entity.Property(e => e.LoctOperation)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasComment("Operation")
                .HasColumnName("LOCT_Operation");
            entity.Property(e => e.LoctQty)
                .HasComment("Quantity")
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("LOCT_Qty");
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("timestamp");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.LogId);

            entity.ToTable("Login");

            entity.Property(e => e.LogId).HasColumnName("LOG_ID");
            entity.Property(e => e.LogPassword)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasComment("Password")
                .HasColumnName("LOG_Password");
            entity.Property(e => e.LogUser)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasComment("User")
                .HasColumnName("LOG_User");
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("timestamp");
        });

        modelBuilder.Entity<LoginCurrent>(entity =>
        {
            entity.HasKey(e => e.LogcId);

            entity.ToTable("Login_Current");

            entity.Property(e => e.LogcId).HasColumnName("LOGC_ID");
            entity.Property(e => e.LogcDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date/Time Logged In")
                .HasColumnType("datetime")
                .HasColumnName("LOGC_DateTime");
            entity.Property(e => e.LogcPcip)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("IP Address of PC")
                .HasColumnName("LOGC_PCIP");
            entity.Property(e => e.LogcUser)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasComment("User")
                .HasColumnName("LOGC_User");
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("timestamp");
        });

        modelBuilder.Entity<LotTrn>(entity =>
        {
            entity.HasKey(e => e.LottId);

            entity.ToTable("Lot_TRN");

            entity.Property(e => e.LottId).HasColumnName("LOTT_ID");
            entity.Property(e => e.LottDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date/Time")
                .HasColumnType("datetime")
                .HasColumnName("LOTT_DateTime");
            entity.Property(e => e.LottItemId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Item ID")
                .HasColumnName("LOTT_ItemID");
            entity.Property(e => e.LottLocation)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasComment("Location")
                .HasColumnName("LOTT_Location");
            entity.Property(e => e.LottNbr).HasColumnName("LOTT_Nbr");
            entity.Property(e => e.LottOperation)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasComment("Operation")
                .HasColumnName("LOTT_Operation");
            entity.Property(e => e.LottQty)
                .HasComment("Quantity")
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("LOTT_Qty");
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("timestamp");
        });

        modelBuilder.Entity<MenuArea>(entity =>
        {
            entity.HasKey(e => e.SecId);

            entity.ToTable("Menu_Areas");

            entity.Property(e => e.SecId).HasColumnName("SEC_ID");
            entity.Property(e => e.SecArea)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("Menu Area")
                .HasColumnName("SEC_Area");
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("timestamp");
        });

        modelBuilder.Entity<MenuGroup>(entity =>
        {
            entity.HasKey(e => e.GrpId);

            entity.ToTable("Menu_Groups");

            entity.Property(e => e.GrpId).HasColumnName("GRP_ID");
            entity.Property(e => e.GrpGroup)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("Group")
                .HasColumnName("GRP_Group");
            entity.Property(e => e.GrpMenuItem)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Menu Item Group Hass Access To")
                .HasColumnName("GRP_MenuItem");
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("timestamp");
        });

        modelBuilder.Entity<MenuMenuItem>(entity =>
        {
            entity.HasKey(e => e.MnuId);

            entity.ToTable("Menu_MenuItems");

            entity.Property(e => e.MnuId).HasColumnName("MNU_ID");
            entity.Property(e => e.MnuDisplayWhere)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Display Where")
                .HasColumnName("MNU_DisplayWhere");
            entity.Property(e => e.MnuMenuItemName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Menu Item Name")
                .HasColumnName("MNU_MenuItemName");
            entity.Property(e => e.MnuMenuItemObject)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Menu Item Object")
                .HasColumnName("MNU_MenuItemObject");
            entity.Property(e => e.MnuType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MNU_Type");
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("timestamp");
        });

        modelBuilder.Entity<MenuUsersGroup>(entity =>
        {
            entity.HasKey(e => e.UsrgrpId);

            entity.ToTable("Menu_UsersGroups");

            entity.Property(e => e.UsrgrpId).HasColumnName("USRGRP__ID");
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("timestamp");
            entity.Property(e => e.UsrgrpGroup)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("Group")
                .HasColumnName("USRGRP_Group");
            entity.Property(e => e.UsrgrpUser)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("User")
                .HasColumnName("USRGRP_User");
        });

        modelBuilder.Entity<StockDescription>(entity =>
        {
            entity.HasKey(e => e.StkdId);

            entity.ToTable("Stock_Description");

            entity.Property(e => e.StkdId).HasColumnName("STKD_ID");
            entity.Property(e => e.StkdDesc)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasComment("Description")
                .HasColumnName("STKD_Desc");
            entity.Property(e => e.StkdItemId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Item ID")
                .HasColumnName("STKD_ItemID");
            entity.Property(e => e.StkdLongDesc)
                .HasComment("Long Description")
                .HasColumnType("text")
                .HasColumnName("STKD_LongDesc");
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("timestamp");
        });

        modelBuilder.Entity<StockItem>(entity =>
        {
            entity.HasKey(e => e.StkiId);

            entity.ToTable("Stock_Items");

            entity.Property(e => e.StkiId).HasColumnName("STKI_ID");
            entity.Property(e => e.StkiItemId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Item ID")
                .HasColumnName("STKI_ItemID");
            entity.Property(e => e.StkiLocLot)
                .HasDefaultValue(true)
                .HasComment("Loc/Lot Tracking?")
                .HasColumnName("STKI_LocLot");
            entity.Property(e => e.StkiPrice)
                .HasComment("Price")
                .HasColumnType("money")
                .HasColumnName("STKI_Price");
            entity.Property(e => e.StkiProductFamily)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("Product Family")
                .HasColumnName("STKI_ProductFamily");
            entity.Property(e => e.StkiUom)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("Unit of Measure")
                .HasColumnName("STKI_UOM");
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("timestamp");
        });

        modelBuilder.Entity<StockLoc>(entity =>
        {
            entity.HasKey(e => e.LocId);

            entity.ToTable("Stock_Loc");

            entity.Property(e => e.LocId).HasColumnName("LOC_ID");
            entity.Property(e => e.LocDescription)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("LOC_Description");
            entity.Property(e => e.LocItemId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Item ID")
                .HasColumnName("LOC_ItemID");
            entity.Property(e => e.LocLocation)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasComment("Location")
                .HasColumnName("LOC_Location");
            entity.Property(e => e.LocNonNet)
                .HasComment("Non Net?")
                .HasColumnName("LOC_NonNet");
            entity.Property(e => e.LocQty)
                .HasComment("Quantity")
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("LOC_Qty");
            entity.Property(e => e.LocUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date Updated")
                .HasColumnType("datetime")
                .HasColumnName("LOC_Updated");
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("timestamp");
        });

        modelBuilder.Entity<StockLot>(entity =>
        {
            entity.HasKey(e => e.LotId);

            entity.ToTable("Stock_Lot");

            entity.Property(e => e.LotId)
                .HasComment("Lot Number")
                .HasColumnName("LOT_ID");
            entity.Property(e => e.LotItemId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Item ID")
                .HasColumnName("LOT_ItemID");
            entity.Property(e => e.LotLocation)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasComment("Location")
                .HasColumnName("LOT_Location");
            //entity.Property(e => e.LotNbr)
            //    .HasDefaultValue(0)
            //    .HasColumnName("LOT_Nbr");
            entity.Property(e => e.LotNonNet)
                .HasComment("Non Net?")
                .HasColumnName("LOT_NonNet");
            entity.Property(e => e.LotQty)
                .HasComment("Quantity")
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("LOT_Qty");
            entity.Property(e => e.LotUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date Updated")
                .HasColumnType("datetime")
                .HasColumnName("LOT_Updated");
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("timestamp");
        });

        modelBuilder.Entity<StockMedium>(entity =>
        {
            entity.HasKey(e => e.StkmId);

            entity.ToTable("Stock_Media");

            entity.Property(e => e.StkmId).HasColumnName("STKM_ID");
            entity.Property(e => e.StkmItemId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Item ID")
                .HasColumnName("STKM_ItemID");
            entity.Property(e => e.StkmPath)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasComment("Path To File")
                .HasColumnName("STKM_Path");
            entity.Property(e => e.StkmType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasComment("Type")
                .HasColumnName("STKM_Type");
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("timestamp");
        });

        modelBuilder.Entity<StockProductFamily>(entity =>
        {
            entity.HasKey(e => e.StkpId);

            entity.ToTable("Stock_ProductFamily");

            entity.Property(e => e.StkpId).HasColumnName("STKP_ID");
            entity.Property(e => e.StkpDesc)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasComment("Description")
                .HasColumnName("STKP_Desc");
            entity.Property(e => e.StkpProductFamily)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasComment("Product Family")
                .HasColumnName("STKP_ProductFamily");
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("timestamp");
        });

        modelBuilder.Entity<StockUom>(entity =>
        {
            entity.HasKey(e => e.StkuId);

            entity.ToTable("Stock_UOM");

            entity.Property(e => e.StkuId).HasColumnName("STKU_ID");
            entity.Property(e => e.StkuDesc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("Description")
                .HasColumnName("STKU_Desc");
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("timestamp");
        });

        modelBuilder.Entity<StockVendor>(entity =>
        {
            entity.HasKey(e => e.StkvId);

            entity.ToTable("Stock_Vendors");

            entity.Property(e => e.StkvId).HasColumnName("STKV_ID");
            entity.Property(e => e.StkvItemId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Item ID")
                .HasColumnName("STKV_ItemID");
            entity.Property(e => e.StkvPreferred)
                .HasComment("Preferred Vendor?")
                .HasColumnName("STKV_Preferred");
            entity.Property(e => e.StkvPrice)
                .HasComment("Price")
                .HasColumnType("money")
                .HasColumnName("STKV_Price");
            entity.Property(e => e.StkvVendorId)
                .HasComment("Vendor ID")
                .HasColumnName("STKV_VendorID");
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("timestamp");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
