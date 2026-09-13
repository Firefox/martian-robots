using MartianRobot.Domain;

namespace MartianRobot.Parsing;

public record RobotInstructions(Position Position, Orientation Orientation, string Instructions);

public class InputParser
{
    public (World World, List<RobotInstructions> Robots) Parse(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Input cannot be null or empty.");

        // Normalise line endings and strip out any blank/whitespace lines (including trailing newlines)
        var lines = input
            .Replace("\r\n", "\n")
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            // Strip hidden control characters (like \u0018) while preserving standard text
            .Select(line => new string(line.Where(c => !char.IsControl(c) || c == ' ').ToArray()).Trim())
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .ToArray();

        Console.WriteLine(lines[lines.Length - 1]);

        if (lines.Length == 0)
            throw new ArgumentException("Input contains no valid lines.");

        // Parse world dimensions
        var worldCoordinates = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (worldCoordinates.Length < 2 ||
            !int.TryParse(worldCoordinates[0], out var upperX) ||
            !int.TryParse(worldCoordinates[1], out var upperY))
        {
            throw new FormatException("Invalid world dimensions format.");
        }

        var world = new World(upperX, upperY);
        var robots = new List<RobotInstructions>();

        // Ensure paired lines exist (1 position line + 1 instruction line per robot)
        if ((lines.Length - 1) % 2 != 0)
            throw new ArgumentException("Each robot must have a position and instruction line.");

        for (int i = 1; i < lines.Length; i += 2)
        {
            var robotPosition = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (robotPosition.Length < 3)
            {
                throw new FormatException("Robot position must contain X, Y, and Orientation.");
            }

            if (!int.TryParse(robotPosition[0], out var x) || !int.TryParse(robotPosition[1], out var y))
            {
                throw new FormatException("Invalid robot coordinates format.");
            }

            var orientation = ParseOrientation(robotPosition[2]);
            var instructions = lines[i + 1];

            robots.Add(new RobotInstructions(new Position(x, y), orientation, instructions));
        }

        return (world, robots);
    }

    private static Orientation ParseOrientation(string value) => value switch
    {
        "N" => Orientation.North,
        "S" => Orientation.South,
        "E" => Orientation.East,
        "W" => Orientation.West,
        _ => throw new FormatException($"Invalid orientation: {value}")
    };
}