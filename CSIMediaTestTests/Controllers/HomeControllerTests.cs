using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSIMediaTest.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSIMediaTest.Models;

namespace CSIMediaTest.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void OrderSequenceTest_AscendingOrder_ReturnsAscendingOrder()
        {
            //Arrange
            var sequence = "5 4 6 2 1";
            var newSequence = "1 2 4 5 6";
            var controller = new HomeController();

            //Act
            var result = controller.OrderSequence(sequence, Directions.Ascending);

            //Asert
            Assert.AreEqual(newSequence, result.Item2);
        }

        [TestMethod()]
        public void OrderSequenceTest_DescendingOrder_ReturnsDescendingOrder()
        {
            //Arrange
            var sequence = "5 4 6 2 1";
            var newSequence = "6 5 4 2 1";
            var controller = new HomeController();

            //Act
            var result = controller.OrderSequence(sequence, Directions.Descending);

            //Asert
            Assert.AreEqual(newSequence, result.Item2);
        }
    }
}