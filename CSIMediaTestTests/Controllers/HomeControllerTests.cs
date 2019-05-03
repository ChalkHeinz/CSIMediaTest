using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSIMediaTest.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSIMediaTest.Models;
using System.Web.Mvc;
using Moq;
using CSIMediaTest.DataContext;
using System.Data.Entity;

namespace CSIMediaTest.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {        
        [TestMethod()]
        public void IndexTest_InvokeView_ReturnsView()
        {
            //Arrange
            var controller = new HomeController();
            var sequence = new Sequence { NewSequence = "d3", Direction = Directions.Ascending };

            //Act
            var result = controller.Index(sequence) as ViewResult;

            //Assert
            Assert.AreEqual("", result.ViewName);
        }

        //Moq needs to be looked into before further work
        [TestMethod()]
        public void CreateTest()
        {
            //Arrange 
            var mockSet = new Mock<DbSet<Sequence>>();
            var mockContext = new Mock<SequenceDBContext>();
            mockContext.Setup(x => x.Sequences).Returns(mockSet.Object);

            var controller = new HomeController(mockContext.Object);
            var sequence = new Sequence { NewSequence = "2 1 3", Direction = Directions.Ascending };

            //Act
            var viewResult = controller.Create(sequence);

            //Assert
            Assert.IsInstanceOfType(viewResult, typeof(RedirectToRouteResult));
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

            //Assert
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

            //Assert
            Assert.AreEqual(newSequence, result.Item2);
        }
    }
}