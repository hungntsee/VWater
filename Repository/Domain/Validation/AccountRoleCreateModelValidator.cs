using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class AccountRoleCreateModelValidator
        : AbstractValidator<AccountRoleCreateModel>
    {
        public AccountRoleCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.RoleName).NotEmpty();
            RuleFor(p => p.RoleName).MaximumLength(20);
            #endregion
        }

    }
}
