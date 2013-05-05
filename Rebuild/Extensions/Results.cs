using System.Collections.Generic;

namespace Rebuild.Extensions
{
    #region struct AddIfNotExistsResult<T>
    public struct AddIfNotExistsResult<T>
    {
        public AddIfNotExistsResult(T item, ICollection<T> collection, bool isAdded)
            : this()
        {
            Collection = collection;
            Item = item;
            IsAdded = isAdded;
        }

        public ICollection<T> Collection { get; private set; }

        public bool IsAdded { get; private set; }

        public T Item { get; private set; }
    }
    #endregion

    #region struct AddIfNotExistsResult<TKey, TValue>
    public struct AddIfNotExistsResult<TKey, TValue>
    {
        public AddIfNotExistsResult(TKey key, TValue value, IDictionary<TKey, TValue> dictionary, bool isAdded)
            : this()
        {
            Dictionary = dictionary;
            Key = key;
            Value = value;
            IsAdded = isAdded;
        }

        public IDictionary<TKey, TValue> Dictionary { get; private set; }

        public bool IsAdded { get; private set; }

        public TKey Key { get; private set; }

        public TValue Value { get; private set; }
    }
    #endregion

    #region struct AddRangeResult<T>
    public struct AddRangeResult<T>
    {
        public AddRangeResult(ICollection<T> collection, T[] addedItems)
            : this()
        {
            AddedItems = addedItems;
            Collection = collection;
        }

        public T[] AddedItems { get; private set; }

        public ICollection<T> Collection { get; private set; }
    }
    #endregion

    #region struct AddToResult<T, TCollection>
    public struct AddToResult<T, TCollection>
    {
        public AddToResult(T[] addedItems, TCollection collection)
            : this()
        {
            AddedItems = addedItems;
            Collection = collection;
        }

        public T[] AddedItems { get; private set; }

        public TCollection Collection { get; private set; }
    }
    #endregion

    #region struct BinaryInsertResult<T>
    public struct BinaryInsertResult<T>
    {
        public BinaryInsertResult(IList<T> list, T item, int index, bool isInserted)
            : this()
        {
            List = list;
            Item = item;
            Index = index;
            IsInserted = isInserted;
        }

        public int Index { get; private set; }

        public bool IsInserted { get; private set; }

        public T Item { get; private set; }

        public IList<T> List { get; private set; }
    }
    #endregion

    #region struct BinaryRemoveResult<T>
    public struct BinaryRemoveResult<T>
    {
        public BinaryRemoveResult(IList<T> list, T item, int index)
            : this()
        {
            List = list;
            Item = item;
            Index = index;
        }

        public bool IsItemRemoved { get { return Index > -1; } }

        public int Index { get; private set; }

        public T Item { get; private set; }

        public IList<T> List { get; private set; }
    }
    #endregion

    #region struct CopyToResult<T>
    public struct CopyToResult<T>
    {
        public CopyToResult(IEnumerable<T> source, T[] destination, T[] items)
            : this()
        {
            Source = source;
            Destination = destination;
            Items = items;
        }

        public T[] Destination { get; private set; }

        public T[] Items { get; private set; }

        public IEnumerable<T> Source { get; private set; }
    }
    #endregion

    #region struct MoveResult<T, TCollection>
    public struct MoveResult<T, TCollection>
    {
        public MoveResult(ICollection<T> source, TCollection destination, T[] movedItems)
            : this()
        {
            Source = source;
            Destination = destination;
            MovedItems = movedItems;
        }

        public TCollection Destination { get; private set; }

        public T[] MovedItems { get; private set; }

        public ICollection<T> Source { get; private set; }
    }
    #endregion

    #region struct RemoveFromResult<T, TCollection>
    public struct RemoveFromResult<T, TCollection>
    {
        public RemoveFromResult(T[] removedItems, TCollection collection)
            : this()
        {
            RemovedItems = removedItems;
            Collection = collection;
        }

        public T[] RemovedItems { get; private set; }

        public TCollection Collection { get; private set; }
    }
    #endregion

    #region struct RemoveRangeResult<T>
    public struct RemoveRangeResult<T>
    {
        public RemoveRangeResult(ICollection<T> collection, T[] removedItems)
            : this()
        {
            Collection = collection;
            RemovedItems = removedItems;
        }

        public ICollection<T> Collection { get; private set; }

        public T[] RemovedItems { get; private set; }
    }
    #endregion
}
