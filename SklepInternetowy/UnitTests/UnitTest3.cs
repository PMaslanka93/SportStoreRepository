using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class UnitTest3
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            Product product1 = new Product { ProductID = 1, Name = "P1" };
            Product product2 = new Product { ProductID = 2, Name = "P2" };
            Cart target = new Cart();
            target.AddItem(product1, 1);
            target.AddItem(product2, 1);

            CartLine[] results = target.Lines.ToArray();
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Product, product1);
            Assert.AreEqual(results[1].Product, product2);

        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            Product product1 = new Product { ProductID = 1, Name = "P1" };
            Product product2 = new Product { ProductID = 2, Name = "P2" };
            Cart target = new Cart();
            target.AddItem(product1, 1);
            target.AddItem(product2, 1);
            target.AddItem(product1, 10);
            target.AddItem(product2, 1);

            CartLine[] results = target.Lines.OrderBy(c => c.Product.ProductID).ToArray();

            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Quantity, 11);
            Assert.AreEqual(results[1].Quantity, 2);
        }

        [TestMethod]
        public void can_remove()
        {
            Product product1 = new Product { ProductID = 1, Name = "P1" };
            Product product2 = new Product { ProductID = 2, Name = "P2" };

            Cart target = new Cart();

            target.AddItem(product1, 1);
            target.AddItem(product2, 2);
            target.RemoveLine(product2);

            Assert.AreEqual(target.Lines.Where(x => x.Product == product2).Count(), 0);
            Assert.AreEqual(target.Lines.Count(), 1);
        }

        [TestMethod]
        public void Calculate_total()
        {
            Product p1 = new Product { ProductID = 1, Name = "p1" , Price = 100M};
            Product p2 = new Product { ProductID = 1, Name = "p2", Price = 200M };
            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 2);
            decimal result = target.CamputeTotalValue();
            Assert.AreEqual(result, 300M);
        }

        
    }
}
