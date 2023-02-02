using System;
using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class AreaUpdateModelValidator
        : AbstractValidator<AreaUpdateModel>
    {
        public AreaUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.AreaName).NotEmpty();
            RuleFor(p => p.AreaName).MaximumLength(100);
            #endregion
        }

    }
}
