using System.ComponentModel.DataAnnotations;

namespace Repository.Model.Account
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Required!!!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Required!!!")]
        public string Password { get; set; }
    }
}
