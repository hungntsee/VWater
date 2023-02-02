using System;
using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class RolesCreateModelValidator
        : AbstractValidator<RolesCreateModel>
    {
        public RolesCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.RoleName).NotEmpty();
            #endregion
        }

    }
}
