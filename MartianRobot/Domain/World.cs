namespace MartianRobot.Domain;
public class World
{
    private readonly HashSet<Scent> _scents = [];
    private readonly int _upperX;
    private readonly int _upperY;

    public World(int upperX, int upperY)
    {
        this._upperX = upperX;
        this._upperY = upperY;
    }

    public bool IsWithinBounds(Position pos)
    {
        return pos.X >= 0 && pos.X <= _upperX && pos.Y >= 0 && pos.Y <= _upperY;
    }

    public bool HasScent(Position pos, Orientation ori)
    {
        return _scents.Contains(new Scent(pos, ori));
    }

    public void AddScent(Position pos, Orientation ori)
    {
        _scents.Add(new Scent(pos, ori));
    }
}