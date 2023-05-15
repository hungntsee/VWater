namespace VWater.Data.Entities
{
    public partial class Account
    {
        public Account()
        {
            #region Generated Constructor
            Transaction = new HashSet<Transaction>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string? AccessToken { get; set; }

        public int? StoreId { get; set; }

        public int RoleId { get; set; }

        public bool? IsActive { get; set; }

        #endregion

        #region Generated Relationships
        public virtual AccountRole RoleAccountRole { get; set; }
        public virtual Store? Store { get; set; }
        public virtual Shipper? Shipper{ get; set;}
        public virtual ICollection<Transaction> Transaction { get; set; }
        #endregion

    }
}
