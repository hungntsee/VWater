using System;
using FluentValidation;
using Repository.Domain.Models;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class TransactionUpdateModelValidator
        : AbstractValidator<TransactionUpdateModel>
    {
        public TransactionUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Note).MaximumLength(100);
            #endregion
        }

    }
}
