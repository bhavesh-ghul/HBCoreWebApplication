using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSDotNetCoreUnitTestProject.Utility.Model
{
    class LoginUser
    {
        [Required(ErrorMessage = "userName id should not be Empty")]
        public string Email { get; set; }

        [Required(ErrorMessage = "password should not be Empty")]
        public string Password { get; set; }
    }
}
