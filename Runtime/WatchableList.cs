using ABXY.Watchables.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
namespace ABXY.Watchables
{

    [System.Serializable]
    public class WatchableList<T> : WatchableListBase
    {
        public System.Action<T, int> onElementAssignment;

        public System.Action OnChange;

        public System.Action<T, int> onInsertion;

        public System.Action<T> onRemoval;

        [SerializeField]
        private List<T> _value = new List<T>();

        public WatchableList() { }
        public WatchableList(IEnumerable<T> collection)
        {
            foreach (var item in collection)
                Add(item);
        }
        public WatchableList(int capacity)
        {
            this.Capacity = capacity;
        }

        public T this[int index]
        {
            get { return _value[index]; }
            set
            {
                _value[index] = value;
                OnChange?.Invoke();
                onElementAssignment?.Invoke(value, index);
            }
        }

        public int Count { get { return _value.Count; } }
        public int Capacity
        {
            get { return _value.Capacity; }
            set
            {
                _value.Capacity = value;
                OnChange?.Invoke();
            }
        }

        public void Add(T item)
        {
            _value.Add(item);
            onInsertion?.Invoke(item, _value.Count - 1);
            OnChange?.Invoke();
        }
        public void AddRange(IEnumerable<T> collection)
        {
            foreach (var item in collection)
                Add(item);
        }
        public ReadOnlyCollection<T> AsReadOnly()
        {
            return _value.AsReadOnly();
        }
        public int BinarySearch(T item)
        {
            return _value.BinarySearch(item);
        }
        public int BinarySearch(T item, IComparer<T> comparer)
        {
            return _value.BinarySearch(item, comparer);
        }
        public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
        {
            return _value.BinarySearch(index, count, item, comparer);
        }
        public void Clear()
        {
            List<T> removedItems = new List<T>(_value);
            _value.Clear();
            for (int index = 0; index < removedItems.Count; index++)
                onRemoval?.Invoke(removedItems[index]);
        }
        public bool Contains(T item)
        {
            return _value.Contains(item);
        }
        public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
        {
            return _value.ConvertAll<TOutput>(converter);
        }
        public void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            _value.CopyTo(index, array, arrayIndex, count);
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            _value.CopyTo(array, arrayIndex);
        }
        public void CopyTo(T[] array)
        {
            _value.CopyTo(array);
        }
        public bool Exists(Predicate<T> match)
        {
            return _value.Exists(match);
        }
        public T Find(Predicate<T> match)
        {
            return _value.Find(match);
        }
        public List<T> FindAll(Predicate<T> match)
        {
            return _value.FindAll(match);
        }
        public int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            return _value.FindIndex(startIndex, count, match);
        }
        public int FindIndex(int startIndex, Predicate<T> match)
        {
            return _value.FindIndex(startIndex, match);
        }
        public int FindIndex(Predicate<T> match)
        {
            return _value.FindIndex(match);
        }
        public T FindLast(Predicate<T> match)
        {
            return _value.FindLast(match);
        }
        public int FindLastIndex(int startIndex, int count, Predicate<T> match)
        {
            return _value.FindLastIndex(startIndex, count, match);
        }
        public int FindLastIndex(int startIndex, Predicate<T> match)
        {
            return _value.FindLastIndex(startIndex, match);
        }
        public int FindLastIndex(Predicate<T> match)
        {
            return _value.FindLastIndex(match);
        }
        public void ForEach(Action<T> action)
        {
            _value.ForEach(action);
        }
        public List<T>.Enumerator GetEnumerator()
        {
            return _value.GetEnumerator();
        }
        public List<T> GetRange(int index, int count)
        {
            return _value.GetRange(index, count);
        }
        public int IndexOf(T item, int index, int count)
        {
            return _value.IndexOf(item, index, count);
        }
        public int IndexOf(T item, int index)
        {
            return _value.IndexOf(item, index);
        }
        public int IndexOf(T item)
        {
            return _value.IndexOf(item);
        }
        public void Insert(int index, T item)
        {
            _value.Insert(index, item);
            onInsertion?.Invoke(item, index);
            OnChange?.Invoke();
        }
        public void InsertRange(int index, IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                _value.Insert(index, item);
                index++;
            }
        }
        public int LastIndexOf(T item)
        {
            return _value.LastIndexOf(item);
        }
        public int LastIndexOf(T item, int index)
        {
            return _value.LastIndexOf(item, index);
        }
        public int LastIndexOf(T item, int index, int count)
        {
            return _value.LastIndexOf(item, index, count);
        }
        public bool Remove(T item)
        {
            bool removed = _value.Remove(item);
            if (removed)
            {
                onRemoval?.Invoke(item);
                OnChange?.Invoke();
            }
            return removed;
        }
        public int RemoveAll(Predicate<T> match)
        {
            List<T> removeItems = _value.FindAll(match);
            foreach (var item in removeItems)
                Remove(item);

            return removeItems.Count;
        }
        public void RemoveAt(int index)
        {
            T targetItem = _value[index];
            Remove(targetItem);
        }
        public void RemoveRange(int index, int count)
        {
            for (int removals = 0; removals < count; removals++)
                RemoveAt(index);
        }
        /*public void Reverse(int index, int count);
        public void Reverse();
        public void Sort(Comparison<T> comparison);
        public void Sort(int index, int count, IComparer<T> comparer);
        public void Sort();
        public void Sort(IComparer<T> comparer);*/
        public T[] ToArray()
        {
            return _value.ToArray();
        }
        /*public void TrimExcess();
        public bool TrueForAll(Predicate<T> match);*/

    }
}