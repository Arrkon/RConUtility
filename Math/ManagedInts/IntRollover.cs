//RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR
//  RConApps		2022-08-12 6:35:34 PM
//	RConUtility
//  IntRollover
//  Integers that over/underflow at specified max/min values
//RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR

using System;
using System.Collections;
using UnityEngine;

namespace RConUtility.Math
{
	[Serializable]
	public class IntRollover : IManagedInt
	{
		#region Public Vars and Properties
		public int Value { get { return _value; } set { _value = SetValue(value); } }
		public int Min { get { return _min; } set { SetMinMax(value, _max); } }
		public int Max { get { return _max; } set { SetMinMax(_min, value); } }
		#endregion

		#region Private Vars and Properties
		[SerializeField] private int _value;
		[SerializeField] private int _min;
		[SerializeField] private int _max;
		#endregion

		#region Initialization
		public IntRollover() { _min = 0; _max = int.MaxValue; _value = _min; }
		public IntRollover(int value) { _min = 0; _max = int.MaxValue; _value = SetValue(value); }
		public IntRollover(int min, int max) { SetMinMax(min, max); _value = _min; }
		public IntRollover(int value, int min, int max) { SetMinMax(min, max); _value = SetValue(value); }
		#endregion

		#region Functions
		public int SetValue(int value)
        {
			int result = value;

			if(value < _min)
			{
				int diff = value - _min;
				result = SetValue(_max + diff + 1);
			}
			else if(value > _max)
			{
				int diff = value - _max;
				result = SetValue(_min + diff - 1);
			}

			return result;
		}

		public void SetMinMax(int a, int b)
		{
			_min = Mathf.Min(a, b);
			_max = Mathf.Max(a, b);
		}
		#endregion

		#region Operator Overloads
		public static IntRollover operator +(IntRollover ir1, IManagedInt ir2)
		{
			return new IntRollover(ir1.Value + ir2.Value, ir1.Min, ir2.Max);
		}

		public static IntRollover operator -(IntRollover ir1, IManagedInt ir2)
		{
			return new IntRollover(ir1.Value + ir2.Value, ir1.Min, ir2.Max);
		}

		public static IntRollover operator +(IntRollover ir, int i)
		{
			return new IntRollover(ir._value + i, ir._min, ir._max);
		}

		public static IntRollover operator -(IntRollover ir, int i)
		{
			return new IntRollover(ir._value - i, ir._min, ir._max);
		}

		public static IntRollover operator +(int i, IntRollover ir)
		{
			return ir._value + i;
		}

		public static IntRollover operator -(int i, IntRollover ir)
		{
			return ir._value - i;
		}

		public static IntRollover operator ++(IntRollover i)
		{
			return new IntRollover(i._value + 1, i._min, i._max);
		}

		public static IntRollover operator --(IntRollover i)
		{
			return new IntRollover(i._value - 1, i._min, i._max);
		}

		public static bool operator ==(int i, IntRollover ir)
		{
			return ir._value == i;
		}

		public static bool operator !=(int i, IntRollover ir)
		{
			return ir._value != i;
		}

		public static bool operator ==(IntRollover ir, int i)
		{
			return ir._value == i;
		}

		public static bool operator !=(IntRollover ir, int i)
		{
			return ir._value != i;
		}

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

		public static bool operator >(int i, IntRollover ir)
        {
			return i > ir._value;
        }

		public static bool operator <(int i, IntRollover ir)
		{
			return i < ir._value;
		}

		public static bool operator >(IntRollover ir, int i)
		{
			return i > ir._value;
		}

		public static bool operator <(IntRollover ir, int i)
		{
			return i < ir._value;
		}

        public override string ToString()
        {
            return _value.ToString();
        }

		public string ToString(IManagedInt.StringFormat format)
		{
			return ToString(format, " ");
		}

		public string ToString(IManagedInt.StringFormat format, string delim)
		{
			return ToString(format, delim, 0);
		}

		public string ToString(IManagedInt.StringFormat format, string delim, int fluff)
		{
			switch(format)
			{
				case IManagedInt.StringFormat.Min:
					return (_min + fluff).ToString();
				case IManagedInt.StringFormat.Min_Val:
					return (_min + fluff).ToString() + delim + (_value + fluff).ToString();
				case IManagedInt.StringFormat.Val:
					return (_value + fluff).ToString();
				case IManagedInt.StringFormat.Max:
					return (_max + fluff).ToString();
				case IManagedInt.StringFormat.Max_Val:
					return (_value + fluff).ToString() + delim + (_max + fluff).ToString();
				case IManagedInt.StringFormat.Full:
					return (_min + fluff).ToString() + delim + (_value + fluff).ToString() + delim + (_max + fluff).ToString();
				default:
					return _value.ToString();
			}
		}

		public static implicit operator float(IntRollover ir)
		{
			return (float)ir._value;
		}

		public static implicit operator int(IntRollover ir)
		{
			return ir._value;
		}

		public static implicit operator IntRollover(float f)
		{
			return new IntRollover((int)f);
		}

		public static implicit operator IntRollover(int i)
		{
			return new IntRollover(i);
		}
		#endregion
	}
}