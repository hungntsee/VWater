using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class DeliveryAddressUpdateModelValidator
        : AbstractValidator<DeliveryAddressUpdateModel>
    {
        public DeliveryAddressUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Address).NotEmpty();
            RuleFor(p => p.Address).MaximumLength(100);
            #endregion
        }

    }
}
