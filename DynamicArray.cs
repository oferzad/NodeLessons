using System;
using System.Collections.Generic;
using System.Text;
using DataStructureCore;

namespace NodeLessons
{
    class DynamicArray<T>
    {
        private int size, emptyCells;
        private Node<T> cells;

        public DynamicArray(int size)
        {
            if (size >= 0)
            {
                this.cells = null;
                this.size = size;
                this.emptyCells = size;
                for (int i = 0; i < size; i++)
                {
                    this.cells = new Node<T>(default(T), this.cells);
                }
            }
            else
                throw new ArgumentOutOfRangeException("size", "Size cannot be negative!");
        }

        public void Set(int index, T value)
        {
            if (index < size)
            {
                Node<T> pos = this.cells;
                for (int i = 0; i < index; i++)
                {
                    pos = pos.GetNext();
                }
                
                if (pos.GetValue().Equals(default(T)) && !value.Equals(default(T)))
                    this.emptyCells--;

                pos.SetValue(value);
            }
            else
                throw new ArgumentOutOfRangeException("index", "Index is out of range");
        }

        public T Get(int index)
        {
            if (index < size)
            {
                Node<T> pos = this.cells;
                for (int i = 0; i < index; i++)
                {
                    pos = pos.GetNext();
                }
                return pos.GetValue();
            }
            else
                throw new ArgumentOutOfRangeException("index", "Index is out of range");
        }

        public void Resize(int newSize)
        {
            if (newSize == 0)
            {
                this.cells = null;
                this.size = 0;
                this.emptyCells = 0;
            }
            else if (newSize < size)
            {
                Node<T> pos = this.cells;
                for (int i = 0; i < newSize - 1; i++)
                {
                    pos = pos.GetNext();
                }
                Node<T> pos2 = pos.GetNext();
                pos.SetNext(null);
                while (pos2 != null)
                {
                    if (pos2.GetValue().Equals(default(T)))
                        this.emptyCells--;
                    pos2 = pos2.GetNext();
                }

                this.size = newSize;
            }
            else
            {
                Node<T> pos = this.cells;
                while (pos.HasNext())
                    pos = pos.GetNext();
                for (int i = 0; i < newSize - this.size; i++)
                {
                    pos.SetNext(new Node<T>(default(T)));
                    this.emptyCells++;
                    pos = pos.GetNext();
                }
                this.size = newSize;
                
            }
        }
        public int GetLength()
        {
            return this.size;
        }

        public int Length
        {
            get
            {
                return this.size;
            }
        }

        public int EmptyCells()
        {
            return this.emptyCells;
        }

        public T this[int index]
        {
            get
            {
                return this.Get(index);
            }
            set
            {
                this.Set(index, value);
            }
        }
    }
}
