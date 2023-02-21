using System.Collections.Generic;
using Space.BuildingTool.ExtensionMethod;
using Tizen.NUI;

namespace Space.BuildingTool.ProceduralMesh
{
    public class MeshDraft
    {
        public List<Vector3> vertices;
        public List<int> triangles;

        public MeshDraft()
        {
            vertices = new List<Vector3>();
            triangles = new List<int>();
        }

        public MeshDraft(IList<Vector3> vertices, IList<int> triangles)
        {
            this.vertices = new List<Vector3>(vertices);
            this.triangles = new List<int>(triangles);
        }

        public MeshDraft ReverseTriangles(int startIndex, int count)
        {
            if (count % 3 != 0)
            {
                throw new System.Exception("count must be multiple of 3 !");
            }

            for (int i = startIndex; i < startIndex + count; i += 3)
            {
                int tmp = triangles[i];
                triangles[i] = triangles[i + 1];
                triangles[i + 1] = tmp;
            }

            return this;
        }

        private List<(int, int)> GetLinesOnBoundary()
        {
            List<(int, int)> linesOnBoundary = new List<(int, int)>();
            Dictionary<(int, int), int> lineCounts = new Dictionary<(int, int), int>();

            for (int i = 0; i < triangles.Count; i += 3)
            {
                for (int j = 0; j < 3; j++)
                {
                    int start = triangles[i + j];
                    int end = triangles[i + (j + 1) % 3];

                    if (lineCounts.ContainsKey((end, start)))
                    {
                        lineCounts[(end, start)]++;
                    }
                    else
                    {
                        lineCounts[(start, end)] = 1;
                    }
                }
            }

            foreach (var line in lineCounts)
            {
                if (line.Value == 1)
                {
                    linesOnBoundary.Add(line.Key);
                }
            }

            return linesOnBoundary;
        }


        public MeshDraft SpreadOut(Vector3 normal, float thickness)
        {
            int nOriginalVertices = vertices.Count;
            int nOriginalTriangles = triangles.Count;

            var linesOnBoundary = GetLinesOnBoundary();

            // // First nOriginalVertices are right plane of wall.
            // // Last nOriginalVertices are left plane of wall.
            vertices.AddRange(vertices.GetRange(0, nOriginalVertices));
            triangles.AddRange(triangles.GetRange(0, nOriginalTriangles));

            Vector3 moveVector = normal * thickness;
            vertices.AddValue(moveVector, 0, nOriginalVertices);
            vertices.AddValue(-moveVector, nOriginalVertices, nOriginalVertices);
            triangles.AddValue(nOriginalVertices, nOriginalTriangles, nOriginalTriangles);
            ReverseTriangles(nOriginalTriangles, nOriginalTriangles);

            foreach (var line in linesOnBoundary)
            {
                int v0 = line.Item2;
                int v0Opposite = v0 + nOriginalVertices;

                int v1 = line.Item1;
                int v1Opposite = v1 + nOriginalVertices;

                // TODO(ilwoo1.kwon) : I want to use these lines instead of below.
                // But with these, Z-Fighting is occured. I can't understand what difference is.

                // md.triangles.AddRange(new List<int> { v0Opposite, v0, v1Opposite });
                // md.triangles.AddRange(new List<int> { v0, v1, v1Opposite });

                vertices.Add(vertices[v0]);
                vertices.Add(vertices[v0Opposite]);

                vertices.Add(vertices[v1]);
                vertices.Add(vertices[v1Opposite]);

                triangles.AddRange(new List<int> { vertices.Count - 3, vertices.Count - 4, vertices.Count - 1 });
                triangles.AddRange(new List<int> { vertices.Count - 4, vertices.Count - 2, vertices.Count - 1 });
            }

            return this;
        }

        public void FillMesh(/*Mesh mesh, */bool calcuateBounds = true, bool calculateNormals = true)
        {
            /*
            mesh.Clear();
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();

            if (calcuateBounds)
            {
                mesh.RecalculateBounds();
            }
            if (calculateNormals)
            {
                mesh.RecalculateNormals();
            }
            */
        }
    }
}
