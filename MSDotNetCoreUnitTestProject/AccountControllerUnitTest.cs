using Domain.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MSDotNetCoreApplication.Controllers;
using MSDotNetCoreUnitTestProject.Utility;
using MSDotNetCoreUnitTestProject.Utility.Model;
using Service;
using System.Linq;

namespace MSDotNetCoreUnitTestProject
{
    [TestClass]
    public class AccountControllerUnitTest
    {
        #region Signup

        [TestMethod]
        public void SignupUnitTestMethod()
        {
            UserProfile upObj = new UserProfile();
            upObj.email = "mistryaalap@gmail.com";
            upObj.firstName = "Aalap";
            upObj.lastName = "Mistry";
            upObj.gender = 1;
            upObj.phone = "9974257234";
            upObj.state = "Gujarat";
            upObj.terms = true;
            upObj.userName = "mistryaalap";
            upObj.password = "abc@123";
            upObj.confirmPassword = "abc@123";

            // arrange
            Mock<IUserService> userService = new Mock<IUserService>();
            var accountController = new AccountController(userService.Object);

            // act
            Microsoft.AspNetCore.Mvc.IActionResult result = accountController.Signup(upObj);
            var okResult = result as Microsoft.AspNetCore.Mvc.IActionResult;

            // assert
            Assert.IsNotNull(okResult);
        }

        [TestMethod]
        public void RequiredFieldsValidation()
        {
            CheckPropertyValidation cpv = new CheckPropertyValidation();
            var up = new UserProfile
            {
                firstName = "Krishna",
                phone = "9974257234",
                gender = 1,
                email = "mistryaalap@gmail.com",
                userName = "aalaptest",
                terms = true,
                password = "aalap@123",
                confirmPassword = "aalap@123"
            };
            var errorcount = cpv.myValidation(up).Count();
            Assert.AreEqual(0, errorcount);
        }

        [TestMethod]
        public void VerifyEmailAddress_Format()
        {
            CheckPropertyValidation cpv = new CheckPropertyValidation();
            var up = new UserProfile
            {
                email = "abc@gmail.com",
            };
            var errorcount = cpv.IsValidEmail(up.email);
            Assert.AreEqual(true, errorcount);
        }

        /// <summary>
        /// Phone Number only allow 10 digit 
        /// </summary>
        [TestMethod]
        public void VerifyPhoneNumber_Length()
        {
            CheckPropertyValidation cpv = new CheckPropertyValidation();
            var up = new UserProfile
            {
                phone = "9974257234",
            };
            var errorcount = cpv.IsValidPhoneNumber(up.phone);
            Assert.AreEqual(true, errorcount);
        }

        /// <summary>
        /// 8 characters including 1 uppercase letter, 
        /// 1 special character, 
        /// alphanumeric characters
        /// </summary>
        [TestMethod]
        public void VerifyPassword_Format()
        {
            CheckPropertyValidation cpv = new CheckPropertyValidation();
            var up = new UserProfile
            {
                password = "apple@8A",
            };
            var errorcount = cpv.IsValidPassword(up.password);
            Assert.AreEqual(true, errorcount);
        }

        #endregion

        #region Login

        /// <summary>
        /// To verify all validation fields rrquired 
        /// </summary>
        [TestMethod]
        public void IsLoginFieldsRequired()
        {
            CheckPropertyValidation cpv = new CheckPropertyValidation();
            var up = new LoginUser
            {
                Email = "mistry@gmail.com",
                Password = "aalap@123",
            };
            var errorcount = cpv.myValidation(up).Count();
            Assert.AreEqual(0, errorcount);
        }

        /// <summary>
        /// Required fields should works
        /// </summary>
        [TestMethod]
        public void IsLoginRequiredFieldsValidationWorks()
        {
            CheckPropertyValidation cpv = new CheckPropertyValidation();
            var up = new LoginUser
            {
                Email = "",
                Password = "",
            };
            var errorcount = cpv.myValidation(up).Count();
            Assert.AreNotEqual(0, errorcount);
        }

        /// <summary>
        /// Is Login is Works with correct credential
        /// </summary>
        [TestMethod]
        public void IsLoginWithCorrectCredential()
        {
            UserProfile upObj = new UserProfile();
            upObj.email = "mistryaalap@gmail.com";
            upObj.password = "aalap@123";

            // arrange
            FakeAPI objFakeAPI = new FakeAPI();

            // act
            bool isLogin = objFakeAPI.IsValidUser(upObj.email, upObj.password);

            // assert
            Assert.IsTrue(isLogin);
        }


        /// <summary>
        /// Check User Not To Login With Wrong Credential
        /// </summary>
        [TestMethod]
        public void CheckUserNotToLoginWithWrongCredential()
        {
            UserProfile upObj = new UserProfile();
            upObj.email = "mistryaalap@gmail.com";
            upObj.password = "abc@123";

            // arrange
            FakeAPI objFakeAPI = new FakeAPI();

            // act
            bool isLogin = objFakeAPI.IsValidUser(upObj.email, upObj.password);

            // assert
            Assert.IsFalse(isLogin);
        }


        #endregion

        #region forgotPassword

        [TestMethod]
        public void IsEmailExists()
        {
            UserProfile upObj = new UserProfile();
            upObj.email = "mistryaalap@gmail.com";

            // arrange
            FakeAPI objFakeAPI = new FakeAPI();

            // act
            bool isLogin = objFakeAPI.IsEmailExists(upObj.email);

            // assert
            Assert.IsTrue(isLogin);
        }

        [TestMethod]
        public void IsChangePassword()
        {
            PendingPasswordChangeRequest ppObj = new PendingPasswordChangeRequest();
            ppObj.Email = "mistryaalap@gmail.com";

            // arrange
            FakeAPI objFakeAPI = new FakeAPI();

            // act
            bool isLogin = objFakeAPI.IsChangeRequestEmailExists(ppObj.Email);
            Assert.IsTrue(isLogin);

            // password change
            UserProfile upObj = new UserProfile();
            upObj.email = "mistryaalap@gmail.com";
            upObj.password = "aalaptest@123";
            bool result = objFakeAPI.SetNewPasswordByEmailId(upObj.email, upObj.password);

            // assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void IsChangePasswordWihtWorngEmailIdPasswordShouldNotToChange()
        {
            PendingPasswordChangeRequest ppObj = new PendingPasswordChangeRequest();
            ppObj.Email = "mistryaalap123@gmail.com";

            // arrange
            FakeAPI objFakeAPI = new FakeAPI();

            // act
            bool isLogin = objFakeAPI.IsChangeRequestEmailExists(ppObj.Email);
            Assert.IsFalse(isLogin);

            if (isLogin != false)
            {
                // password change
                UserProfile upObj = new UserProfile();
                upObj.email = "mistryaalap@gmail.com";
                upObj.password = "aalaptest@123";
                bool result = objFakeAPI.SetNewPasswordByEmailId(upObj.email, upObj.password);

                // assert
                Assert.IsTrue(result);
            }
           

        }

        #endregion

    }
}
