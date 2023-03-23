using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class CustomerCreateModelValidator
        : AbstractValidator<CustomerCreateModel>
    {
        public CustomerCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.FullName).NotEmpty();
            RuleFor(p => p.FullName).MaximumLength(100);
            RuleFor(p => p.PhoneNumber).MaximumLength(12);
            #endregion
        }

    }
}
