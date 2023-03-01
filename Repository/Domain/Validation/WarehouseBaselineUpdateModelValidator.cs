using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class WarehouseBaselineUpdateModelValidator
        : AbstractValidator<WarehouseBaselineUpdateModel>
    {
        public WarehouseBaselineUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Note).MaximumLength(100);
            #endregion
        }

    }
}
