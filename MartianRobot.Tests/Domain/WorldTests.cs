using MartianRobot.Domain;
using Shouldly;

namespace MartianRobot.Tests.Domain;

public class WorldTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(5, 3)]
    [InlineData(2, 1)]
    public void IsWithinBounds_PositionInsideWorld_ShouldReturnTrue(int x, int y)
    {
        var world = new World(5, 3);

        world.IsWithinBounds(new Position(x, y)).ShouldBeTrue();
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(0, -1)]
    [InlineData(6, 3)]
    [InlineData(5, 4)]
    public void IsWithinBounds_PositionOutsideWorld_ShouldReturnFalse(int x, int y)
    {
        var world = new World(5, 3);

        world.IsWithinBounds(new Position(x, y)).ShouldBeFalse();
    }

    [Fact]
    public void HasScent_BeforeScentIsAdded_ShouldReturnFalse()
    {
        var world = new World(5, 3);

        world.HasScent(new Position(5, 3), Orientation.E)
            .ShouldBeFalse();
    }

    [Fact]
    public void AddScent_ShouldMakeScentDetectable()
    {
        var world = new World(5, 3);
        var position = new Position(5, 3);

        world.AddScent(position, Orientation.E);

        world.HasScent(position, Orientation.E).ShouldBeTrue();
    }

    [Fact]
    public void ScentAtDifferentOrientation_ShouldNotMatch()
    {
        var world = new World(5, 3);
        var position = new Position(5, 3);

        world.AddScent(position, Orientation.E);

        world.HasScent(position, Orientation.N).ShouldBeFalse();
    }

    [Fact]
    public void ScentAtDifferentPosition_ShouldNotMatch()
    {
        var world = new World(5, 3);

        world.AddScent(new Position(5, 3), Orientation.E);

        world.HasScent(new Position(4, 3), Orientation.E).ShouldBeFalse();
    }

    [Fact]
    public void AddingSameScentMoreThanOnce_ShouldRemainSafe()
    {
        var world = new World(5, 3);
        var position = new Position(5, 3);

        world.AddScent(position, Orientation.E);
        world.AddScent(position, Orientation.E);

        world.HasScent(position, Orientation.E).ShouldBeTrue();
    }
}