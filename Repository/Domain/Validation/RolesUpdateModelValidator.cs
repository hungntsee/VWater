using System;
using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class RolesUpdateModelValidator
        : AbstractValidator<RolesUpdateModel>
    {
        public RolesUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.RoleName).NotEmpty();
            #endregion
        }

    }
}
