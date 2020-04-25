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
        public void VerifyEmailAddress()
        {
            CheckPropertyValidation cpv = new CheckPropertyValidation();
            var up = new UserProfile
            {
                email = "abc@gmail.com",
            };
            var errorcount = cpv.IsValidEmail(up.email);
            Assert.AreEqual(true, errorcount);
        }

        [TestMethod]
        public void VerifyPhoneNumber()
        {
            CheckPropertyValidation cpv = new CheckPropertyValidation();
            var up = new UserProfile
            {
                phone = "9904092555",
            };
            var errorcount = cpv.IsValidPhoneNumber(up.phone);
            Assert.AreEqual(true, errorcount);
        }

        #endregion

        #region Login

        [TestMethod]
        public void IsLoginFieldsRequired()
        {
            CheckPropertyValidation cpv = new CheckPropertyValidation();
            var up = new LoginUser
            {
                Email = "mistryaalap@gmail.com",
                Password = "aalap@123",
            };
            var errorcount = cpv.myValidation(up).Count();
            Assert.AreEqual(0, errorcount);
        }

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

        [TestMethod]
        public void IsLoginWithWrongCredential()
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
            bool result= objFakeAPI.SetNewPasswordByEmailId(upObj.email, upObj.password);

            // assert
            Assert.IsTrue(result);

        }

        #endregion

    }
}
