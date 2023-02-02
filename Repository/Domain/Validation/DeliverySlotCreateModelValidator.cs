using System;
using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class DeliverySlotCreateModelValidator
        : AbstractValidator<DeliverySlotCreateModel>
    {
        public DeliverySlotCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.SlotName).NotEmpty();
            RuleFor(p => p.SlotName).MaximumLength(50);
            #endregion
        }

    }
}
