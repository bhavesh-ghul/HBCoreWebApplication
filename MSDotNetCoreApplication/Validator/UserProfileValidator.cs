using Domain.Common;
using Domain.User;
using FluentValidation;
using Microsoft.Extensions.Options;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSDotNetCoreApplication.Validator
{
    public class UserProfileValidator : AbstractValidator<UserProfile>
    {
        public UserProfileValidator()
        {
            RuleFor(x => x.id).NotNull();
            RuleFor(x => x.userName).NotEmpty().MinimumLength(4).MaximumLength(25);
            RuleFor(x => x.password).NotEmpty().MinimumLength(8).MaximumLength(18);
            RuleFor(x => x.confirmPassword).NotEmpty().MinimumLength(8).MaximumLength(18).Equal(x => x.password);
            RuleFor(x => x.email).NotEmpty().EmailAddress();
            RuleFor(x => x.phone).NotEmpty().MinimumLength(10).MaximumLength(15);
            RuleFor(x => x.terms).Equal(x => true).WithMessage("You must accept our terms and conditioin!");
        }
    }
}
