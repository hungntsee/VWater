namespace VWater.Data.Entities
{
    public partial class AccountRole
    {
        public AccountRole()
        {
            #region Generated Constructor
            RoleAccounts = new HashSet<Account>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public string RoleName { get; set; }

        #endregion

        #region Generated Relationships
        public virtual ICollection<Account> RoleAccounts { get; set; }

        #endregion

    }
}
