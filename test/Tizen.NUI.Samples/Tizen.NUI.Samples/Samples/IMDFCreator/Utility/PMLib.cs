using System.Collections.Generic;
using Tizen.NUI;

namespace Space.BuildingTool.Utility
{
    public static class PMLib
    {
        #region For PM

        // return {topLeft, topRight, bottomRight, bottomLeft}
        public static List<Vector3> ApplyHeight(Vector3 topLeft, Vector3 topRight,
            float heightOfTop, float heightOfBottom = 0f)
        {
            Vector3 v0 = new Vector3(topLeft.X, heightOfTop, topLeft.Z);
            Vector3 v1 = new Vector3(topLeft.X, heightOfBottom, topLeft.Z);

            Vector3 v2 = new Vector3(topRight.X, heightOfTop, topRight.Z);
            Vector3 v3 = new Vector3(topRight.X, heightOfBottom, topRight.Z);

            Tizen.Log.Error("MYLOG", $"ApplyHeight v0 {v0.X} {v0.Y} {v0.Z}\n");
            Tizen.Log.Error("MYLOG", $"ApplyHeight v2 {v2.X} {v2.Y} {v2.Z}\n");
            Tizen.Log.Error("MYLOG", $"ApplyHeight v3 {v3.X} {v3.Y} {v3.Z}\n");
            Tizen.Log.Error("MYLOG", $"ApplyHeight v1 {v1.X} {v1.Y} {v1.Z}\n");
            
            return new List<Vector3> { v0, v2, v3, v1 };
        }
        #endregion

        public static void Swap<T>(ref T a, ref T b)
        {
            T tmp = a;
            a = b;
            b = tmp;
        }

        public static void SortByDistanceOnRay(ref Vector3 a, ref Vector3 b, Vector3 ray)
        {
            if (Geometry3D.CompareByDistanceOnRay(a, b, ray) == 1)
            {
                Swap(ref a, ref b);
            }
        }

        public static void ClippingSegmentBySegment(Vector3 pivotLeft, Vector3 pivotRight, ref Vector3 left, ref Vector3 right)
        {
            var leftToRightDirection = pivotRight - pivotLeft;
            bool isLeftOut = Geometry3D.CompareByDistanceOnRay(left, pivotLeft, leftToRightDirection) == -1;
            bool isRightOut = Geometry3D.CompareByDistanceOnRay(pivotRight, right, leftToRightDirection) == -1;

            if (isLeftOut)
            {
                left = pivotLeft;
            }

            if (isRightOut)
            {
                right = pivotRight;
            }
        }
    }
}
