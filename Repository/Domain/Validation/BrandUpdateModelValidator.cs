using System;
using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class BrandUpdateModelValidator
        : AbstractValidator<BrandUpdateModel>
    {
        public BrandUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.BrandName).NotEmpty();
            RuleFor(p => p.BrandName).MaximumLength(50);
            RuleFor(p => p.Logo).MaximumLength(100);
            RuleFor(p => p.Origin).MaximumLength(100);
            #endregion
        }

    }
}