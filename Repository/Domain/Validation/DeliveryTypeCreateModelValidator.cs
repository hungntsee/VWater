using System;
using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class DeliveryTypeCreateModelValidator
        : AbstractValidator<DeliveryTypeCreateModel>
    {
        public DeliveryTypeCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.TypeName).NotEmpty();
            RuleFor(p => p.TypeName).MaximumLength(50);
            RuleFor(p => p.Description).NotEmpty();
            RuleFor(p => p.Description).MaximumLength(100);
            #endregion
        }

    }
}
