using System.ComponentModel.DataAnnotations;

namespace Repository.Entity
{
    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Gender { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; } 
        public string Address { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Access_token { get; set; }

        public int? Role_Id{ get; set; }

        // Navigation property
        public virtual Account_Role Role { get; set; }
    }
}
