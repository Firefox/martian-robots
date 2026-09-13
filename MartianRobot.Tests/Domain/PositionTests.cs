using MartianRobot.Domain;
using Shouldly;

namespace MartianRobot.Tests.Domain;

public class PositionTests
{
    [Fact]
    public void Positions_WithSameCoordinates_ShouldBeEqual()
    {
        var first = new Position(1, 2);
        var second = new Position(1, 2);

        first.ShouldBe(second);
    }

    [Fact]
    public void Positions_WithDifferentXCoordinates_ShouldNotBeEqual()
    {
        var first = new Position(1, 2);
        var second = new Position(2, 2);

        first.ShouldNotBe(second);
    }

    [Fact]
    public void Positions_WithDifferentYCoordinates_ShouldNotBeEqual()
    {
        var first = new Position(1, 2);
        var second = new Position(1, 3);

        first.ShouldNotBe(second);
    }

    [Fact]
    public void Position_ShouldExposeItsCoordinates()
    {
        var position = new Position(3, 4);

        position.X.ShouldBe(3);
        position.Y.ShouldBe(4);
    }
}