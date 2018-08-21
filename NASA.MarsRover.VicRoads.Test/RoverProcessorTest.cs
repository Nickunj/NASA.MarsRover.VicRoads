using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Moq;
using NASA.MarsRover.VicRoads;
using NASA.MarsRover.VicRoads.Main.processors;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace NASA.MarsRover.VicRoads.Test
{
    [TestClass]
    public class RoverProcessorTest
    {
        [TestMethod]
        public void Test_RoverProcessorConstructor_when_correct_coordinates_provided()
        {
            // arrange
            var fieldArea = new FieldAreaProcessor(5,5);
            var expectedX = 1;
            var expectedY = 2;
            var expectedDirection = "N";

            // act
            var roverProcessorUnderTest = new RoverProcessor(1, 2, "N", fieldArea);

            // asserts
            Assert.AreEqual(expectedX, roverProcessorUnderTest.RoverPositionX);
            Assert.AreEqual(expectedY, roverProcessorUnderTest.RoverPositionY);
            Assert.AreEqual(expectedDirection, roverProcessorUnderTest.Direction);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_RoverProcessorConstructor_when_Xcoordinate_is_negative()
        {
            // arrange
            var fieldArea = new FieldAreaProcessor(5, 5);

            // act
            var roverProcessorUnderTest = new RoverProcessor(-1, 2, "N", fieldArea);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_RoverProcessorConstructor_when_Ycoordinate_is_negative()
        {
            // arrange
            var fieldArea = new FieldAreaProcessor(5, 5);

            // act
            var roverProcessorUnderTest = new RoverProcessor(1, -2, "N", fieldArea);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_RoverProcessorConstructor_when_Xcoordinate_is_outof_FiledArea()
        {
            // arrange
            var fieldArea = new FieldAreaProcessor(5, 5);

            // act
            var roverProcessorUnderTest = new RoverProcessor(7, 2, "N", fieldArea);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_RoverProcessorConstructor_when_Ycoordinate_is_outof_FiledArea()
        {
            // arrange
            var fieldArea = new FieldAreaProcessor(5, 5);

            // act
            var roverProcessorUnderTest = new RoverProcessor(1, 7, "N", fieldArea);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_RoverProcessorConstructor_when_Direction_is_incorrect()
        {
            // arrange
            var fieldArea = new FieldAreaProcessor(5, 5);

            // act
            var roverProcessorUnderTest = new RoverProcessor(1, 2, "A", fieldArea);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_RoverProcessorConstructor_when_FieldArea_is_null()
        {
            // act
            var roverProcessorUnderTest = new RoverProcessor(1, 2, "N", null);
        }


        [TestMethod]
        public void Test_RoverProcessorConstructor_ReadRoverCommand_when_valid_commandstring_provided()
        {
            // arrange
            var fieldArea = new FieldAreaProcessor(5, 5);
            var roverProcessorUnderTest = new RoverProcessor(1, 2, "N", fieldArea);
            String expectedDirection = "N";

            // act
            string actualDirection = roverProcessorUnderTest.ReadRoverCommands("LRM");

            // assert
            Assert.AreEqual(expectedDirection,actualDirection);
        }

        [TestMethod]
        public void Test_RoverProcessorConstructor_ReadRoverCommand_when_invalid_commandstring_provided()
        {
            // arrange
            var fieldArea = new FieldAreaProcessor(5, 5);
            var roverProcessorUnderTest = new RoverProcessor(1, 2, "N", fieldArea);
            String expectedDirection = "N";

            // act
            string actualDirection = roverProcessorUnderTest.ReadRoverCommands("ABCD");

            // assert
            Assert.AreEqual(expectedDirection, actualDirection);
        }

        [TestMethod()]
        public void Test_RoverProcessorConstructor_ReadRoverCommand_MoveRoverForward_RoverOne()
        {
            // arrange
            var fieldArea = new FieldAreaProcessor(5, 5);
            var roverProcessorUnderTest = new RoverProcessor(1, 2, "N", fieldArea);
            String expectedDirection = "N";
            int expectedX = 1;
            int expectedY = 3;

            // act
            roverProcessorUnderTest.ReadRoverCommands("LMLMLMLMM");

            // assert
            Assert.AreEqual(expectedDirection, roverProcessorUnderTest.Direction);
            Assert.AreEqual(expectedX, roverProcessorUnderTest.RoverPositionX);
            Assert.AreEqual(expectedY, roverProcessorUnderTest.RoverPositionY);
        }


        [TestMethod()]
        public void Test_RoverProcessorConstructor_ReadRoverCommand_MoveRoverForward_RoverTwo()
        {
            // arrange
            var fieldArea = new FieldAreaProcessor(5, 5);
            var roverProcessorUnderTest = new RoverProcessor(3, 3, "E", fieldArea);
            String expectedDirection = "E";
            int expectedX = 5;
            int expectedY = 1;

            // act
            roverProcessorUnderTest.ReadRoverCommands("MMRMMRMRRM");

            // assert
            Assert.AreEqual(expectedDirection, roverProcessorUnderTest.Direction);
            Assert.AreEqual(expectedX, roverProcessorUnderTest.RoverPositionX);
            Assert.AreEqual(expectedY, roverProcessorUnderTest.RoverPositionY);
        }

        [TestMethod]
        public void Test_RoverProcessorConstructor_ReadRoverCommand_MoveRoverForward_ToPositionString()
        {
            // arrange
            var fieldArea = new FieldAreaProcessor(5, 5);
            var roverProcessorUnderTest = new RoverProcessor(3, 3, "E", fieldArea);

            string roverPosition = roverProcessorUnderTest.ToPositionString();

            Assert.IsNotNull(roverPosition);
        }

        [TestMethod]
        public void Test_RoverProcessorConstructor_ReadRoverCommand_MoveRoverForward_CheckRoverPositionOutsideField_when_outside_fieldarea()
        {
            // arrange
            var fieldArea = new FieldAreaProcessor(5, 5);
            var roverProcessorUnderTest = new RoverProcessor(3, 3, "E", fieldArea);
            roverProcessorUnderTest.ReadRoverCommands("MMMMM");

            bool roverPositionOutsideFieldArea = roverProcessorUnderTest.CheckRoverPositionOutsideField();

            Assert.IsTrue(roverPositionOutsideFieldArea);
        }

        [TestMethod]
        public void Test_RoverProcessorConstructor_ReadRoverCommand_MoveRoverForward_CheckRoverPositionOutsideField_when_inside_fieldarea()
        {
            // arrange
            var fieldArea = new FieldAreaProcessor(5, 5);
            var roverProcessorUnderTest = new RoverProcessor(1, 2, "E", fieldArea);
            roverProcessorUnderTest.ReadRoverCommands("MM");

            bool roverPositionOutsideFieldArea = roverProcessorUnderTest.CheckRoverPositionOutsideField();

            Assert.IsFalse(roverPositionOutsideFieldArea);
        }

        [TestMethod]
        public void Test_RoverProcessorConstructor_ReadRoverCommand_TurnRoverToLeft()
        {
            // arrange
            var fieldArea = new FieldAreaProcessor(5, 5);
            var roverProcessorUnderTest = new RoverProcessor(1, 2, "N", fieldArea);
            String expectedDirection = "W";

            // act
            roverProcessorUnderTest.TurnRoverToLeft();

            // assert
            NUnit.Framework.Assert.That(roverProcessorUnderTest.Direction, NUnit.Framework.Is.EqualTo(expectedDirection));

        }


        [TestMethod]
        public void Test_RoverProcessorConstructor_ReadRoverCommand_TurnRoverToRight()
        {
            // arrange
            var fieldArea = new FieldAreaProcessor(5, 5);
            var roverProcessorUnderTest = new RoverProcessor(1, 2, "N", fieldArea);
            String expectedDirection = "E";

            // act
            roverProcessorUnderTest.TurnRoverToRight();

            // assert
            NUnit.Framework.Assert.That(roverProcessorUnderTest.Direction, NUnit.Framework.Is.EqualTo(expectedDirection));
        }


        [TestMethod]
        public void Test_RoverProcessorConstructor_ReadRoverCommand_MoveRoverForward()
        {
            // arrange
            var fieldArea = new FieldAreaProcessor(5, 5);
            var roverProcessorUnderTest = new RoverProcessor(1, 2, "N", fieldArea);
            var expectedY = 3;

            // act
            roverProcessorUnderTest.MoveRoverForward();

            // assert
            NUnit.Framework.Assert.That(roverProcessorUnderTest.RoverPositionY, NUnit.Framework.Is.EqualTo(expectedY));
        }

    }
}
