using System;
using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class DistributorUpdateModelValidator
        : AbstractValidator<DistributorUpdateModel>
    {
        public DistributorUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.DistributorName).NotEmpty();
            RuleFor(p => p.DistributorName).MaximumLength(100);
            RuleFor(p => p.PhoneNumber).NotEmpty();
            RuleFor(p => p.PhoneNumber).MaximumLength(20);
            RuleFor(p => p.Address).NotEmpty();
            RuleFor(p => p.Address).MaximumLength(100);
            RuleFor(p => p.Payment).NotEmpty();
            RuleFor(p => p.Payment).MaximumLength(100);
            #endregion
        }

    }
}
