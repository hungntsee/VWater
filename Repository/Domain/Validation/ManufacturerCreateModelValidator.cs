using System;
using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class ManufacturerCreateModelValidator
        : AbstractValidator<ManufacturerCreateModel>
    {
        public ManufacturerCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.ManufacturerName).NotEmpty();
            RuleFor(p => p.ManufacturerName).MaximumLength(100);
            RuleFor(p => p.PhoneNumber).NotEmpty();
            RuleFor(p => p.PhoneNumber).MaximumLength(20);
            RuleFor(p => p.Address).NotEmpty();
            RuleFor(p => p.Address).MaximumLength(100);
            RuleFor(p => p.Note).MaximumLength(100);
            #endregion
        }

    }
}
