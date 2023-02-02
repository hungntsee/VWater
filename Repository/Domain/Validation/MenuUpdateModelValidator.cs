using System;
using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class MenuUpdateModelValidator
        : AbstractValidator<MenuUpdateModel>
    {
        public MenuUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Note).MaximumLength(100);
            #endregion
        }

    }
}
