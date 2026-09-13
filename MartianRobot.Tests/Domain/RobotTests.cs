using MartianRobot.Domain;
using Shouldly;

namespace MartianRobot.Tests.Domain
{
    public class RobotTests
    {
        [Theory]
        [InlineData(Orientation.N, Orientation.W)]
        [InlineData(Orientation.W, Orientation.S)]
        [InlineData(Orientation.S, Orientation.E)]
        [InlineData(Orientation.E, Orientation.N)]
        public void TurnLeft_ShouldRotateCounterClockwise(Orientation initial, Orientation expected)
        {
            var robot = new Robot(new Position(1, 1), initial);

            robot.TurnLeft();

            robot.Orientation.ShouldBe(expected);
        }

        [Theory]
        [InlineData(Orientation.N, Orientation.E)]
        [InlineData(Orientation.E, Orientation.S)]
        [InlineData(Orientation.S, Orientation.W)]
        [InlineData(Orientation.W, Orientation.N)]
        public void TurnRight_ShouldRotateClockwise(Orientation initial, Orientation expected)
        {
            var robot = new Robot(new Position(1, 1), initial);

            robot.TurnRight();

            robot.Orientation.ShouldBe(expected);
        }

        [Theory]
        [InlineData(Orientation.N, 1, 2)]
        [InlineData(Orientation.E, 2, 1)]
        [InlineData(Orientation.S, 1, 0)]
        [InlineData(Orientation.W, 0, 1)]
        public void GetForwardPosition_ShouldCalculateCorrectNextCoordinates(Orientation orientation, int expectedX, int expectedY)
        {
            var robot = new Robot(new Position(1, 1), orientation);

            var nextPosition = robot.GetForwardPosition();

            nextPosition.ShouldBe(new Position(expectedX, expectedY));
        }
    }
}