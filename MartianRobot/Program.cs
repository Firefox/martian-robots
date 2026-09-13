using MartianRobot.Domain;
using MartianRobot.Parsing;
using MartianRobot.Simulator;

// input is read from stdin only, e.g via redirection: dotnet run < input.txt
var input = Console.In.ReadToEnd();

var parser = new InputParser();
var (world, robots) = parser.Parse(input);

var simulator = new RobotSimulator();

foreach (var robotInput in robots)
{
    var robot = new Robot(robotInput.Position, robotInput.Orientation);

    var result = simulator.Simulate(
        robot,
        robotInput.Instructions,
        world
    );

    var orientation = robot.Orientation switch
    {
        Orientation.North => "N",
        Orientation.East => "E",
        Orientation.South => "S",
        Orientation.West => "W",
        _ => throw new ArgumentOutOfRangeException()
    };

    Console.WriteLine(
        $"{robot.Position.X} {robot.Position.Y} {orientation}" +
        (result == RobotSimulator.MoveResult.Lost ? " LOST" : "")
    );
}