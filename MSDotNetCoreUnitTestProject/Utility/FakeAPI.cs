using Domain.User;
using MSDotNetCoreUnitTestProject.Utility.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MSDotNetCoreUnitTestProject.Utility
{
    public class FakeAPI
    {
        /// <summary>
        /// GetAllUserProfile
        /// </summary>
        /// <returns></returns>
        public List<UserProfile> GetAllUserProfile()
        {
            List<UserProfile> userProfileList = new List<UserProfile>();
            userProfileList.Add(new UserProfile
            {
                firstName = "Aalap",
                lastName = "Mistry",
                phone = "9974257234",
                gender = 1,
                email = "mistryaalap@gmail.com",
                userName = "aalaptest",
                terms = true,
                password = "aalap@123",
                confirmPassword = "aalap@123"
            });

            userProfileList.Add(new UserProfile
            {
                firstName = "Manthan",
                lastName = "Patel",
                phone = "9858745874",
                gender = 1,
                email = "patelmanthan@gmail.com",
                userName = "manthantest",
                terms = true,
                password = "manthan@123",
                confirmPassword = "manthan@123"
            });

            userProfileList.Add(new UserProfile
            {
                firstName = "Harsh",
                lastName = "Mistry",
                phone = "9858745858",
                gender = 1,
                email = "mistryHarsh@gmail.com",
                userName = "Harshtest",
                terms = true,
                password = "Harsh@123",
                confirmPassword = "Harsh@123"
            });

            userProfileList.Add(new UserProfile
            {
                firstName = "Jaymin",
                lastName = "Thakkar",
                phone = "9685742585",
                gender = 1,
                email = "jayminthakkar@gmail.com",
                userName = "jaymintest",
                terms = true,
                password = "jaymin@123",
                confirmPassword = "jaymin@123"
            });


            return userProfileList;
        }

        /// <summary>
        /// IsValidUser
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public bool IsValidUser(string email, string pass)
        {
            List<UserProfile> userList = GetAllUserProfile();
            int cnt= userList.Where(x => x.email == email && x.password == pass).ToList().Count;
            return cnt > 0 ? true : false;
        }

        /// <summary>
        /// IsEmailExists
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsEmailExists(string email)
        {
            List<UserProfile> userList = GetAllUserProfile();
            int cnt = userList.Where(x => x.email == email).ToList().Count;
            return cnt > 0 ? true : false;
        }

        /// <summary>
        /// GetAllPendingPasswordChange
        /// </summary>
        /// <returns></returns>
        public List<PendingPasswordChangeRequest> GetAllPendingPasswordChange()
        {
            List<PendingPasswordChangeRequest> pendingPasswordChangeRequestList = new List<PendingPasswordChangeRequest>();
            pendingPasswordChangeRequestList.Add(new PendingPasswordChangeRequest
            {
                Email = "mistryaalap@gmail.com",
                IsActive=true
            });

            pendingPasswordChangeRequestList.Add(new PendingPasswordChangeRequest
            {
                Email = "patelmanthan@gmail.com",
                IsActive=false
            });
           

            return pendingPasswordChangeRequestList;
        }

        /// <summary>
        /// IsChangeRequestEmailExists
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsChangeRequestEmailExists(string email)
        {
            List<PendingPasswordChangeRequest> userList = GetAllPendingPasswordChange();
            int cnt = userList.Where(x => x.Email == email).ToList().Count;
            return cnt > 0 ? true : false;
        }

        /// <summary>
        /// SetNewPasswordByEmailId
        /// </summary>
        public bool SetNewPasswordByEmailId(string email,string newPassword)
        {
            List<UserProfile> userList = GetAllUserProfile();
            List<UserProfile> modifyuserList = userList.Where(x => x.email == email).ToList();
            modifyuserList[0].password = newPassword;
            return modifyuserList.Count > 0 ? true : false;
        }
    }
}
