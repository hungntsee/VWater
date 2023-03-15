using System;
using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class StatusCreateModelValidator
        : AbstractValidator<StatusCreateModel>
    {
        public StatusCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.StatusName).NotEmpty();
            RuleFor(p => p.StatusName).MaximumLength(50);
            RuleFor(p => p.Note).MaximumLength(50);
            #endregion
        }

    }
}
