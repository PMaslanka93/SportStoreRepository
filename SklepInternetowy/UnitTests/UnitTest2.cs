using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Abstract;
using Moq;
using Domain.Entities;
using System.Linq;
using Web.Controllers;
using Web.Models;
namespace UnitTests
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void Generate_Category_Specific_Product_Count()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
                new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductID = 4, Name = "P4", Category = "Cat2"},
                new Product {ProductID = 5, Name = "P5", Category = "Cat3"},
                new Product {ProductID = 6, Name = "P6", Category = "Cat3"},
                new Product {ProductID = 7, Name = "P7", Category = "Cat3"},
            }.AsQueryable());
            
            ProductController target = new ProductController(mock.Object);
            target.PageSize = 3;
            int res1 = ((ProductsListViewModel)target.List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((ProductsListViewModel)target.List("Cat2").Model).PagingInfo.TotalItems;
            int res3 = ((ProductsListViewModel)target.List("Cat3").Model).PagingInfo.TotalItems;
            int resall = ((ProductsListViewModel)target.List(null).Model).PagingInfo.TotalItems;


            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 3);
            Assert.AreEqual(resall, 7);
        }
    }
}
