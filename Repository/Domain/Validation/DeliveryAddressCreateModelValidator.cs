using System;
using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class DeliveryAddressCreateModelValidator
        : AbstractValidator<DeliveryAddressCreateModel>
    {
        public DeliveryAddressCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Address).NotEmpty();
            RuleFor(p => p.Address).MaximumLength(100);
            #endregion
        }

    }
}
