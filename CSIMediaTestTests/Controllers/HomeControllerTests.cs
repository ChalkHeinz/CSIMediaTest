﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
using CSIMediaTest.ViewModels;

namespace CSIMediaTest.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        private Mock<DbSet<Sequence>> mockSet;
        private Mock<SequenceDBContext> mockContext;

        [TestInitialize]
        public void Init()
        {
            mockSet = new Mock<DbSet<Sequence>>();
            mockContext = new Mock<SequenceDBContext>();
            mockContext.Setup(x => x.Sequences).Returns(mockSet.Object);
        }

        [TestMethod()]
        public void IndexTest_InvokeView_ReturnsView()
        {
            //Arrange
            var controller = new HomeController();
            var sequence = new Sequence { NewSequence = "3", Direction = Directions.Ascending };

            //Act
            var result = controller.Index(sequence) as ViewResult;

            //Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod()]
        public void CreateTest_EnterSequence_ReturnSequenceList()
        {
            //Arrange          
            var controller = new HomeController(mockContext.Object);
            var sequence = new Sequence { NewSequence = "2 1 3", Direction = Directions.Ascending };

            //Act
            var result = (RedirectToRouteResult)controller.Create(sequence);

            //Assert
            Assert.IsTrue(result.RouteValues["action"].Equals("SequenceList"));
        }

        [TestMethod()]
        public void CreateTest_InvalidSequence_ReturnIndex()
        {
            //Arrange 
            var controller = new HomeController();
            controller.ModelState.AddModelError("Incorrect Model", "Incorrect Model");

            //Act
            var result = (RedirectToRouteResult)controller.Create(new Sequence());

            //Assert
            Assert.IsTrue(result.RouteValues["action"].Equals("Index"));
        }

        [TestMethod()]
        public void OrderSequenceTest_AscendingOrder_ReturnAscendingOrder()
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
        public void OrderSequenceTest_DescendingOrder_ReturnDescendingOrder()
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