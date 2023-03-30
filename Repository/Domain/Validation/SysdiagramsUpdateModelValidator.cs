using System;
using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class SysdiagramsUpdateModelValidator
        : AbstractValidator<SysdiagramsUpdateModel>
    {
        public SysdiagramsUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MaximumLength(128);
            #endregion
        }

    }
}
