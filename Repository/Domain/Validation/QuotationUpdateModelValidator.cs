using System;
using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class QuotationUpdateModelValidator
        : AbstractValidator<QuotationUpdateModel>
    {
        public QuotationUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Note).MaximumLength(100);
            #endregion
        }

    }
}
