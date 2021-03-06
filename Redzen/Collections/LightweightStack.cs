﻿using System;
using System.Diagnostics;

namespace Redzen.Collections
{
    /// <summary>
    /// A lightweight generic stack.
    /// A simpler alternative to Stack<int> that provides additional Poke() and TryPoke() methods.
    /// </summary>
    public sealed class LightweightStack<T>
    {
        #region Fields

        const int __defaultCapacity = 4;
        T[] _array; 
        int _size; 
        
        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public LightweightStack()
        {
            _array = new T[__defaultCapacity];
        }

        /// <summary>
        /// Construct with the given initial capacity.
        /// </summary>
        /// <param name="capacity">Initial capacity.</param>
        public LightweightStack(int capacity)
        {
            if (capacity < 0) {
                throw new ArgumentOutOfRangeException("Capacity must be non-negative.");
            }
            _array = new T[capacity];
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of items on the stack.
        /// </summary>
        public int Count { get { return _size; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Pushes a value onto the top of the stack.
        /// </summary>
        /// <param name="val">The value to push.</param>
        public void Push(T val)
        {
            if (_size == _array.Length) {
                Array.Resize(ref _array, (_array.Length == 0) ? __defaultCapacity : 2 * _array.Length);
            }
            _array[_size++] = val;
        }

        /// <summary>
        /// Pop a value from the top of the stack.
        /// </summary>
        /// <returns>The popped value from the top of the stack.</returns>
        public T Pop()
        {
            if(0 == _size) {
                ThrowForEmptyStack();
            }
            
            return _array[--_size];
        }

        /// <summary>
        /// Attempt to pop a value from the top of the stack.
        /// </summary>
        /// <param name="result">The value from the top of the stack.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public bool TryPop(out T result)
        {
            if(0 == _size)
            {
                result = default(T);
                return false;
            }

            result = _array[--_size];
            return true;
        }

        /// <summary>
        /// Returns the value at the top of the stack without popping it.
        /// </summary>
        /// <returns>The value at the top of the stack.</returns>
        public T Peek()
        {
            if(0 == _size) {
                ThrowForEmptyStack();
            }
            return _array[_size - 1];
        }

        /// <summary>
        /// Returns the value at the top of the stack without popping it, if the stack is not empty.
        /// </summary>
        /// <param name="result">The value at the top of the stack.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public bool TryPeek(out T result)
        {
            if(0 == _size) 
            {
                result = default(T);
                return false;
            }
            result = _array[_size - 1];
            return true;
        }

        /// <summary>
        /// Sets/overwrites he value at the top of the stack.
        /// </summary>
        /// <param name="val">The value to set.</param>
        public void Poke(T val)
        {
            if(0 == _size) {
                ThrowForEmptyStack();
            }
            _array[_size - 1] = val;
        }

        /// <summary>
        /// Sets/overwrites he value at the top of the stack, if the stack is not empty.
        /// </summary>
        /// <param name="val">The value to set.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public bool TryPoke(T val)
        {
            if(0 == _size) {
                return false;
            }
            _array[_size - 1] = val;
            return true;
        }

        /// <summary>
        /// Removes all items from the stack; i.e. moves the stack pointer to the bottom of the stack.
        /// </summary>
        public void Clear()
        {
            // Note. For efficiency the elements of _array are not reset.
            _size = 0;   
        }

        #endregion

        #region Private Methods

        private void ThrowForEmptyStack()
        {
            Debug.Assert(_size == 0);
            throw new InvalidOperationException("Attempt to obtain an item from an empty stack.");
        }

        #endregion
    }
}
