using System;
using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class TransactionTypeCreateModelValidator
        : AbstractValidator<TransactionTypeCreateModel>
    {
        public TransactionTypeCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.TransactionTypeName).MaximumLength(50);
            #endregion
        }

    }
}
