using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class GoodsCompositionUpdateModelValidator
        : AbstractValidator<GoodsCompositionUpdateModel>
    {
        public GoodsCompositionUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MaximumLength(100);
            RuleFor(p => p.Img).MaximumLength(50);
            RuleFor(p => p.Volume).MaximumLength(50);
            RuleFor(p => p.Description).MaximumLength(100);
            #endregion
        }

    }
}
