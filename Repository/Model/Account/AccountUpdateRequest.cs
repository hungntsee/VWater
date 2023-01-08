using System.ComponentModel.DataAnnotations;

namespace Repository.Model.Account
{
    public class AccountUpdateRequest
    {
        public int Id { get; set; }
        public string Username { get; set; }
        private string _password;
        [MinLength(6)]
        public string Password
        {
            get => _password;
            set => _password = replaceEmptyWithNull(value);
        }
        private string _confirmPassword;
        [Compare("Password")]
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => _confirmPassword = replaceEmptyWithNull(value);
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Gender { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string RoleName { get; set; }

        private string replaceEmptyWithNull(string value)
        {
            return string.IsNullOrEmpty(value) ? null : value;
        }
    }
}
