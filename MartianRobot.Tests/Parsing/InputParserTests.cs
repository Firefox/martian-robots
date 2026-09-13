using MartianRobot.Domain;
using MartianRobot.Parsing;
using Shouldly;

namespace MartianRobot.Tests.Parsing;

public class InputParserTests
{
    [Fact]
    public void Parse_ValidInput_ShouldReturnExpectedResult()
    {
        // Arrange
        var input = "5 3\r\n1 1 E\r\nRFRFRFRF\r\n3 2 N\r\nFRRFLLFFRRFLL\r\n0 3 W\r\nLLFFFLFLFL";
        var parser = new InputParser();

        // Act
        (World World, List<RobotInstructions> Robots) result = parser.Parse(input);

        // Assert
        result.World.ShouldNotBeNull();
        result.Robots.ShouldNotBeNull();
        result.World.ShouldBe(new World(5, 3));
        result.Robots.Count.ShouldBe(3);
        result.Robots.ShouldBe([
            new RobotInstructions(new Position(1, 1), Orientation.East, "RFRFRFRF"),
            new RobotInstructions(new Position(3, 2), Orientation.North, "FRRFLLFFRRFLL"),
            new RobotInstructions(new Position(0, 3), Orientation.West, "LLFFFLFLFL")
        ]);
    }

    [Fact]
    public void Parse_InvalidInput_ShouldThrowException()
    {
        // Arrange
        var input = "invalid input";
        var parser = new InputParser();

        // Act & Assert
        Should.Throw<FormatException>(() => parser.Parse(input));
    }

    [Fact]
    public void Parse_UnixLineEndings_ShouldReturnExpectedResult()
    {
        // Arrange
        var input = "5 3\n1 1 E\nRFRFRFRF\n";
        var parser = new InputParser();

        // Act
        var result = parser.Parse(input);

        // Assert
        result.World.ShouldBe(new World(5, 3));
        result.Robots.ShouldBe([
            new RobotInstructions(new Position(1, 1), Orientation.East, "RFRFRFRF")
        ]);
    }

    [Fact]
    public void Parse_InvalidOrientation_ShouldThrowFormatException()
    {
        // Arrange
        var input = "5 3\r\n1 1 Q\r\nF";
        var parser = new InputParser();

        // Act & Assert
        Should.Throw<FormatException>(() => parser.Parse(input));
    }

    [Fact]
    public void Parse_InvalidRobotCoordinates_ShouldThrowFormatException()
    {
        // Arrange
        var input = "5 3\r\na 1 E\r\nF";
        var parser = new InputParser();

        // Act & Assert
        Should.Throw<FormatException>(() => parser.Parse(input));
    }

    [Fact]
    public void Parse_RobotPositionWithoutOrientation_ShouldThrowFormatException()
    {
        // Arrange
        var input = "5 3\r\n1 1\r\nF";
        var parser = new InputParser();

        // Act & Assert
        Should.Throw<FormatException>(() => parser.Parse(input));
    }

    [Fact]
    public void Parse_EmptyInput_ShouldThrowException()
    {
        // Arrange
        var input = "";
        var parser = new InputParser();

        // Act & Assert
        Should.Throw<ArgumentException>(() => parser.Parse(input));
    }

    [Fact]
    public void Parse_NullInput_ShouldThrowException()
    {
        // Arrange
        string? input = null;
        var parser = new InputParser();

        // Act & Assert
        Should.Throw<ArgumentException>(() => parser.Parse(input));
    }

    [Fact]
    public void Parse_InputWithExtraWhitespace_ShouldReturnExpectedResult()
    {
        // Arrange
        var input = "  5 3  \r\n  1 1 E  \r\n  RFRFRFRF  \r\n  3 2 N  \r\n  FRRFLLFFRRFLL  \r\n  0 3 W  \r\n  LLFFFLFLFL  ";
        var parser = new InputParser();

        // Act
        var result = parser.Parse(input);

        // Assert
        result.World.ShouldNotBeNull();
        result.Robots.ShouldNotBeNull();
        result.World.ShouldBe(new World(5, 3));
        result.Robots.Count.ShouldBe(3);
        result.Robots.ShouldBe([
            new RobotInstructions(new Position(1, 1), Orientation.East, "RFRFRFRF"),
            new RobotInstructions(new Position(3, 2), Orientation.North, "FRRFLLFFRRFLL"),
            new RobotInstructions(new Position(0, 3), Orientation.West, "LLFFFLFLFL")
        ]);
    }

    [Fact]
    public void Parse_InputWithMissingRobotInstructions_ShouldThrowException()
    {
        // Arrange
        var input = "5 3\r\n1 1 E\r\n3 2 N\r\nFRRFLLFFRRFLL\r\n0 3 W\r\nLLFFFLFLFL";
        var parser = new InputParser();

        // Act & Assert
        Should.Throw<ArgumentException>(() => parser.Parse(input));
    }

    [Fact]
    public void Parse_InputWithTrailingNewlines_ShouldParseSuccessfully()
    {
        // Arrange: Valid input with extra blank lines at the end
        var input = "5 3\r\n1 1 E\r\nRFRFRFRF\r\n\r\n  \r\n";
        var parser = new InputParser();

        // Act
        var (world, robots) = parser.Parse(input);

        // Assert
        world.ShouldNotBeNull();
        robots.Count.ShouldBe(1);
    }
}
