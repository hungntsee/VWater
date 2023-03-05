using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class ProductTypeUpdateModelValidator
        : AbstractValidator<ProductTypeUpdateModel>
    {
        public ProductTypeUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.ProductTypeName).NotEmpty();
            RuleFor(p => p.ProductTypeName).MaximumLength(20);
            RuleFor(p => p.Url).MaximumLength(100);
            #endregion
        }

    }
}
