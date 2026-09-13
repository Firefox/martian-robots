namespace MartianRobot.Domain;
public class World : IEquatable<World>
{
    private readonly HashSet<Scent> _scents = [];
    private readonly int _upperX;
    private readonly int _upperY;

    public World(int upperX, int upperY)
    {
        this._upperX = upperX;
        this._upperY = upperY;
    }

    public bool IsWithinBounds(Position position)
    {
        return position.X >= 0 && position.X <= _upperX && position.Y >= 0 && position.Y <= _upperY;
    }

    public bool HasScent(Position pos, Orientation ori)
    {
        return _scents.Contains(new Scent(pos, ori));
    }

    public void AddScent(Position pos, Orientation ori)
    {
        _scents.Add(new Scent(pos, ori));
    }

    public bool Equals(World? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return _upperX == other._upperX && _upperY == other._upperY && _scents.SetEquals(other._scents);
    }

    public override bool Equals(object? obj) => Equals(obj as World);

    public override int GetHashCode() => HashCode.Combine(_upperX, _upperY);
}