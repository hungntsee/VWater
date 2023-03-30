using System;
using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class SysdiagramsCreateModelValidator
        : AbstractValidator<SysdiagramsCreateModel>
    {
        public SysdiagramsCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MaximumLength(128);
            #endregion
        }

    }
}
