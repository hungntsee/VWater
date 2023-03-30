using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class WarehouseCreateModelValidator
        : AbstractValidator<WarehouseCreateModel>
    {
        public WarehouseCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.WarehouseName).NotEmpty();
            RuleFor(p => p.WarehouseName).MaximumLength(100);
            RuleFor(p => p.Capacity).MaximumLength(50);
            #endregion
        }

    }
}
