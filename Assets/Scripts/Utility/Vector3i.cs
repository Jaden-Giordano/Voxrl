using UnityEngine;

public struct Vector3i
{
    public int x, y, z;

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

    public static Vector3i zero = new Vector3i();

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }

    public override string ToString()
    {
        return "X: " + x + " Y: " + y + " Z: " + z;
    }
}
