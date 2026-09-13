using MartianRobot.Domain;
using MartianRobot.Simulator;
using Shouldly;

namespace MartianRobot.Tests.Simulator;

public class RobotSimulatorTests
{
    [Fact]
    public void Execute_ShouldMoveRobotToExpectedPosition()
    {
        var robot = new Robot(
            new Position(1, 2),
            Orientation.N);

        var simulator = new RobotSimulator();
        var world = new World(5, 5);
        simulator.Simulate(robot, "LFLFLFLFF", world);

        robot.Position.ShouldBe(new Position(1, 3));
        robot.Orientation.ShouldBe(Orientation.N);
    }

    [Fact]
    public void Execute_GivenExampleInstructions_ShouldProduceExpectedResults()
    {
        // Arrange
        var world = new World(5, 3);

        var robot1 = new Robot(
            new Position(1, 1),
            Orientation.E);

        var robot2 = new Robot(
            new Position(3, 2),
            Orientation.N);

        var robot3 = new Robot(
            new Position(0, 3),
            Orientation.W);

        // Act
        var simulator = new RobotSimulator();
        var result1 = simulator.Simulate(robot1, "RFRFRFRF", world);
        var result2 = simulator.Simulate(robot2, "FRRFLLFFRRFLL", world);
        var result3 = simulator.Simulate(robot3, "LLFFFLFLFL", world);

        // Assert
        robot1.Position.ShouldBe(new Position(1, 1));
        robot1.Orientation.ShouldBe(Orientation.E);
        result1.ShouldBe(RobotSimulator.MoveResult.Moved);

        robot2.Position.ShouldBe(new Position(3, 3));
        robot2.Orientation.ShouldBe(Orientation.N);
        result2.ShouldBe(RobotSimulator.MoveResult.Lost);

        robot3.Position.ShouldBe(new Position(2, 3));
        robot3.Orientation.ShouldBe(Orientation.S);
        result3.ShouldBe(RobotSimulator.MoveResult.Moved);
    }

    //
    [Fact]
    public void Simulate_RobotLeavingWorld_ShouldReturnLostAndKeepLastPosition()
    {
        var world = new World(5, 3);
        var robot = new Robot(new Position(5, 3), Orientation.E);

        var result = new RobotSimulator().Simulate(robot, "F", world);

        result.ShouldBe(RobotSimulator.MoveResult.Lost);
        robot.Position.ShouldBe(new Position(5, 3));
        robot.Orientation.ShouldBe(Orientation.E);
    }

    [Fact]
    public void Simulate_RobotEncounteringExistingScent_ShouldIgnoreMove()
    {
        var world = new World(5, 3);
        var firstRobot = new Robot(new Position(5, 3), Orientation.E);
        var secondRobot = new Robot(new Position(5, 3), Orientation.E);
        var simulator = new RobotSimulator();

        simulator.Simulate(firstRobot, "F", world);
        var result = simulator.Simulate(secondRobot, "F", world);

        result.ShouldBe(RobotSimulator.MoveResult.Moved);
        secondRobot.Position.ShouldBe(new Position(5, 3));
    }

    [Fact]
    public void Simulate_AfterIgnoredMove_ShouldContinueProcessingInstructions()
    {
        var world = new World(5, 3);
        var firstRobot = new Robot(new Position(5, 3), Orientation.E);
        var secondRobot = new Robot(new Position(5, 3), Orientation.E);
        var simulator = new RobotSimulator();

        simulator.Simulate(firstRobot, "F", world);

        var result = simulator.Simulate(secondRobot, "FR", world);

        result.ShouldBe(RobotSimulator.MoveResult.Moved);
        secondRobot.Position.ShouldBe(new Position(5, 3));
        secondRobot.Orientation.ShouldBe(Orientation.S);
    }

    [Fact]
    public void Simulate_InvalidInstruction_ShouldThrowArgumentException()
    {
        var robot = new Robot(new Position(1, 1), Orientation.N);
        var world = new World(5, 3);

        Should.Throw<ArgumentException>(
            () => new RobotSimulator().Simulate(robot, "X", world));
    }

    [Fact]
    public void Simulate_EmptyInstructions_ShouldLeaveRobotUnchanged()
    {
        var robot = new Robot(new Position(1, 1), Orientation.N);
        var world = new World(5, 3);

        var result = new RobotSimulator().Simulate(robot, "", world);

        result.ShouldBe(RobotSimulator.MoveResult.Moved);
        robot.Position.ShouldBe(new Position(1, 1));
        robot.Orientation.ShouldBe(Orientation.N);
    }

    [Fact]
    public void Simulate_RobotLost_ShouldCreateScent()
    {
        var world = new World(5, 3);
        var robot = new Robot(new Position(5, 3), Orientation.E);

        new RobotSimulator().Simulate(robot, "F", world);

        world.HasScent(new Position(5, 3), Orientation.E).ShouldBeTrue();
    }

    [Fact]
    public void Simulate_WhenRobotIsLost_ShouldStopProcessingInstructions()
    {
        var world = new World(5, 3);
        var robot = new Robot(new Position(5, 3), Orientation.E);

        var result = new RobotSimulator().Simulate(robot, "FR", world);

        result.ShouldBe(RobotSimulator.MoveResult.Lost);
        robot.Position.ShouldBe(new Position(5, 3));
        robot.Orientation.ShouldBe(Orientation.E);
    }
}
