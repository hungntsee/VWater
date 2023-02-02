using System;
using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class GoodsUpdateModelValidator
        : AbstractValidator<GoodsUpdateModel>
    {
        public GoodsUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.GoodsName).NotEmpty();
            RuleFor(p => p.GoodsName).MaximumLength(50);
            RuleFor(p => p.Img).MaximumLength(100);
            RuleFor(p => p.Volume).MaximumLength(50);
            RuleFor(p => p.Note).MaximumLength(100);
            #endregion
        }

    }
}
