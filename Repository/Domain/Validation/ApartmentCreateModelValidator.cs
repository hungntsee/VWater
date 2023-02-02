using System;
using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class ApartmentCreateModelValidator
        : AbstractValidator<ApartmentCreateModel>
    {
        public ApartmentCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.ApartmentName).NotEmpty();
            RuleFor(p => p.ApartmentName).MaximumLength(50);
            RuleFor(p => p.Address).NotEmpty();
            RuleFor(p => p.Address).MaximumLength(100);
            RuleFor(p => p.Note).MaximumLength(100);
            #endregion
        }

    }
}
