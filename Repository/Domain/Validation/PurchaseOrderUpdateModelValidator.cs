using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class PurchaseOrderUpdateModelValidator
        : AbstractValidator<PurchaseOrderUpdateModel>
    {
        public PurchaseOrderUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Payment).MaximumLength(100);
            RuleFor(p => p.Note).MaximumLength(100);
            #endregion
        }

    }
}
