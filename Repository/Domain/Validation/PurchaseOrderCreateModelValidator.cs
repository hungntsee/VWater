using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class PurchaseOrderCreateModelValidator
        : AbstractValidator<PurchaseOrderCreateModel>
    {
        public PurchaseOrderCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Payment).MaximumLength(100);
            RuleFor(p => p.Note).MaximumLength(100);
            #endregion
        }

    }
}
