using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Account_Role",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Area",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Delivery_Type",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TypeLevel = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivery_Type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturer",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManufacturerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Img = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Accesstoken = table.Column<string>(name: "Access_token", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RoleId = table.Column<int>(name: "Role_Id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_account_account_role",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "Account_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Apartment",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApartmentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AreaId = table.Column<int>(name: "Area_Id", type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_apartment_area",
                        column: x => x.AreaId,
                        principalSchema: "dbo",
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Distributor",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistributorName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    Payment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AreaId = table.Column<int>(name: "Area_Id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distributor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_distributor_area",
                        column: x => x.AreaId,
                        principalSchema: "dbo",
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaId = table.Column<int>(name: "Area_Id", type: "int", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "date", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "date", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_menu_area",
                        column: x => x.AreaId,
                        principalSchema: "dbo",
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AreaId = table.Column<int>(name: "Area_Id", type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.Id);
                    table.ForeignKey(
                        name: "FK_store_area",
                        column: x => x.AreaId,
                        principalSchema: "dbo",
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Brand",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ManufactureId = table.Column<int>(name: "Manufacture_Id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                    table.ForeignKey(
                        name: "FK_brand_manufacture",
                        column: x => x.ManufactureId,
                        principalSchema: "dbo",
                        principalTable: "Manufacturer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Building",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuildingName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ApartmentId = table.Column<int>(name: "Apartment_Id", type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.Id);
                    table.ForeignKey(
                        name: "FK_building_apartment",
                        column: x => x.ApartmentId,
                        principalSchema: "dbo",
                        principalTable: "Apartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quotation",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValidFrom = table.Column<DateTime>(type: "date", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "date", nullable: true),
                    DistributorId = table.Column<int>(name: "Distributor_Id", type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_quotation_distributor",
                        column: x => x.DistributorId,
                        principalSchema: "dbo",
                        principalTable: "Distributor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_In_Menu",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuId = table.Column<int>(name: "Menu_Id", type: "int", nullable: false),
                    ProductId = table.Column<int>(name: "Product_Id", type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_In_Menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_in_menu_product",
                        column: x => x.ProductId,
                        principalSchema: "dbo",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_in_menu_product_menu",
                        column: x => x.MenuId,
                        principalSchema: "dbo",
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Delivery_Slot",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SlotName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    TimeFrom = table.Column<TimeSpan>(type: "time", nullable: false),
                    TimeTo = table.Column<TimeSpan>(type: "time", nullable: false),
                    StoreId = table.Column<int>(name: "Store_Id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivery_Slot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_delivery_slot_store",
                        column: x => x.StoreId,
                        principalSchema: "dbo",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Purchase_Order",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(name: "Store_Id", type: "int", nullable: false),
                    DistributorId = table.Column<int>(name: "Distributor_Id", type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "date", nullable: false),
                    TotalQuantity = table.Column<int>(type: "int", nullable: false),
                    ToatalPrice = table.Column<decimal>(type: "money", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Payment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_purchase_order_distributor",
                        column: x => x.DistributorId,
                        principalSchema: "dbo",
                        principalTable: "Distributor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchase_order_store",
                        column: x => x.StoreId,
                        principalSchema: "dbo",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shipper",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(name: "Account_Id", type: "int", nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StoreId = table.Column<int>(name: "Store_Id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipper", x => x.Id);
                    table.ForeignKey(
                        name: "FK_shipper_account",
                        column: x => x.AccountId,
                        principalSchema: "dbo",
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shipper_store",
                        column: x => x.StoreId,
                        principalSchema: "dbo",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StoreId = table.Column<int>(name: "Store_Id", type: "int", nullable: false),
                    AreaId = table.Column<int>(name: "Area_Id", type: "int", nullable: false),
                    Capacity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_warehouse_area",
                        column: x => x.AreaId,
                        principalSchema: "dbo",
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_warehouse_store",
                        column: x => x.StoreId,
                        principalSchema: "dbo",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goods",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoodsName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Img = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Volume = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BrandId = table.Column<int>(name: "Brand_Id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_goods_brand1",
                        column: x => x.BrandId,
                        principalSchema: "dbo",
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Delivery_Address",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(name: "Customer_Id", type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StoreId = table.Column<int>(name: "Store_Id", type: "int", nullable: false),
                    BuildingId = table.Column<int>(name: "Building_Id", type: "int", nullable: false),
                    DeliveryTypeId = table.Column<int>(name: "DeliveryType_Id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivery_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Delivery_Address_Customer",
                        column: x => x.CustomerId,
                        principalSchema: "dbo",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Delivery_Address_Delivery_Type",
                        column: x => x.DeliveryTypeId,
                        principalSchema: "dbo",
                        principalTable: "Delivery_Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_delivery_address_building",
                        column: x => x.BuildingId,
                        principalSchema: "dbo",
                        principalTable: "Building",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_delivery_address_store",
                        column: x => x.StoreId,
                        principalSchema: "dbo",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse_Baseline",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseId = table.Column<int>(name: "Warehouse_Id", type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    TotalQuantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse_Baseline", x => x.Id);
                    table.ForeignKey(
                        name: "FK_warehouse_baseline_warehouse",
                        column: x => x.WarehouseId,
                        principalSchema: "dbo",
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goods_Composition",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Img = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Volume = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GoodsId = table.Column<int>(name: "Goods_Id", type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods_Composition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_goods_composition_goods",
                        column: x => x.GoodsId,
                        principalSchema: "dbo",
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goods_In_Product",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(name: "Product_Id", type: "int", nullable: false),
                    GoodId = table.Column<int>(name: "Good_Id", type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods_In_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_good_in_product_goods",
                        column: x => x.GoodId,
                        principalSchema: "dbo",
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_good_in_product_product",
                        column: x => x.ProductId,
                        principalSchema: "dbo",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goods_In_Quotation",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuotationId = table.Column<int>(name: "Quotation_Id", type: "int", nullable: false),
                    GoodsId = table.Column<int>(name: "Goods_Id", type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods_In_Quotation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_goods_in_quotation_goods",
                        column: x => x.GoodsId,
                        principalSchema: "dbo",
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_goods_in_quotation_quotation",
                        column: x => x.QuotationId,
                        principalSchema: "dbo",
                        principalTable: "Quotation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Purchase_Order_Detail",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseOrderId = table.Column<int>(name: "PurchaseOrder_Id", type: "int", nullable: false),
                    GoodsId = table.Column<int>(name: "Goods_Id", type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase_Order_Detail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_purchase_order_detail_goods",
                        column: x => x.GoodsId,
                        principalSchema: "dbo",
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchase_order_detail_purchase_order",
                        column: x => x.PurchaseOrderId,
                        principalSchema: "dbo",
                        principalTable: "Purchase_Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryAddressId = table.Column<int>(name: "DeliveryAddress_Id", type: "int", nullable: false),
                    StoreId = table.Column<int>(name: "Store_Id", type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "date", nullable: false),
                    TotalQuantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "money", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    DeliverySlotId = table.Column<int>(name: "DeliverySlot_Id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_delivery_address",
                        column: x => x.DeliveryAddressId,
                        principalSchema: "dbo",
                        principalTable: "Delivery_Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_delivery_slot",
                        column: x => x.DeliverySlotId,
                        principalSchema: "dbo",
                        principalTable: "Delivery_Slot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_store",
                        column: x => x.StoreId,
                        principalSchema: "dbo",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goods_In_Baseline",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseBaselineId = table.Column<int>(name: "WarehouseBaseline_Id", type: "int", nullable: false),
                    GoodsId = table.Column<int>(name: "Goods_Id", type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods_In_Baseline", x => x.Id);
                    table.ForeignKey(
                        name: "FK_goods_in_baseline_goods",
                        column: x => x.GoodsId,
                        principalSchema: "dbo",
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_goods_in_baseline_warehouse_baseline",
                        column: x => x.WarehouseBaselineId,
                        principalSchema: "dbo",
                        principalTable: "Warehouse_Baseline",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goods_Exchange_Note",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseOrderId = table.Column<int>(name: "PurchaseOrder_Id", type: "int", nullable: false),
                    WarehouseId = table.Column<int>(name: "Warehouse_Id", type: "int", nullable: false),
                    NoteDate = table.Column<DateTime>(type: "date", nullable: false),
                    GoodsId = table.Column<int>(name: "Goods_Id", type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    OrderId = table.Column<int>(name: "Order_Id", type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods_Exchange_Note", x => x.Id);
                    table.ForeignKey(
                        name: "FK_goods_exchange_note_goods",
                        column: x => x.GoodsId,
                        principalSchema: "dbo",
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_goods_exchange_note_order",
                        column: x => x.OrderId,
                        principalSchema: "dbo",
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_goods_exchange_note_purchase_order",
                        column: x => x.PurchaseOrderId,
                        principalSchema: "dbo",
                        principalTable: "Purchase_Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_goods_exchange_note_warehouse",
                        column: x => x.WarehouseId,
                        principalSchema: "dbo",
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order_Detail",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(name: "Order_Id", type: "int", nullable: false),
                    ProductId = table.Column<int>(name: "Product_Id", type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Detail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_detail_order",
                        column: x => x.OrderId,
                        principalSchema: "dbo",
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_detail_product",
                        column: x => x.ProductId,
                        principalSchema: "dbo",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_Role_Id",
                schema: "dbo",
                table: "Account",
                column: "Role_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Apartment_Area_Id",
                schema: "dbo",
                table: "Apartment",
                column: "Area_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_Manufacture_Id",
                schema: "dbo",
                table: "Brand",
                column: "Manufacture_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Building_Apartment_Id",
                schema: "dbo",
                table: "Building",
                column: "Apartment_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_Address_Building_Id",
                schema: "dbo",
                table: "Delivery_Address",
                column: "Building_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_Address_Customer_Id",
                schema: "dbo",
                table: "Delivery_Address",
                column: "Customer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_Address_DeliveryType_Id",
                schema: "dbo",
                table: "Delivery_Address",
                column: "DeliveryType_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_Address_Store_Id",
                schema: "dbo",
                table: "Delivery_Address",
                column: "Store_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_Slot_Store_Id",
                schema: "dbo",
                table: "Delivery_Slot",
                column: "Store_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Distributor_Area_Id",
                schema: "dbo",
                table: "Distributor",
                column: "Area_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_Brand_Id",
                schema: "dbo",
                table: "Goods",
                column: "Brand_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_Composition_Goods_Id",
                schema: "dbo",
                table: "Goods_Composition",
                column: "Goods_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_Exchange_Note_Goods_Id",
                schema: "dbo",
                table: "Goods_Exchange_Note",
                column: "Goods_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_Exchange_Note_Order_Id",
                schema: "dbo",
                table: "Goods_Exchange_Note",
                column: "Order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_Exchange_Note_PurchaseOrder_Id",
                schema: "dbo",
                table: "Goods_Exchange_Note",
                column: "PurchaseOrder_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_Exchange_Note_Warehouse_Id",
                schema: "dbo",
                table: "Goods_Exchange_Note",
                column: "Warehouse_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_In_Baseline_Goods_Id",
                schema: "dbo",
                table: "Goods_In_Baseline",
                column: "Goods_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_In_Baseline_WarehouseBaseline_Id",
                schema: "dbo",
                table: "Goods_In_Baseline",
                column: "WarehouseBaseline_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_In_Product_Good_Id",
                schema: "dbo",
                table: "Goods_In_Product",
                column: "Good_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_In_Product_Product_Id",
                schema: "dbo",
                table: "Goods_In_Product",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_In_Quotation_Goods_Id",
                schema: "dbo",
                table: "Goods_In_Quotation",
                column: "Goods_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_In_Quotation_Quotation_Id",
                schema: "dbo",
                table: "Goods_In_Quotation",
                column: "Quotation_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_Area_Id",
                schema: "dbo",
                table: "Menu",
                column: "Area_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_DeliveryAddress_Id",
                schema: "dbo",
                table: "Order",
                column: "DeliveryAddress_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_DeliverySlot_Id",
                schema: "dbo",
                table: "Order",
                column: "DeliverySlot_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Store_Id",
                schema: "dbo",
                table: "Order",
                column: "Store_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Detail_Order_Id",
                schema: "dbo",
                table: "Order_Detail",
                column: "Order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Detail_Product_Id",
                schema: "dbo",
                table: "Order_Detail",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_In_Menu_Menu_Id",
                schema: "dbo",
                table: "Product_In_Menu",
                column: "Menu_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_In_Menu_Product_Id",
                schema: "dbo",
                table: "Product_In_Menu",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_Order_Distributor_Id",
                schema: "dbo",
                table: "Purchase_Order",
                column: "Distributor_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_Order_Store_Id",
                schema: "dbo",
                table: "Purchase_Order",
                column: "Store_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_Order_Detail_Goods_Id",
                schema: "dbo",
                table: "Purchase_Order_Detail",
                column: "Goods_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_Order_Detail_PurchaseOrder_Id",
                schema: "dbo",
                table: "Purchase_Order_Detail",
                column: "PurchaseOrder_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_Distributor_Id",
                schema: "dbo",
                table: "Quotation",
                column: "Distributor_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Shipper_Account_Id",
                schema: "dbo",
                table: "Shipper",
                column: "Account_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Shipper_Store_Id",
                schema: "dbo",
                table: "Shipper",
                column: "Store_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Store_Area_Id",
                schema: "dbo",
                table: "Store",
                column: "Area_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_Area_Id",
                schema: "dbo",
                table: "Warehouse",
                column: "Area_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_Store_Id",
                schema: "dbo",
                table: "Warehouse",
                column: "Store_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_Baseline_Warehouse_Id",
                schema: "dbo",
                table: "Warehouse_Baseline",
                column: "Warehouse_Id");*/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropTable(
                name: "Goods_Composition",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Goods_Exchange_Note",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Goods_In_Baseline",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Goods_In_Product",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Goods_In_Quotation",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Order_Detail",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Product_In_Menu",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Purchase_Order_Detail",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Shipper",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Warehouse_Baseline",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Quotation",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Menu",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Goods",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Purchase_Order",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Account",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Warehouse",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Delivery_Address",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Delivery_Slot",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Brand",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Distributor",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Account_Role",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Delivery_Type",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Building",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Store",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Manufacturer",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Apartment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Area",
                schema: "dbo");*/
        }
    }
}
