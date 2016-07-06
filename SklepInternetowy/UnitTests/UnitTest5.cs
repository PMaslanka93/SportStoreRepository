using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Domain.Abstract;
using Domain.Entities;
using System.Linq;
using Web.Controllers;
using System.Web.Mvc;
using System.Web;
using Web.Models;


namespace UnitTests
{
    [TestClass]
    public class UnitTest5
    {
        [TestMethod]
        public void can_add()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "Apples"},
            }.AsQueryable());

            Cart cart = new Cart();
            CartController target = new CartController(mock.Object, null);
            target.AddToCart(cart, 1, null);
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductID, 1);

        }

        [TestMethod]
        public void Adding_Product_To_Cart_Goes_To_Cart_Screen()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "Apples"},
            }.AsQueryable());
            // przygotowanie — utworzenie koszyka
            Cart cart = new Cart();
            // przygotowanie — utworzenie kontrolera
            CartController target = new CartController(mock.Object, null);
            // działanie — dodanie produktu do koszyka
            RedirectToRouteResult result = target.AddToCart(cart, 2, "myUrl");
            // asercje
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");

        }

        [TestMethod]
        public void Can_View_Cart_Contents() 
        {
            Cart cart = new Cart();
            CartController controller = new CartController(null, null);
            CartIndexViewModel result = (CartIndexViewModel)controller.Index(cart, "myUrl").ViewData.Model;

            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        }

    }
}
