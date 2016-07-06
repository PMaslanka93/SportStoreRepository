using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Domain.Abstract;
using Domain.Entities;
using System.Linq;
using Web.Controllers;
using System.Collections.Generic;
using System.Web.Mvc;

namespace UnitTests
{
    [TestClass]
    public class AdminTest
    {
        [TestMethod]
        public void Inted_conteins_all_prod()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]{
                new Product{ProductID = 1, Name = "N1"},
                new Product{ProductID = 2, Name = "N2"},
                new Product{ProductID = 3, Name="N3"},

            }.AsQueryable());

            AdminController target = new AdminController(mock.Object);
            Product[] result = ((IEnumerable<Product>)target.Index().ViewData.Model).ToArray();

            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual(result[0].ProductID, 1);
            Assert.AreEqual(result[1].ProductID, 2);
            Assert.AreEqual(result[2].ProductID, 3);
        }


        [TestMethod]
        public void is_product_edited()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]{
                new Product{ProductID = 1, Name = "N1"},
                new Product{ProductID = 2, Name = "N2"},
                new Product{ProductID = 3, Name="N3"},

            }.AsQueryable());

            AdminController target = new AdminController(mock.Object);

            Product p1 = target.Edit(1).ViewData.Model as Product;
            Product p2 = target.Edit(2).ViewData.Model as Product;
            Product p3 = target.Edit(3).ViewData.Model as Product;
            Product p4 = (Product)target.Edit(4).ViewData.Model;

            Assert.AreEqual(p1.ProductID, 1);
            Assert.AreEqual(p2.ProductID, 2);
            Assert.AreEqual(p3.ProductID, 3);
            Assert.IsNotNull(p4);
        }

        [TestMethod]
        public void Save_changes()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            AdminController target = new AdminController(mock.Object);
            Product product = new Product { Name = "test" };
            ActionResult result = target.Edit(product, null);
            mock.Verify(m => m.SaveProduct(product));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            AdminController target = new AdminController(mock.Object);
            Product product = new Product { Name = "test" };
            target.ModelState.AddModelError("error", "error");
            ActionResult result = target.Edit(product, null);
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());
            Assert.IsInstanceOfType(result, typeof(ViewResult));


        }

        [TestMethod]
        public void Can_Delete_Valid_Products()
        {
            Product prod = new Product { ProductID = 2, Name = "Test" };
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]{
                new Product {ProductID = 1, Name = "P1"},
                prod,
                new Product {ProductID = 3, Name = "P3"},
            }.AsQueryable());

            AdminController target = new AdminController(mock.Object);
            target.Delete(prod.ProductID);
            mock.Verify(m => m.DeleteProduct(prod.ProductID));
        }
    }
}
