using System;
using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class WarehouseBaselineCreateModelValidator
        : AbstractValidator<WarehouseBaselineCreateModel>
    {
        public WarehouseBaselineCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Note).MaximumLength(100);
            #endregion
        }

    }
}
