using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class StoreCreateModelValidator
        : AbstractValidator<StoreCreateModel>
    {
        public StoreCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.StoreName).NotEmpty();
            RuleFor(p => p.StoreName).MaximumLength(100);
            RuleFor(p => p.Address).MaximumLength(100);
            RuleFor(p => p.Note).MaximumLength(100);
            #endregion
        }

    }
}
