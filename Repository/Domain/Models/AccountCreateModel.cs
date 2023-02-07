using System;
using System.Collections.Generic;

namespace VWater.Domain.Models
{
    public partial class AccountCreateModel
    {
        #region Generated Properties
        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public int RoleId { get; set; }

        #endregion

    }
}
