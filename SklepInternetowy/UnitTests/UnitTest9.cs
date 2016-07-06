using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Controllers;
using Domain.Entities;
using Domain.Abstract;
using System.Linq;
using Moq;
using System.Web.Mvc;
namespace UnitTests
{
    [TestClass]
    public class UnitTest9
    {
        [TestMethod]
        public void Can_Retrieve_Image_Data()
        {
            Product prod = new Product
            {
                ProductID = 2,
                Name = "Test",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };
            // przygotowanie — tworzenie imitacji repozytorium
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                prod,
                new Product {ProductID = 3, Name = "P3"}
            }.AsQueryable());
            // przygotowanie — tworzenie kontrolera
            ProductController target = new ProductController(mock.Object);
            ActionResult result = target.GetImage(2);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(prod.ImageMimeType, ((FileResult)result).ContentType);
        }

        [TestMethod]
        public void Cannot_Retrieve_Image_Data_For_Invalid_ID()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"}
            }.AsQueryable());
            // przygotowanie — tworzenie kontrolera
            ProductController target = new ProductController(mock.Object);
            ActionResult result = target.GetImage(100);
            // asercje
            Assert.IsNull(result);
        }
    }
}
