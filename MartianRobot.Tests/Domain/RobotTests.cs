using MartianRobot.Domain;
using Shouldly;

namespace MartianRobot.Tests.Domain
{
    public class RobotTests
    {
        [Theory]
        [InlineData(Orientation.North, Orientation.West)]
        [InlineData(Orientation.West, Orientation.South)]
        [InlineData(Orientation.South, Orientation.East)]
        [InlineData(Orientation.East, Orientation.North)]
        public void TurnLeft_ShouldRotateCounterClockwise(Orientation initial, Orientation expected)
        {
            var robot = new Robot(new Position(1, 1), initial);

            robot.TurnLeft();

            robot.Orientation.ShouldBe(expected);
        }

        [Theory]
        [InlineData(Orientation.North, Orientation.East)]
        [InlineData(Orientation.East, Orientation.South)]
        [InlineData(Orientation.South, Orientation.West)]
        [InlineData(Orientation.West, Orientation.North)]
        public void TurnRight_ShouldRotateClockwise(Orientation initial, Orientation expected)
        {
            var robot = new Robot(new Position(1, 1), initial);

            robot.TurnRight();

            robot.Orientation.ShouldBe(expected);
        }

        [Theory]
        [InlineData(Orientation.North, 1, 2)]
        [InlineData(Orientation.East, 2, 1)]
        [InlineData(Orientation.South, 1, 0)]
        [InlineData(Orientation.West, 0, 1)]
        public void GetForwardPosition_ShouldCalculateCorrectNextCoordinates(Orientation orientation, int expectedX, int expectedY)
        {
            var robot = new Robot(new Position(1, 1), orientation);

            var nextPosition = robot.GetForwardPosition();

            nextPosition.ShouldBe(new Position(expectedX, expectedY));
        }
    }
}