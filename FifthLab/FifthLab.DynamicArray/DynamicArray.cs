using System.Collections;

namespace FifthLab.DynamicArray;

public class DynamicArray<T> : ICollection<T>
{
    private T[] _array;

    private readonly int _initialCapacity;
    
    public DynamicArray(int initialCapacity = 4)
    {
        Count = 0;
        _initialCapacity = initialCapacity;
        _array = new T[_initialCapacity];
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException("Index out of range");
            return _array[index];
        }
        set
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException("Index out of range");
            _array[index] = value;
        }
    }
    public int Count { get; private set; }
    
    public bool IsReadOnly => false;

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
        {
            yield return _array[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(T item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        if (Count == _array.Length)
        {
            Array.Resize(ref _array, _array.Length * 2);
        }
        _array[Count] = item;

        Count++;
    }

    public void Clear()
    {
        _array = new T[_initialCapacity];
        Count = 0;
    }

    public bool Contains(T item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        return Array.IndexOf(_array, item) != -1;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        _array.CopyTo(array, arrayIndex);
    }

    public bool Remove(T item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        int index = Array.IndexOf(_array, item);
        if (index != -1)
        {
            RemoveAt(index);
            return true;
        }

        return false;
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= Count)
            throw new IndexOutOfRangeException("Index out of range");
        
        ArraySegment<T> segment = new ArraySegment<T>(_array, index + 1, Count - index - 1);
        Array.Copy(segment.Array, segment.Offset, _array, index, segment.Count);

        Count--;
        _array[Count] = default!;
    }
}