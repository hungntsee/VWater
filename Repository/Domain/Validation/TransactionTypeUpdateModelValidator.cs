using System;
using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class TransactionTypeUpdateModelValidator
        : AbstractValidator<TransactionTypeUpdateModel>
    {
        public TransactionTypeUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.TransactionTypeName).MaximumLength(50);
            #endregion
        }

    }
}
