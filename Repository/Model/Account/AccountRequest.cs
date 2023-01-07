
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Repository.Model.Account
{
    public class AccountRequest
    {
        [Required(ErrorMessage = "Required!!!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Required!!!")]
        public string Password { get; set; }
    }
}
