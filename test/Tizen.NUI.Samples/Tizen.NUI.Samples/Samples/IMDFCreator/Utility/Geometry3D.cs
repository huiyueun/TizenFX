using System.Collections.Generic;
using Tizen.NUI;

namespace Space.BuildingTool.Utility
{
    //TODO - huiyu
    // Check function, but now non-use
    public static class Geometry3D
    {
        public static float CounterClockwiseForPlane(Vector3 dot0, Vector3 dot1, Vector3 dot2, Vector3 normal)
        {
            return 0.0f;
            /*
            Plane plane = new Plane(normal, dot0);

            Vector3 d0 = dot0;
            Vector3 d1 = plane.ClosestPointOnPlane(dot1);
            Vector3 d2 = plane.ClosestPointOnPlane(dot2);

            return Vector3.Dot(Vector3.Cross(d1 - d0, d2 - d1), normal);*/
        }

        public static bool IsClockwise(Vector3 dot0, Vector3 dot1, Vector3 dot2, Vector3 normal)
        {
            return CounterClockwiseForPlane(dot0, dot1, dot2, normal) >= 0.0f;
        }

        public static bool IsCounterClockwise(Vector3 dot0, Vector3 dot1, Vector3 dot2, Vector3 normal)
        {
            return CounterClockwiseForPlane(dot0, dot1, dot2, normal) < 0.0f;
        }

        public static float GetAreaForTriangle(Vector3 dot0, Vector3 dot1, Vector3 dot2)
        {
            return 0.0f;// Vector3.Cross(dot1 - dot0, dot2 - dot1).magnitude / 2.0f;
        }

        public static bool IsPointInTriangle(Vector3 dot0, Vector3 dot1, Vector3 dot2, Vector3 target, float epsilon = 0.0001f)
        {
            /*
            if (dot0 == target || dot1 == target || dot2 == target) return false;

            float triangleArea = GetAreaForTriangle(dot0, dot1, dot2);
            float dot01 = GetAreaForTriangle(dot0, dot1, target);
            float dot12 = GetAreaForTriangle(dot1, dot2, target);
            float dot20 = GetAreaForTriangle(dot2, dot0, target);

            return (dot01 + dot12 + dot20) <= triangleArea + epsilon;*/
            return false;
        }

        public static bool CheckDotsInTriangle(List<Vector3> vertices, int index)
        {
            /*
            Vector3 dot0 = vertices[index + 0];
            Vector3 dot1 = vertices[index + 1];
            Vector3 dot2 = vertices[index + 2];
            for (int i = index + 3; i < vertices.Count; i++)
                if (IsPointInTriangle(dot0, dot1, dot2, vertices[i])) return true;
            */
            return false;
        }

        public static bool IsPointInLine(Vector3 dotA, Vector3 dotB, Vector3 dot, float epsilon = 0.01f)
        {
            return false;
            /*
            float dAB = Vector3.Distance(dotA, dotB);
            float dADot = Vector3.Distance(dotA, dot);
            float dBDot = Vector3.Distance(dotB, dot);

            return ((dAB + epsilon) >= (dADot + dBDot));
            */
        }

        #region Compare Function
        public static int CompareByDistance(Vector3 a, Vector3 b, Vector3 pivot)
        {
            return 0;
            /*float distanceA = Vector3.Distance(a, pivot);
            float distanceB = Vector3.Distance(b, pivot);

            if (distanceA == distanceB) return 0;

            return distanceA < distanceB ? -1 : 1;*/
        }

        public static int CompareByDistanceOnRay(Vector3 a, Vector3 b, Vector3 ray)
        {
            if (a == b) return 0;
            var rayNormal = ray;
            var baNormal = (b - a);
            rayNormal.Normalize();
            baNormal.Normalize();
            return rayNormal == baNormal ? -1 : 1;
        }
        #endregion
    }
}
