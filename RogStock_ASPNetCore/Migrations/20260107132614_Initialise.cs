using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RogStock_ASPNetCore.Migrations
{
    /// <inheritdoc />
    public partial class Initialise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Find_FieldInfo",
                columns: table => new
                {
                    FFI_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FFI_SearchType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    FFI_TableName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, comment: "Table Name"),
                    FFI_Fieldname = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, comment: "Field Name"),
                    FFI_Order = table.Column<int>(type: "int", nullable: false, comment: "Order In List"),
                    FFI_FieldDataType = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true, comment: "Filed Data Type"),
                    FFI_LabelText = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, comment: "Label Text"),
                    FFI_FieldLength = table.Column<int>(type: "int", nullable: true, defaultValue: 0, comment: "Field Length"),
                    FFI_DecimalPlaces = table.Column<int>(type: "int", nullable: true, defaultValue: 0, comment: "Decimal Places"),
                    timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Find_FieldInfo", x => x.FFI_ID);
                });

            migrationBuilder.CreateTable(
                name: "Find_Relations",
                columns: table => new
                {
                    FFR_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FFR_SearchType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, comment: "Search Type"),
                    FFR_TableName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, comment: "Table name"),
                    FFR_QualifiedJoin = table.Column<string>(type: "varchar(512)", unicode: false, maxLength: 512, nullable: true, comment: "Qualified Join"),
                    FFR_SearchCondition = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, comment: "Search Condition"),
                    FFR_IsDefault = table.Column<bool>(type: "bit", nullable: false, comment: "Is Default?"),
                    FFR_OtherTableFields = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true, comment: "Fields From Other Tables"),
                    FFR_Distinct = table.Column<bool>(type: "bit", nullable: false, comment: "Distinct Search?"),
                    FFR_GroupBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValueSql: "((0))", comment: "Group By"),
                    FFR_OrderBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, comment: "Order By"),
                    timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Find_Relations", x => x.FFR_ID);
                });

            migrationBuilder.CreateTable(
                name: "Find_SearchField_Delete?",
                columns: table => new
                {
                    FND_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FND_SearchType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, comment: "Search Type"),
                    FND_FieldName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, comment: "Field Name"),
                    FND_TableName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, comment: "Table Name"),
                    FND_Order = table.Column<int>(type: "int", nullable: false, comment: "Search Order"),
                    timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Find_SearchField", x => x.FND_ID);
                });

            migrationBuilder.CreateTable(
                name: "Loc_TRN",
                columns: table => new
                {
                    LOCT_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LOCT_ItemID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, comment: "Item ID"),
                    LOCT_Location = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false, comment: "Location"),
                    LOCT_DateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date/Time"),
                    LOCT_OldLocation = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true, comment: "Previous Name"),
                    LOCT_Qty = table.Column<decimal>(type: "numeric(18,0)", nullable: false, comment: "Quantity"),
                    LOCT_Operation = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false, comment: "Operation"),
                    timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loc_TRN", x => x.LOCT_ID);
                });

            migrationBuilder.CreateTable(
                name: "Login",
                columns: table => new
                {
                    LOG_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LOG_User = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false, comment: "User"),
                    LOG_Password = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false, comment: "Password"),
                    timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => x.LOG_ID);
                });

            migrationBuilder.CreateTable(
                name: "Login_Current",
                columns: table => new
                {
                    LOGC_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LOGC_User = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false, comment: "User"),
                    LOGC_DateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date/Time Logged In"),
                    LOGC_PCIP = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, comment: "IP Address of PC"),
                    timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login_Current", x => x.LOGC_ID);
                });

            migrationBuilder.CreateTable(
                name: "Lot_TRN",
                columns: table => new
                {
                    LOTT_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LOTT_ItemID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, comment: "Item ID"),
                    LOTT_Nbr = table.Column<int>(type: "int", nullable: false),
                    LOTT_DateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date/Time"),
                    LOTT_Qty = table.Column<decimal>(type: "numeric(18,0)", nullable: false, comment: "Quantity"),
                    LOTT_Location = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false, comment: "Location"),
                    LOTT_Operation = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false, comment: "Operation"),
                    timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lot_TRN", x => x.LOTT_ID);
                });

            migrationBuilder.CreateTable(
                name: "Menu_Areas",
                columns: table => new
                {
                    SEC_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SEC_Area = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, comment: "Menu Area"),
                    timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu_Areas", x => x.SEC_ID);
                });

            migrationBuilder.CreateTable(
                name: "Menu_Groups",
                columns: table => new
                {
                    GRP_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GRP_Group = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, comment: "Group"),
                    GRP_MenuItem = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, comment: "Menu Item Group Hass Access To"),
                    timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu_Groups", x => x.GRP_ID);
                });

            migrationBuilder.CreateTable(
                name: "Menu_MenuItems",
                columns: table => new
                {
                    MNU_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MNU_MenuItemName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, comment: "Menu Item Name"),
                    MNU_MenuItemObject = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, comment: "Menu Item Object"),
                    MNU_Type = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    MNU_DisplayWhere = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, comment: "Display Where"),
                    timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu_MenuItems", x => x.MNU_ID);
                });

            migrationBuilder.CreateTable(
                name: "Menu_UsersGroups",
                columns: table => new
                {
                    USRGRP__ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USRGRP_User = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, comment: "User"),
                    USRGRP_Group = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, comment: "Group"),
                    timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu_UsersGroups", x => x.USRGRP__ID);
                });

            migrationBuilder.CreateTable(
                name: "Stock_Description",
                columns: table => new
                {
                    STKD_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STKD_ItemID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, comment: "Item ID"),
                    STKD_Desc = table.Column<string>(type: "varchar(512)", unicode: false, maxLength: 512, nullable: false, comment: "Description"),
                    STKD_LongDesc = table.Column<string>(type: "text", nullable: true, comment: "Long Description"),
                    timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock_Description", x => x.STKD_ID);
                });

            migrationBuilder.CreateTable(
                name: "Stock_Items",
                columns: table => new
                {
                    STKI_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STKI_ItemID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, comment: "Item ID"),
                    STKI_ProductFamily = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, comment: "Product Family"),
                    STKI_LocLot = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Loc/Lot Tracking?"),
                    STKI_UOM = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, comment: "Unit of Measure"),
                    STKI_Price = table.Column<decimal>(type: "money", nullable: false, comment: "Price"),
                    timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock_Items", x => x.STKI_ID);
                });

            migrationBuilder.CreateTable(
                name: "Stock_Loc",
                columns: table => new
                {
                    LOC_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LOC_ItemID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, comment: "Item ID"),
                    LOC_Location = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false, comment: "Location"),
                    LOC_Updated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date Updated"),
                    LOC_Qty = table.Column<decimal>(type: "numeric(18,0)", nullable: false, comment: "Quantity"),
                    LOC_NonNet = table.Column<bool>(type: "bit", nullable: false, comment: "Non Net?"),
                    LOC_Description = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock_Loc", x => x.LOC_ID);
                });

            migrationBuilder.CreateTable(
                name: "Stock_Lot",
                columns: table => new
                {
                    LOT_ID = table.Column<int>(type: "int", nullable: false, comment: "Lot Number")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LOT_ItemID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, comment: "Item ID"),
                    LOT_Nbr = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    LOT_Updated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "Date Updated"),
                    LOT_Qty = table.Column<decimal>(type: "numeric(18,0)", nullable: false, comment: "Quantity"),
                    LOT_NonNet = table.Column<bool>(type: "bit", nullable: false, comment: "Non Net?"),
                    LOT_Location = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false, comment: "Location"),
                    timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock_Lot", x => x.LOT_ID);
                });

            migrationBuilder.CreateTable(
                name: "Stock_Media",
                columns: table => new
                {
                    STKM_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STKM_ItemID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, comment: "Item ID"),
                    STKM_Path = table.Column<string>(type: "varchar(512)", unicode: false, maxLength: 512, nullable: false, comment: "Path To File"),
                    STKM_Type = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true, comment: "Type"),
                    timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock_Media", x => x.STKM_ID);
                });

            migrationBuilder.CreateTable(
                name: "Stock_ProductFamily",
                columns: table => new
                {
                    STKP_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STKP_ProductFamily = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false, comment: "Product Family"),
                    STKP_Desc = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false, comment: "Description"),
                    timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock_ProductFamily", x => x.STKP_ID);
                });

            migrationBuilder.CreateTable(
                name: "Stock_UOM",
                columns: table => new
                {
                    STKU_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STKU_Desc = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, comment: "Description"),
                    timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock_UOM", x => x.STKU_ID);
                });

            migrationBuilder.CreateTable(
                name: "Stock_Vendors",
                columns: table => new
                {
                    STKV_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STKV_VendorID = table.Column<int>(type: "int", nullable: false, comment: "Vendor ID"),
                    STKV_ItemID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, comment: "Item ID"),
                    STKV_Price = table.Column<decimal>(type: "money", nullable: false, comment: "Price"),
                    STKV_Preferred = table.Column<bool>(type: "bit", nullable: false, comment: "Preferred Vendor?"),
                    timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock_Vendors", x => x.STKV_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Find_FieldInfo");

            migrationBuilder.DropTable(
                name: "Find_Relations");

            migrationBuilder.DropTable(
                name: "Find_SearchField_Delete?");

            migrationBuilder.DropTable(
                name: "Loc_TRN");

            migrationBuilder.DropTable(
                name: "Login");

            migrationBuilder.DropTable(
                name: "Login_Current");

            migrationBuilder.DropTable(
                name: "Lot_TRN");

            migrationBuilder.DropTable(
                name: "Menu_Areas");

            migrationBuilder.DropTable(
                name: "Menu_Groups");

            migrationBuilder.DropTable(
                name: "Menu_MenuItems");

            migrationBuilder.DropTable(
                name: "Menu_UsersGroups");

            migrationBuilder.DropTable(
                name: "Stock_Description");

            migrationBuilder.DropTable(
                name: "Stock_Items");

            migrationBuilder.DropTable(
                name: "Stock_Loc");

            migrationBuilder.DropTable(
                name: "Stock_Lot");

            migrationBuilder.DropTable(
                name: "Stock_Media");

            migrationBuilder.DropTable(
                name: "Stock_ProductFamily");

            migrationBuilder.DropTable(
                name: "Stock_UOM");

            migrationBuilder.DropTable(
                name: "Stock_Vendors");
        }
    }
}
