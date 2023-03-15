using Microsoft.EntityFrameworkCore;

namespace VWater.Data
{
    public partial class VWaterContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public VWaterContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("VWaterDatabase"));

        }

        #region Generated Properties
        public virtual DbSet<VWater.Data.Entities.AccountRole> AccountRoles { get; set; }

        public virtual DbSet<VWater.Data.Entities.Account> Accounts { get; set; }

        public virtual DbSet<VWater.Data.Entities.Apartment> Apartments { get; set; }

        public virtual DbSet<VWater.Data.Entities.Area> Areas { get; set; }

        public virtual DbSet<VWater.Data.Entities.Brand> Brands { get; set; }

        public virtual DbSet<VWater.Data.Entities.Building> Buildings { get; set; }

        public virtual DbSet<VWater.Data.Entities.Customer> Customers { get; set; }

        public virtual DbSet<VWater.Data.Entities.DeliveryAddress> DeliveryAddresses { get; set; }

        public virtual DbSet<VWater.Data.Entities.DeliverySlot> DeliverySlots { get; set; }

        public virtual DbSet<VWater.Data.Entities.DeliveryType> DeliveryTypes { get; set; }

        public virtual DbSet<VWater.Data.Entities.Distributor> Distributors { get; set; }

        public virtual DbSet<VWater.Data.Entities.Goods> Goods { get; set; }

        public virtual DbSet<VWater.Data.Entities.GoodsComposition> GoodsCompositions { get; set; }

        public virtual DbSet<VWater.Data.Entities.GoodsExchangeNote> GoodsExchangeNotes { get; set; }

        public virtual DbSet<VWater.Data.Entities.GoodsInBaseline> GoodsInBaselines { get; set; }

        public virtual DbSet<VWater.Data.Entities.GoodsInProduct> GoodsInProducts { get; set; }

        public virtual DbSet<VWater.Data.Entities.GoodsInQuotation> GoodsInQuotations { get; set; }

        public virtual DbSet<VWater.Data.Entities.Manufacturer> Manufacturers { get; set; }

        public virtual DbSet<VWater.Data.Entities.Menu> Menus { get; set; }

        public virtual DbSet<VWater.Data.Entities.OrderDetail> OrderDetails { get; set; }

        public virtual DbSet<VWater.Data.Entities.Order> Orders { get; set; }

        public virtual DbSet<VWater.Data.Entities.ProductInMenu> ProductInMenus { get; set; }

        public virtual DbSet<VWater.Data.Entities.Product> Products { get; set; }

        public virtual DbSet<VWater.Data.Entities.ProductType> ProductTypes { get; set; }

        public virtual DbSet<VWater.Data.Entities.PurchaseOrderDetail> PurchaseOrderDetails { get; set; }

        public virtual DbSet<VWater.Data.Entities.PurchaseOrder> PurchaseOrders { get; set; }

        public virtual DbSet<VWater.Data.Entities.Quotation> Quotations { get; set; }

        public virtual DbSet<VWater.Data.Entities.Shipper> Shippers { get; set; }

        public virtual DbSet<VWater.Data.Entities.Status> Statuses { get; set; }

        public virtual DbSet<VWater.Data.Entities.Store> Stores { get; set; }

        public virtual DbSet<VWater.Data.Entities.WarehouseBaseline> WarehouseBaselines { get; set; }

        public virtual DbSet<VWater.Data.Entities.Warehouse> Warehouses { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Generated Configuration
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.AccountMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.AccountRoleMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.ApartmentMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.AreaMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.BrandMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.BuildingMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.CustomerMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.DeliveryAddressMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.DeliverySlotMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.DeliveryTypeMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.DistributorMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.GoodsCompositionMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.GoodsExchangeNoteMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.GoodsInBaselineMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.GoodsInProductMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.GoodsInQuotationMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.GoodsMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.ManufacturerMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.MenuMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.OrderDetailMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.OrderMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.ProductInMenuMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.ProductMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.ProductTypeMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.PurchaseOrderDetailMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.PurchaseOrderMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.QuotationMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.ShipperMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.StatusMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.StoreMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.WarehouseBaselineMap());
            modelBuilder.ApplyConfiguration(new VWater.Data.Mapping.WarehouseMap());
            #endregion
        }
    }
}
