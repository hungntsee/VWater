namespace VWater.Data.Entities
{
    public partial class Account
    {
        public Account()
        {
            #region Generated Constructor
            Shippers = new HashSet<Shipper>();
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

        public string AccessToken { get; set; }

        public int RoleId { get; set; }

        #endregion

        #region Generated Relationships
        public virtual AccountRole RoleAccountRole { get; set; }

        public virtual ICollection<Shipper> Shippers { get; set; }

        #endregion

    }
}
