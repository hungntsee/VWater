using System;
using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class CustomerUpdateModelValidator
        : AbstractValidator<CustomerUpdateModel>
    {
        public CustomerUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.FullName).NotEmpty();
            RuleFor(p => p.FullName).MaximumLength(100);
            RuleFor(p => p.PhoneNumber).MaximumLength(50);
            RuleFor(p => p.Note).MaximumLength(10);
            #endregion
        }

    }
}
