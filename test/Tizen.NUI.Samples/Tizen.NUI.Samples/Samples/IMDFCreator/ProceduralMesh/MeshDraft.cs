using System.Collections.Generic;
using Space.BuildingTool.ExtensionMethod;
using Tizen.NUI;

namespace Space.BuildingTool.ProceduralMesh
{
    public class MeshDraft
    {
        public List<Vector3> vertices;
        public List<int> triangles;
        public List<Vector3> normals;

        public Vector3 normal;

        public MeshDraft()
        {
            vertices = new List<Vector3>();
            triangles = new List<int>();
        }

        public MeshDraft(IList<Vector3> vertices, IList<int> triangles)
        {
            this.vertices = new List<Vector3>(vertices);
            this.normals = new List<Vector3>(vertices);
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
            Tizen.Log.Error("MYLOG", "----------SpreadOut---------------------\n");
            int nOriginalVertices = vertices.Count;
            int nOriginalTriangles = triangles.Count;

            var linesOnBoundary = GetLinesOnBoundary();

            Tizen.Log.Error("MYLOG", "Start Vertices Count : " + vertices.Count + "\n");
            // // First nOriginalVertices are right plane of wall.
            // // Last nOriginalVertices are left plane of wall.
            vertices.AddRange(vertices.GetRange(0, nOriginalVertices));
            normals.AddRange(normals.GetRange(0, nOriginalVertices));
            triangles.AddRange(triangles.GetRange(0, nOriginalTriangles));


            Tizen.Log.Error("MYLOG", "Step1. Vertices Count : " + vertices.Count + "\n");
            Vector3 moveVector = normal * thickness;
            Tizen.Log.Error("MYLOG", $"Vert0 :{vertices[0].X }, {vertices[0].Y},{vertices[0].Z} " + "\n");
            Tizen.Log.Error("MYLOG", $"Vert1 :{vertices[1].X }, {vertices[1].Y},{vertices[1].Z} " + "\n");
            Tizen.Log.Error("MYLOG", $"Vert2 :{vertices[2].X }, {vertices[2].Y},{vertices[2].Z} " + "\n");
            Tizen.Log.Error("MYLOG", $"Vert3 :{vertices[3].X }, {vertices[3].Y},{vertices[3].Z} " + "\n");
            Tizen.Log.Error("MYLOG", $"MoveVector :{moveVector.X }, {moveVector.Y},{moveVector.Z} " + "\n");

            vertices.AddValue(moveVector, 0, nOriginalVertices);
            normals.AddValue(normal, 0, nOriginalVertices);
            Tizen.Log.Error("MYLOG", "Step2. Vertices Count : " + vertices.Count + "\n");

            vertices.AddValue(-moveVector, nOriginalVertices, nOriginalVertices);
            normals.AddValue(-normal, nOriginalVertices, nOriginalVertices);
            Tizen.Log.Error("MYLOG", "Step3. Vertices Count : " + vertices.Count + "\n");
            Tizen.Log.Error("MYLOG", $"Step3. Normal :{normal.X }, {normal.Y},{normal.Z} "+ "\n");

            triangles.AddValue(nOriginalVertices, nOriginalTriangles, nOriginalTriangles);
            ReverseTriangles(nOriginalTriangles, nOriginalTriangles);


            Tizen.Log.Error("MYLOG", $"LineCount : {linesOnBoundary.Count}" + "\n");
            foreach (var line in linesOnBoundary)
            {
                Tizen.Log.Error("MYLOG", $"------------------------------------" + "\n");
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

                Tizen.Log.Error("MYLOG", $"Vert0 :{vertices[v0].X }, {vertices[v0].Y},{vertices[v0].Z} " + "\n");
                Tizen.Log.Error("MYLOG", $"Vert1 :{vertices[v0Opposite].X }, {vertices[v0Opposite].Y},{vertices[v0Opposite].Z} " + "\n");
                Tizen.Log.Error("MYLOG", $"Vert2 :{vertices[v1].X }, {vertices[v1].Y},{vertices[v1].Z} " + "\n");
                Tizen.Log.Error("MYLOG", $"Vert3 :{vertices[v1Opposite].X }, {vertices[v1Opposite].Y},{vertices[v1Opposite].Z} " + "\n");

                var sideNormal = GetPlaneNormal(vertices[v0], vertices[v0Opposite], vertices[v1]);
                normals.Add(sideNormal);
                normals.Add(sideNormal);
                normals.Add(sideNormal);
                normals.Add(sideNormal);
                Tizen.Log.Error("MYLOG", $"SideNormal :{sideNormal.X }, {sideNormal.Y},{sideNormal.Z} " + "\n");

                triangles.AddRange(new List<int> { vertices.Count - 3, vertices.Count - 4, vertices.Count - 1 });
                triangles.AddRange(new List<int> { vertices.Count - 4, vertices.Count - 2, vertices.Count - 1 });
                Tizen.Log.Error("MYLOG", $"------------------------------------" + "\n");
            }
            Tizen.Log.Error("MYLOG", "Step4. Vertices Count : " + vertices.Count + "\n");
            Tizen.Log.Error("MYLOG", "Step4. Normals Count : " + normals.Count + "\n");
            this.normal = normal;
            Tizen.Log.Error("MYLOG", "-******Finish---------SpreadOut---------------------\n");
            return this;
        }

        public Vector3 GetPlaneNormal(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            var cv1 = new System.Numerics.Vector3(v1.X, v1.Y, v1.Z);
            var cv2 = new System.Numerics.Vector3(v2.X, v2.Y, v2.Z);
            var cv3 = new System.Numerics.Vector3(v3.X, v3.Y, v3.Z);

            var plane = System.Numerics.Plane.CreateFromVertices(cv1, cv2, cv3);
            var normal = plane.Normal;
            return new Vector3(normal.X, normal.Y, normal.Z);
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
