//RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR
//  RConApps		2022-08-11 10:34:00 PM
//	RConUtility
//  IntVector3
//  Vector 3 that uses integers instead of floats
//RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR

using UnityEngine;

namespace RConUtility.Math
{
	public class IntVector3 : IIntVector
	{
        #region Public Vars and Properties
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Z { get => z; set => z = value; }
        #endregion

        #region Private Vars and Properties
        private int x, y, z;
        #endregion

        public IntVector3() { x = y = z = 0; }

        public IntVector3(int value) { x = y = z = value; }

        public IntVector3(int X, int Y, int Z) { x = X; y = Y; z = Z; }

        public IntVector3(float X, float Y, float Z) { x = (int)X; y = (int)Y; z = (int)Z; }

        #region Functions
        public float Distance(IIntVector from, IIntVector to)
        {
            Vector3 direction = new Vector3(to.X - from.X, to.Y - from.Y, to.Z - from.Z);
            return direction.magnitude;
        }

        public float Magnitude()
        {
            return Mathf.Sqrt(this.MagnitudeSqr());
        }
        public int MagnitudeSqr()
        {
            return x * x + y * y + z * z;
        }

        public int DistanceHigh(IIntVector from, IIntVector to)
        {
            return (int)Mathf.Ceil(Distance(from, to));
        }

        public int MagnitudeHigh()
        {
            return (int)Mathf.Ceil(Magnitude());
        }

        public int DistanceLow(IIntVector from, IIntVector to)
        {
            return (int)Mathf.Floor(Distance(from, to));
        }

        public int MagnitudeLow()
        {
            return (int)Mathf.Floor(Magnitude());
        }

        public int DistanceNear(IIntVector from, IIntVector to)
        {
            return (int)Mathf.Round(Distance(from, to));
        }

        public int MagnitudeNear()
        {
            return (int)Mathf.Round(Magnitude());
        }
        #endregion

        #region Conversions
        public static implicit operator float(IntVector3 v)
        {
            return v.Magnitude();
        }

        public static implicit operator Vector3(IntVector3 v)
        {
            return new Vector3(v.x, v.y, v.z);
        }

        public static implicit operator IntVector2(IntVector3 v)
        {
            return new IntVector2(v.x, v.y);
        }

        public static implicit operator Vector2(IntVector3 v)
        {
            return new Vector2(v.x, v.y);
        }

        public static implicit operator IntVector3(Vector3 v)
        {
            return new IntVector3(v.x, v.y, v.z);
        }


        public static bool operator ==(IntVector3 v1, Vector3 v2)
        {
            //if (v1.Equals(null))
            //    return v2.Equals(null);

            //if (v2.Equals(null))
            //    return v1.Equals(null);

            return v1.x == v2.x && v1.y == v2.y && v1.z == v2.z;
        }

        public static bool operator !=(IntVector3 v1, Vector3 v2)
        {
            //if (v1.Equals(null))
            //    return !v2.Equals(null);

            //if (v2.Equals(null))
            //    return !v1.Equals(null);

            return v1.x != v2.x || v1.y != v2.y || v1.z != v2.z;
        }

        public static bool operator ==(IntVector3 v1, IIntVector v2)
        {
            return v1.x == v2.X && v1.y == v2.Y && v1.z == v2.Z;
        }
        public static bool operator !=(IntVector3 v1, IIntVector v2)
        {
            if(v2.Z == 0) return true;

            return v1.x != v2.X || v1.y != v2.Y || v1.z != v2.Z;
        }

        public override bool Equals(object obj)
        {
            if(obj.GetType() != typeof(IIntVector)) return false;

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "IntVector2: ( " + x.ToString() + " , " + y.ToString() + ")";
        }
        #endregion
    }
}