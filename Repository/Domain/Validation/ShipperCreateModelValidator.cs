using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class ShipperCreateModelValidator
        : AbstractValidator<ShipperCreateModel>
    {
        public ShipperCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.PhoneNumber).NotEmpty();
            RuleFor(p => p.PhoneNumber).MaximumLength(20);
            #endregion
        }

    }
}
