using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using NASA.MarsRover.VicRoads.Main.processors;

namespace NASA.MarsRover.VicRoads.Test
{
    [TestFixture]
    class FieldAreaProcessorTest
    {
        [TestCase(5,5)]
        [TestCase(3,3)]
        public void Test_FieldAreaProcessor_Constructor_For_Valid_FieldArea_With_Coordibates(int x, int y)
        {
            var fieldArea = new FieldAreaProcessor(x,y);
            Assert.AreEqual(fieldArea.Xcoordinate, x);
        }
    }
}
