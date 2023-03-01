using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class AccountUpdateModelValidator
        : AbstractValidator<AccountUpdateModel>
    {
        public AccountUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Username).MaximumLength(50);
            RuleFor(p => p.Password).MaximumLength(100);
            RuleFor(p => p.FirstName).MaximumLength(20);
            RuleFor(p => p.LastName).MaximumLength(20);
            RuleFor(p => p.Address).MaximumLength(100);
            RuleFor(p => p.Email).MaximumLength(100);
            #endregion
        }

    }
}
