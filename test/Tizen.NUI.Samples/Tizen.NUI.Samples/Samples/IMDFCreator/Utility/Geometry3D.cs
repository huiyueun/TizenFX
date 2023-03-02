using System;
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
            System.Numerics.Vector3 cNormal = new System.Numerics.Vector3(normal.X, normal.Y, normal.Z);
            System.Numerics.Vector3 cDot1 = new System.Numerics.Vector3(dot0.X, dot0.Y, dot0.Z);
            System.Numerics.Vector3 cDot2 = new System.Numerics.Vector3(dot1.X, dot1.Y, dot1.Z);
            System.Numerics.Vector3 cDot3 = new System.Numerics.Vector3(dot2.X, dot2.Y, dot2.Z);

            var plane = System.Numerics.Plane.CreateFromVertices(cDot1, cDot2, cDot3);

            System.Numerics.Vector3 d0 = new System.Numerics.Vector3(dot0.X, dot0.Y, dot0.Z);
            System.Numerics.Vector3 d1 = ClosestPointOnPlane(plane, dot1);
            System.Numerics.Vector3 d2 = ClosestPointOnPlane(plane, dot2);

            return System.Numerics.Vector3.Dot(System.Numerics.Vector3.Cross(d1 - d0, d2 - d1), cNormal);
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
            var cDot0 = new System.Numerics.Vector3(dot0.X, dot0.Y, dot0.Z);
            var cDot1 = new System.Numerics.Vector3(dot1.X, dot1.Y, dot1.Z);
            var cDot2 = new System.Numerics.Vector3(dot2.X, dot2.Y, dot2.Z);

            return MyMagnitute(System.Numerics.Vector3.Cross(cDot1 - cDot0, cDot2 - cDot1)) / 2.0f;
        }

        public static bool IsPointInTriangle(Vector3 dot0, Vector3 dot1, Vector3 dot2, Vector3 target, float epsilon = 0.0001f)
        {
            if (dot0 == target || dot1 == target || dot2 == target) return false;

            float triangleArea = GetAreaForTriangle(dot0, dot1, dot2);
            float dot01 = GetAreaForTriangle(dot0, dot1, target);
            float dot12 = GetAreaForTriangle(dot1, dot2, target);
            float dot20 = GetAreaForTriangle(dot2, dot0, target);

            return (dot01 + dot12 + dot20) <= triangleArea + epsilon;
        }

        public static bool CheckDotsInTriangle(List<Vector3> vertices, int index)
        {
            Vector3 dot0 = vertices[index + 0];
            Vector3 dot1 = vertices[index + 1];
            Vector3 dot2 = vertices[index + 2];
            for (int i = index + 3; i < vertices.Count; i++)
                if (IsPointInTriangle(dot0, dot1, dot2, vertices[i])) return true;
            
            return false;
        }

        public static bool IsPointInLine(Vector3 dotA, Vector3 dotB, Vector3 dot, float epsilon = 0.01f)
        {
            float dAB = DistanceOfVector(dotA, dotB);
            float dADot = DistanceOfVector(dotA, dot);
            float dBDot = DistanceOfVector(dotB, dot);

            return ((dAB + epsilon) >= (dADot + dBDot));
        }

        #region Compare Function
        public static int CompareByDistance(Vector3 a, Vector3 b, Vector3 pivot)
        {
            float distanceA = DistanceOfVector(a, pivot);
            float distanceB = DistanceOfVector(b, pivot);

            if (distanceA == distanceB) return 0;

            return distanceA < distanceB ? -1 : 1;
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

        public static float MyMagnitute(System.Numerics.Vector3 v)
        {
            return (float)Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
        }

        public static System.Numerics.Vector3 ClosestPointOnPlane(System.Numerics.Plane plane, Vector3 point)
        {
            System.Numerics.Vector3 cPoint = new System.Numerics.Vector3(point.X, point.Y, point.Z);
            float distance = System.Numerics.Plane.DotCoordinate(plane, cPoint) - plane.D;
            var cResult = cPoint - distance * plane.Normal;
            return cResult;
        }

        public static float DistanceOfVector(Vector3 v1, Vector3 v2)
        {
            var cVec1 = new System.Numerics.Vector3(v1.X, v1.Y, v1.Z);
            var cVec2 = new System.Numerics.Vector3(v2.X, v2.Y, v2.Z);

            return System.Numerics.Vector3.Distance(cVec1, cVec2);
        }
    }
}
