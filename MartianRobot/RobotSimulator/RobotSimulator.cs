using MartianRobot.Domain;

namespace MartianRobot.Simulator
{
    public class RobotSimulator
    {
        public enum MoveResult
        {
            Moved,
            Ignored,
            Lost
        }
        public  MoveResult Simulate(Robot robot, string instructions, World world)
        {
            foreach (char instruction in instructions)
            {
                switch (instruction)
                {
                    case 'L':
                        robot.TurnLeft();
                        break;
                    case 'R':
                        robot.TurnRight();
                        break;
                    case 'F':
                        var result = TryMoveForward(robot, world);

                        if (result == MoveResult.Lost)
                            return result;

                        break;
                    default:
                        throw new ArgumentException($"Invalid instruction: {instruction}");
                }
            }
            return MoveResult.Moved;
        }

        private static MoveResult TryMoveForward(Robot robot, World world)
        {
            var newPosition = robot.GetForwardPosition();

            if (world.IsWithinBounds(newPosition))
            {
                robot.MoveTo(newPosition);
                return MoveResult.Moved;
            }

            if (world.HasScent(robot.Position, robot.Orientation))
            {
                return MoveResult.Ignored;
            }

            world.AddScent(robot.Position, robot.Orientation);
            return MoveResult.Lost;
        }
    }
}
