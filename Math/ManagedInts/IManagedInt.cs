//RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR
//  RConApps		2022-08-12 10:29:29 PM
//	RConUtility
//  IManagedInt
//  Interface for integers that are governed by min/max values
//RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR

using System;
using System.Collections;
using UnityEngine;

namespace RConUtility.Math
{
	public interface IManagedInt
	{
		#region Public Vars and Properties
		public enum StringFormat { Min, Min_Val, Val, Max, Max_Val, Full }
		public int Value { get; set; }
		public int Min { get; set; }
		public int Max { get; set; }
		#endregion

		#region Functions
		public int SetValue(int value);
		public void SetMinMax(int a, int b);

		public string ToString(StringFormat format);
		public string ToString(StringFormat format, string delim);
		public string ToString(StringFormat format, string delim, int fluff);
		#endregion
	}
}