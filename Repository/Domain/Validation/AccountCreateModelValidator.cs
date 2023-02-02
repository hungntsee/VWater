using System;
using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class AccountCreateModelValidator
        : AbstractValidator<AccountCreateModel>
    {
        public AccountCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Username).NotEmpty();
            RuleFor(p => p.Username).MaximumLength(50);
            RuleFor(p => p.Password).NotEmpty();
            RuleFor(p => p.Password).MaximumLength(100);
            RuleFor(p => p.FirstName).NotEmpty();
            RuleFor(p => p.FirstName).MaximumLength(20);
            RuleFor(p => p.LastName).NotEmpty();
            RuleFor(p => p.LastName).MaximumLength(20);
            RuleFor(p => p.Address).NotEmpty();
            RuleFor(p => p.Address).MaximumLength(100);
            RuleFor(p => p.Email).NotEmpty();
            RuleFor(p => p.Email).MaximumLength(100);
            RuleFor(p => p.AccessToken).MaximumLength(100);
            #endregion
        }

    }
}
