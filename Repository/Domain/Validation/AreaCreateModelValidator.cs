using System;
using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class AreaCreateModelValidator
        : AbstractValidator<AreaCreateModel>
    {
        public AreaCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.AreaName).NotEmpty();
            RuleFor(p => p.AreaName).MaximumLength(100);
            #endregion
        }

    }
}
