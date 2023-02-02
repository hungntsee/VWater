using System;
using FluentValidation;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class GoodsExchangeNoteCreateModelValidator
        : AbstractValidator<GoodsExchangeNoteCreateModel>
    {
        public GoodsExchangeNoteCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Note).MaximumLength(100);
            #endregion
        }

    }
}
