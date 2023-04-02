using System;
using FluentValidation;
using Repository.Domain.Models;
using VWater.Domain.Models;

namespace VWater.Domain.Validation
{
    public partial class WalletUpdateModelValidator
        : AbstractValidator<WalletUpdateModel>
    {
        public WalletUpdateModelValidator()
        {
            #region Generated Constructor
            #endregion
        }

    }
}
