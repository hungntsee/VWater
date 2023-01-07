namespace Repository.Entity
{
    public class Account_Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        // Navigation property
        public virtual ICollection<Account> Accounts { get; private set; }
    }
}
