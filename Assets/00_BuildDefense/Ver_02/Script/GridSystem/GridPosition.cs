using System;

public struct GridPosition : IEquatable<GridPosition>
{
    public int x;
    public int z;

    public GridPosition(int x, int z)
    {
        this.x = x;
        this.z = z;
    }
    public GridPosition Right => this + new GridPosition(1, 0);
    public GridPosition RightUp => this + new GridPosition(1, 1);
    public GridPosition RightDown => this + new GridPosition(1, -1);


    public override bool Equals(object obj)
    {
        return obj is GridPosition position &&
               x == position.x &&
               z == position.z;
    }

    public bool Equals(GridPosition other)
    {
        return this == other;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, z);
    }


    public static bool operator ==(GridPosition a, GridPosition b)
    {
        return a.x == b.x && a.z == b.z;
    }

    public static bool operator !=(GridPosition a, GridPosition b)
    {
        return !(a == b);
    }

    public static GridPosition operator +(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.x+b.x, a.z+b.z);
    }

    public static GridPosition operator -(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.x-b.x, a.z-b.z);
    }

    public static GridPosition operator +(GridPosition a, int b)
    {
        return new GridPosition(a.x+b, a.z+b);
    }

    public override string ToString()
    {
        return "(" + x + ", " + z + ")";
    }
}
