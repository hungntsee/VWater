using System;
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
            RuleFor(p => p.PhoneNumber).NotEmpty();
            RuleFor(p => p.PhoneNumber).MaximumLength(20);
            #endregion
        }

    }
}
