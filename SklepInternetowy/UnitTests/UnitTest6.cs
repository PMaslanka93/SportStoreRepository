using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Domain.Abstract;
using Domain.Entities;
using Web.Controllers;
using System.Web.Mvc;

namespace UnitTests
{
    [TestClass]
    public class UnitTest6
    {
        [TestMethod]
        public void Cannon_checkout_empty_cart()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();

            ShoppingDetails detail = new ShoppingDetails();

            CartController target = new CartController(null, mock.Object);

            ViewResult result = target.Checkout(cart, detail);

            mock.Verify(m => m.ProcessorOrder(It.IsAny<Cart>(),It.IsAny<ShoppingDetails>() ),Times.Never());

            Assert.AreEqual("", result.ViewName);

            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);

        }

        [TestMethod]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            CartController target = new CartController(null, mock.Object);
            target.ModelState.AddModelError("error", "error");
            ViewResult result = target.Checkout(cart, new ShoppingDetails());
            mock.Verify(m => m.ProcessorOrder(It.IsAny<Cart>(),It.IsAny<ShoppingDetails>() ),Times.Never());
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Cannotblabla()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            CartController target = new CartController(null, mock.Object);
            ViewResult result = target.Checkout(cart, new ShoppingDetails());
            mock.Verify(m => m.ProcessorOrder(It.IsAny<Cart>(), It.IsAny<ShoppingDetails>()), Times.Once());
            Assert.AreEqual("Completed", result.ViewName);
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);
        }
    }
}
