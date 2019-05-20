using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSIMediaTest.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Data.Entity;
using CSIMediaTest.Models;
using CSIMediaTest.DataContext;
using System.Web.Mvc;

namespace CSIMediaTest.Controllers.Tests
{
    [TestClass()]
    public class SequenceControllerTests
    {
        private Mock<DbSet<Sequence>> mockSet;
        private Mock<SequenceDBContext> mockContext;

        [TestInitialize]
        public void Init()
        {
            mockSet = new Mock<DbSet<Sequence>>();
            var sequence = new List<Sequence>
            {
                new Sequence {ID = 1, NewSequence = "1 2 3", Direction = Directions.Ascending, TimeTaken = 0.1}
            }.AsQueryable();

            mockSet.As<IQueryable<Sequence>>().Setup(m => m.Provider).Returns(sequence.Provider);
            mockSet.As<IQueryable<Sequence>>().Setup(m => m.Expression).Returns(sequence.Expression);
            mockSet.As<IQueryable<Sequence>>().Setup(m => m.ElementType).Returns(sequence.ElementType);
            mockSet.As<IQueryable<Sequence>>().Setup(m => m.GetEnumerator()).Returns(sequence.GetEnumerator());

            mockContext = new Mock<SequenceDBContext>();

            mockContext.Setup(x => x.Sequences).Returns(mockSet.Object);
        }

        [TestMethod()]
        public void OrderSequenceTest_AscendingOrder_ReturnAscendingOrder()
        {
            //Arrange
            var controller = new SequenceController();

            //Act
            var result = controller.OrderSequence("5 4 6 2 1", Directions.Ascending);

            //Assert
            Assert.AreEqual("1 2 4 5 6", result.Item2);
        }

        [TestMethod()]
        public void OrderSequenceTest_DescendingOrder_ReturnDescendingOrder()
        {
            //Arrange
            var controller = new SequenceController();

            //Act
            var result = controller.OrderSequence("5 4 6 2 1", Directions.Descending);

            //Assert
            Assert.AreEqual("6 5 4 2 1", result.Item2);
        }

        [TestMethod()]
        public void CreateTest_EnterSequence_ReturnSequenceList()
        {
            //Arrange          
            var controller = new SequenceController(mockContext.Object);

            //Act
            var result = (RedirectToRouteResult)controller.Create(new Sequence
            {
                NewSequence = "2 1 3",
                Direction = Directions.Ascending
            });

            //Assert
            Assert.IsTrue(result.RouteValues["action"].Equals("SequenceList"));
        }
    }
}