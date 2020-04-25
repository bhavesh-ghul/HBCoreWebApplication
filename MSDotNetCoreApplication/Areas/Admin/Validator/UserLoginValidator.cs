using Domain.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSDotNetCoreApplication.Areas.Admin.Validator
{
    public class UserLoginValidator : AbstractValidator<UserLogin>
    {
        public UserLoginValidator()
        {
            RuleFor(x => x.userName).NotEmpty().MinimumLength(4).MaximumLength(25);
            RuleFor(x => x.password).NotEmpty().MinimumLength(8).MaximumLength(18);
        }
    }
}
