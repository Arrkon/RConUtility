//RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR
//  RConApps		2022-08-11 9:56:36 PM
//	RConUtility
//  IIntVector
//  Interface for integer based Vectors
//RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR

using System;
using UnityEngine;

namespace RConUtility.Math
{
	public interface IIntVector
	{
        #region Properties
        public abstract int X { get; set; }
		public abstract int Y { get; set; }
		public abstract int Z { get; set; }
        #endregion

        #region Functions
        public float Distance(IIntVector from, IIntVector to);
		public float Magnitude();
		public int MagnitudeSqr();
		public int DistanceHigh(IIntVector from, IIntVector to);
		public int MagnitudeHigh();
		public int DistanceLow(IIntVector from, IIntVector to);
		public int MagnitudeLow();
		public int DistanceNear(IIntVector from, IIntVector to);
		public int MagnitudeNear();
		#endregion
	}
}