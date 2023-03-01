using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class BuildingUpdateModelValidator
        : AbstractValidator<BuildingUpdateModel>
    {
        public BuildingUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.BuildingName).NotEmpty();
            RuleFor(p => p.BuildingName).MaximumLength(100);
            RuleFor(p => p.Address).MaximumLength(100);
            RuleFor(p => p.Note).MaximumLength(100);
            #endregion
        }

    }
}
