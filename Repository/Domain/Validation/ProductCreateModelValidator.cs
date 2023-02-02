using System;
using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class ProductCreateModelValidator
        : AbstractValidator<ProductCreateModel>
    {
        public ProductCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).MaximumLength(100);
            RuleFor(p => p.Img).NotEmpty();
            RuleFor(p => p.Img).MaximumLength(100);
            RuleFor(p => p.Description).MaximumLength(100);
            #endregion
        }

    }
}
