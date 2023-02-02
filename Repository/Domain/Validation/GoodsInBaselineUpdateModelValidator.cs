using System;
using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class GoodsInBaselineUpdateModelValidator
        : AbstractValidator<GoodsInBaselineUpdateModel>
    {
        public GoodsInBaselineUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Note).MaximumLength(100);
            #endregion
        }

    }
}
