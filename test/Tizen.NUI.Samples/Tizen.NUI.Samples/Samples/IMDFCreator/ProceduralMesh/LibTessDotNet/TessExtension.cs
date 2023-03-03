using System.Collections.Generic;
using LibTessDotNet;
using Tizen.NUI;

namespace Space.BuildingTool.ProceduralMesh
{
    internal static class TessExtension
    {
        public static List<Vector2> ToVector2(this IList<Vec3> vecs)
        {
            List<Vector2> result = new List<Vector2>();
            foreach (var vec in vecs)
            {
                result.Add(new Vector2(vec.X, vec.Y));
            }

            return result;
        }

        public static List<Vector3> ToVector3(this IList<ContourVertex> vecs)
        {
            List<Vector3> result = new List<Vector3>();
            foreach (var vec in vecs)
            {
                result.Add(vec.ToVector3());
            }

            return result;
        }

        public static Vector3 ToVector3(this ContourVertex vec)
        {
            return new Vector3(vec.Position.X, vec.Position.Y, vec.Position.Z);
        }

        public static void AddContour(this Tess tess, IList<Vector3> vertices, ContourOrientation forceOrientation = ContourOrientation.Original)
        {
            var contour = new ContourVertex[vertices.Count];
            for (int i = 0; i < vertices.Count; i++)
            {
                Vector3 vertex = vertices[i];
                contour[i].Position = new Vec3(vertex.X, vertex.Y, vertex.Z);
            }
            tess.AddContour(contour, forceOrientation);
        }
    }
}
