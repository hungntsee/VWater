using System;
using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class GoodsInBaselineCreateModelValidator
        : AbstractValidator<GoodsInBaselineCreateModel>
    {
        public GoodsInBaselineCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Note).MaximumLength(100);
            #endregion
        }

    }
}
