using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.User
{
    public class UserLogin
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Display(Name = "Username")]
        public string userName { get; set; } = "";
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Display(Name = "Password")]
        public string password { get; set; } = "";
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="UserLogin"/> is rememberme.
        /// </summary>
        /// <value>
        ///   <c>true</c> if rememberme; otherwise, <c>false</c>.
        /// </value>
        public bool rememberme { get; set; }
    }
}
