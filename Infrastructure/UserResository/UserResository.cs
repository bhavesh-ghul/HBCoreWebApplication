using Dapper;
using Domain.User;
using Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UserResository : IUserResository
    {
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserResository"/> class.
        /// </summary>
        /// <param name="_connectionString">The connection string.</param>
        public UserResository(string _connectionString)
        {
            this.connectionString = _connectionString;
        }

        /// <summary>
        /// Inserts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public async Task<int> Insert(UserProfile model)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction sqltrans = con.BeginTransaction();
                var param = new DynamicParameters();
                param.Add("@action", "insert");
                param.Add("@userName", model.userName);
                param.Add("@password", model.password);
                param.Add("@salt", model.salt);
                param.Add("@email", model.email);
                param.Add("@phone", model.phone);
                var result = await con.ExecuteAsync("crud_User", param, sqltrans, 0, System.Data.CommandType.StoredProcedure);

                if (result > 0)
                {
                    sqltrans.Commit();
                }
                else
                {
                    sqltrans.Rollback();
                }
                return result;
            }
        }

        /// <summary>
        /// Checks the user.
        /// </summary>
        /// <param name="Username">The username.</param>
        /// <returns></returns>
        public Boolean CheckUser(string Username)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                var param = new DynamicParameters();
                param.Add("@userName", Username);
                param.Add("@action", "checkUser");
                return con.ExecuteScalar<Boolean>("crud_User", param, null, 0, System.Data.CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Logins the specified username.
        /// </summary>
        /// <param name="Username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public UserProfile Login(UserLogin userLogin)
        {
            UserProfile userProfile = new UserProfile();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //con.Open();
                var param = new DynamicParameters();
                param.Add("@userName", userLogin.userName);
                param.Add("@action", "userNameDetail");
                userProfile = con.Query<UserProfile>("crud_User", param, commandType: CommandType.StoredProcedure).SingleOrDefault();

                if(userProfile != null)
                {
                    var hashCode = userProfile.salt;
                    //Password Hasing Process Call Helper Class Method
                    var encodingPasswordString = ComputPassword.EncodePassword(userLogin.password, hashCode);
                    //Check Login Detail User Name Or Password

                    param = new DynamicParameters();
                    param.Add("@userName", userLogin.userName);
                    param.Add("@password", encodingPasswordString);
                    param.Add("@action", "login");
                    userProfile = con.Query<UserProfile>("crud_User", param, commandType: CommandType.StoredProcedure).SingleOrDefault();
                }
            }
            return userProfile;
        }
    }
}
