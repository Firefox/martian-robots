namespace MartianRobot.Domain;

public class Robot
{
    private Position _position;
    private Orientation _orientation;

    public Robot(Position position, Orientation orientation)
    {
        _position = position;
        _orientation = orientation;
    }

    public Position Position => _position;
    public Orientation Orientation => _orientation;

    public Position GetForwardPosition()
    {
        return _orientation switch
        {
            Orientation.North => new Position(_position.X, _position.Y + 1),
            Orientation.East => new Position(_position.X + 1, _position.Y),
            Orientation.South => new Position(_position.X, _position.Y - 1),
            Orientation.West => new Position(_position.X - 1, _position.Y),
            _ => throw new InvalidOperationException("Invalid orientation")
        };
    }

    public void MoveTo(Position position)
    {
        _position = position;
    }

    public void TurnLeft()
    {
        _orientation = _orientation switch
        {
            Orientation.North => Orientation.West,
            Orientation.West => Orientation.South,
            Orientation.South => Orientation.East,
            Orientation.East => Orientation.North,
            _ => throw new InvalidOperationException("Invalid orientation")
        };
    }

    public void TurnRight()
    {
        _orientation = _orientation switch
        {
            Orientation.North => Orientation.East,
            Orientation.East => Orientation.South,
            Orientation.South => Orientation.West,
            Orientation.West => Orientation.North,
            _ => throw new InvalidOperationException("Invalid orientation")
        };
    }
}
