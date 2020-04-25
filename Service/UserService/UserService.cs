using Domain.Common;
using Domain.User;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Helper;

namespace Service
{
    public class UserService : IUserService
    {
        /// <summary>
        /// The user resository
        /// </summary>
        private readonly IUserResository userResository;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public UserService(IOptions<ReadConfig> config)
        {
            userResository = new UserResository(config.Value.ConnectionString);
        }

        /// <summary>
        /// Inserts the user.
        /// </summary>
        /// <param name="userProfile">The user profile.</param>
        public async Task<int> InsertUser(UserProfile userProfile)
        {
            userProfile.salt = ComputPassword.GeneratePassword(15);
            userProfile.password = ComputPassword.EncodePassword(userProfile.password, userProfile.salt);
            return await userResository.Insert(userProfile);
        }

        /// <summary>
        /// Checks the user.
        /// </summary>
        /// <param name="Username">The username.</param>
        /// <returns></returns>
        public Boolean CheckUser(string Username)
        {
            return userResository.CheckUser(Username);
        }

        /// <summary>
        /// Logins the specified username.
        /// </summary>
        /// <param name="Username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public UserProfile Login(UserLogin userLogin)
        {
            return userResository.Login(userLogin);
        }

    }
}
