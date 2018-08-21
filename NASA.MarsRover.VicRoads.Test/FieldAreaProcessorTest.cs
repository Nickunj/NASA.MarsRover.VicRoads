using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Moq;
using NASA.MarsRover.VicRoads.Main.processors;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace NASA.MarsRover.VicRoads.Test
{
    [TestClass]
    public class FieldAreaProcessorTest
    {
        [TestMethod]
        public void Test_FieldAreaProcessorConstructor_when_correct_coordinates_provided()
        {
            // arrange
            var expectedX = 5;
            var expectedY = 5;

            // act
            var fieldAreaUnderTest = new FieldAreaProcessor(5,5);
            
            // asserts
            Assert.AreEqual(expectedX, fieldAreaUnderTest.Xcoordinate);
            Assert.AreEqual(expectedY, fieldAreaUnderTest.Ycoordinate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_FieldAreaProcessorConstructor_when_Xcoordinate_is_negative()
        {
            var fieldAreaUnderTest = new FieldAreaProcessor(-5,5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_FieldAreaProcessorConstructor_when_Ycoordinate_is_negative()
        {
            var fieldAreaUnderTest = new FieldAreaProcessor(5, -5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_FieldAreaProcessorConstructor_when_coordinates_are_zero()
        {
            var fieldAreaUnderTest = new FieldAreaProcessor(0, 0);
        }
    }
}
