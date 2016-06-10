using UnityEngine;
using System.Collections.Generic;

public class HashArray<T> {

    private Dictionary<int, T> diction;
    private int count;

    public int Count { get { return count; } }

    public HashArray() {
        diction = new Dictionary<int, T>();
        count = 0;
    }

    public void Add(T value) {
        diction.Add(count++, value);
    }

    public T Get(int index) {
        T v;
        diction.TryGetValue(index, out v);
        return v;
    }

    public bool Contains(T value) {
        return diction.ContainsValue(value);
    }
	
    public T[] ToArray() {
        T[] vals = new T[count];
        diction.Values.CopyTo(vals, 0);
        return vals;
    }

}
