using Domain.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IUserService
    {
        /// <summary>
        /// Inserts the user.
        /// </summary>
        /// <param name="user">The user.</param>
        Task<int> InsertUser(UserProfile user);

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
