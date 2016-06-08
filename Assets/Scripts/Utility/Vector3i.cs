using UnityEngine;

public struct Vector3i
{
    public int x, y, z;

    public static Vector3i zero = new Vector3i();

    public Vector3i(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static Vector3i operator +(Vector3i a, Vector3i b)
    {
        return new Vector3i(a.x + b.x, a.y + b.y, a.z + b.z);
    }
    public static Vector3i operator +(Vector3i a, int b)
    {
        return new Vector3i(a.x + b, a.y + b, a.z + b);
    }

    public static Vector3i operator -(Vector3i a, Vector3i b)
    {
        return new Vector3i(a.x - b.x, a.y - b.y, a.z - b.z);
    }
    public static Vector3i operator -(Vector3i a, int b)
    {
        return new Vector3i(a.x - b, a.y - b, a.z - b);
    }

    public static Vector3i operator *(Vector3i a, Vector3i b)
    {
        return new Vector3i(a.x * b.x, a.y * b.y, a.z * b.z);
    }
    public static Vector3i operator *(Vector3i a, int b)
    {
        return new Vector3i(a.x * b, a.y * b, a.z * b);
    }

    public static Vector3i operator /(Vector3i a, Vector3i b)
    {
        return new Vector3i(a.x / b.x, a.y / b.y, a.z / b.z);
    }
    public static Vector3i operator /(Vector3i a, int b)
    {
        return new Vector3i(a.x / b, a.y / b, a.z / b);
    }

    public override bool Equals(object obj)
    {
        if (!(obj is Vector3i))
            return false;

        Vector3i pos = (Vector3i)obj;
        if (pos.x != x || pos.y != y || pos.z != z)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 47;

            hash = hash * 227 + x.GetHashCode();
            hash = hash * 227 + y.GetHashCode();
            hash = hash * 227 + z.GetHashCode();

            return hash;
        }
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }

    public override string ToString()
    {
        return "X: " + x + " Y: " + y + " Z: " + z;
    }
}
