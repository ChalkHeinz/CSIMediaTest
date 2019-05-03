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
    public class SequenceListControllerTests
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
        public void SequenceListTest_PassSequence_ReturnViewResult()
        {
            //Assign
            var controller = new SequenceListController(mockContext.Object);
            var sequence = new Sequence { NewSequence = "1 2 3", Direction = Directions.Ascending, TimeTaken = 0.2 };

            //Act
            var result = controller.SequenceList(sequence);

            //Assert           
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}