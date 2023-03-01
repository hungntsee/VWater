using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class GoodsExchangeNoteUpdateModelValidator
        : AbstractValidator<GoodsExchangeNoteUpdateModel>
    {
        public GoodsExchangeNoteUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Note).MaximumLength(100);
            #endregion
        }

    }
}
