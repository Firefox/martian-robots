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
            Orientation.N => new Position(_position.X, _position.Y + 1),
            Orientation.E => new Position(_position.X + 1, _position.Y),
            Orientation.S => new Position(_position.X, _position.Y - 1),
            Orientation.W => new Position(_position.X - 1, _position.Y),
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
            Orientation.N => Orientation.W,
            Orientation.W => Orientation.S,
            Orientation.S => Orientation.E,
            Orientation.E => Orientation.N,
            _ => throw new InvalidOperationException("Invalid orientation")
        };
    }

    public void TurnRight()
    {
        _orientation = _orientation switch
        {
            Orientation.N => Orientation.E,
            Orientation.E => Orientation.S,
            Orientation.S => Orientation.W,
            Orientation.W => Orientation.N,
            _ => throw new InvalidOperationException("Invalid orientation")
        };
    }
}
