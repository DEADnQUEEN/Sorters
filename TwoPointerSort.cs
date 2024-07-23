using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorters
{
    public class TwoPointerSort
    {
        private class Dot
        {
            public readonly double _value;
            private Dot? _left = null;
            private Dot? _right = null;
            public Dot? Right { get { return _right; } set { _right = value; if (value is not null) { value._left = this; } } }
            public Dot? Left { get { return _left; } set { _left = value; if (value is not null) { value._right = this; } } }

            public Dot(double value)
            {
                this._value = value;
            }

            public void InsertLeft(Dot dot)
            {
                dot._left = this._left;
                dot._right = this;

                if (this._left is not null)
                    this._left._right = dot;

                this._left = dot;
            }

            public void InsertRight(Dot dot)
            {
                dot._right = this._right;
                dot._left = this;

                if (this._right is not null)
                    this._right._left = dot;

                this._right = dot;
            }

            public override string ToString()
            {
                string text = string.Empty;
                if (this._left is not null)
                    text += _left._value;
                else
                    text += "null";
                text += " --> " + _value.ToString() + " --> ";

                if (this._right is not null)
                    text += this._right._value;
                else
                    text += "null";

                return text;
            }
        }

        private class DotPointers
        {
            public Dot? _left = null;
            public Dot? _right = null;

            public DotPointers() { }
            public DotPointers(Dot startdot) { this._left = startdot; this._right = startdot; }
            public DotPointers(Dot left, Dot right) { this._left = left; this._right = right; }

            public override string ToString()
            {
                return $"{_left}, {_right}";
            }
        }

        private DotPointers pointers = new DotPointers();

        private Dot? _startDot = null;

        private double[] _values = Array.Empty<double>();
        public double[] Values
        {
            get => _values;
            set
            {
                _values = value;
                Sort(ConvertToDots(value));
            }
        }

        private Dot[] ConvertToDots(double[] values)
        {
            List<Dot> dots = new List<Dot>();
            if (values.Length == 0) return Array.Empty<Dot>();

            for (int i = 0; i < values.Length; i++)
            {
                dots.Add(new Dot(values[i]));
            }
            
            this.pointers._left = dots[0];
            this.pointers._right = dots[dots.Count - 1];

            return dots.ToArray();
        }

        private void Insert(Dot dot)
        {
            ArgumentNullException.ThrowIfNull(this.pointers._left);
            ArgumentNullException.ThrowIfNull(this.pointers._right);

            if (this.pointers._left._value > this.pointers._right._value) throw new InvalidOperationException();

            if (dot._value < (this.pointers._left._value + this.pointers._right._value) / 2)
            {
                if (dot._value < this.pointers._left._value)
                {
                    while (this.pointers._left.Left is not null && dot._value < this.pointers._left.Left._value)
                    {
                        pointers._left = pointers._left.Left;
                    }

                    pointers._left.InsertLeft(dot);
                    pointers._left = pointers._left.Left;
                }
                else
                {
                    while (this.pointers._left.Right is not null && dot._value > this.pointers._left.Right._value)
                    {
                        pointers._left = pointers._left.Right;
                    }

                    pointers._left.InsertRight(dot);
                    pointers._left = pointers._left.Right;
                }
            }
            else
            {
                if (dot._value < this.pointers._right._value)
                {
                    while (this.pointers._right.Left is not null && dot._value < this.pointers._right.Left._value)
                    {
                        pointers._right = pointers._right.Left;
                    }

                    pointers._right.InsertLeft(dot);
                    pointers._right = pointers._left.Left;
                }
                else
                {
                    while (this.pointers._right.Right is not null && dot._value > this.pointers._right.Right._value)
                    {
                        pointers._right = pointers._right.Right;
                    }

                    pointers._right.InsertRight(dot);
                    pointers._right = pointers._left.Right;
                }
            }
        }

        private void Sort(Dot[] dots)
        {
            if (dots.Length == 0) return;

            this.pointers = new DotPointers(dots[0]);
            this._startDot = dots[0];

            for (int i = 1; i < dots.Length; i++)
            {
                this.Insert(dots[i]);
            }
        }

        public void PrintSequence()
        {
            Console.Write("null");
            if (_startDot is null) return;

            Dot? dot = _startDot;

            while (dot is not null)
            {
                Console.Write(" --> " + dot._value.ToString());
                dot = dot.Right;
            }
            Console.WriteLine(" --> null");
        }

        public TwoPointerSort(double[] values)
        {

            Dot[] dots = ConvertToDots(values);

            this.Values = values;
        }
    }
}
