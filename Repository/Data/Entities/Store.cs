namespace VWater.Data.Entities
{
    public partial class Store
    {
        public Store()
        {
            #region Generated Constructor
            Accounts = new HashSet<Account>();
            DeliveryAddresses = new HashSet<DeliveryAddress>();
            DeliverySlots = new HashSet<DeliverySlot>();
            Orders = new HashSet<Order>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
            Warehouses = new HashSet<Warehouse>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public string StoreName { get; set; }

        public int AreaId { get; set; }

        public string Address { get; set; }

        public int? Status { get; set; }

        public string Note { get; set; }

        #endregion

        #region Generated Relationships
        public virtual Area Area { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }

        public virtual ICollection<DeliveryAddress> DeliveryAddresses { get; set; }

        public virtual ICollection<DeliverySlot> DeliverySlots { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }

        public virtual ICollection<Warehouse> Warehouses { get; set; }

        #endregion

    }
}
