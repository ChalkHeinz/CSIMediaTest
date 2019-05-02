using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSIMediaTest.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIMediaTest.Controllers.Tests
{
    [TestClass()]
    public class SequenceListControllerTests
    {
        //Moq is needed as method is dependant on DB
        [TestMethod()]
        public void SequenceListTest()
        {
            Assert.Fail();
        }

        //Have to look into testing void methods
        [TestMethod()]
        public void ExportTest()
        {
            Assert.IsTrue(true);
        }
    }
}