using System;
using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class DeliverySlotUpdateModelValidator
        : AbstractValidator<DeliverySlotUpdateModel>
    {
        public DeliverySlotUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.SlotName).NotEmpty();
            RuleFor(p => p.SlotName).MaximumLength(50);
            #endregion
        }

    }
}
