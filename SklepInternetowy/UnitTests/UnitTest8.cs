using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Web.Infrastructure.Abstract;
using Web.Models;
using Web.Controllers;
using System.Web.Mvc;

namespace UnitTests
{
    [TestClass]
    public class UnitTest8
    {
        
        [TestMethod]
        public void Cannot_Login_With_Invalid_Credentials()
        {
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "blaaaa")).Returns(false);
            // przygotowanie — utworzenie modelu widoku
            LoginViewModel model = new LoginViewModel
            {
                UserName = "adblmin",
                Password = "adfs"
            };
            AccountController target = new AccountController(mock.Object);
            // działanie — uwierzytelnienie z użyciem nieprawidłowych danych
            ActionResult result = target.Login(model, "/MyURL");
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);

        }
    }
}
