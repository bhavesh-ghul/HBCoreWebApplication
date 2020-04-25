using Domain.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IUserResository
    {
        /// <summary>
        /// Inserts the specified user profile.
        /// </summary>
        /// <param name="userProfile">The user profile.</param>
        Task<int> Insert(UserProfile userProfile);

        /// <summary>
        /// Checks the user.
        /// </summary>
        /// <param name="Username">The username.</param>
        /// <returns></returns>
        Boolean CheckUser(string Username);

        /// <summary>
        /// Logins the specified username.
        /// </summary>
        /// <param name="Username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        UserProfile Login(UserLogin userLogin);
    }
}
