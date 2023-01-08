using Repository.Entity;
using System.ComponentModel.DataAnnotations;

namespace Repository.Model.Account;

    public class AccountRespone
    {
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
        public string RoleName { get; set; }

        public AccountRespone(Entity.Account account, Account_Role role)
        {
            Username= account.Username;
            Password= account.Password;
            FirstName= account.FirstName;
            LastName= account.LastName;
            Gender= account.Gender;
            DateOfBirth= account.DateOfBirth;
            Address= account.Address;
            Email= account.Email;
            Access_token= account.Access_token;
            RoleName = role.RoleName;
        }
    }

