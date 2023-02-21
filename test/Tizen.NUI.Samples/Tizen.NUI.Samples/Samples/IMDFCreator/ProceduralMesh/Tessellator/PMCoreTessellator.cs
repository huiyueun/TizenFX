using System.Collections.Generic;
using System;
using Space.BuildingTool.Utility;
using Tizen.NUI;

namespace Space.BuildingTool.ProceduralMesh
{
    [Obsolete]
    public class PMCoreTessellator : ITessellator
    {
        public MeshDraft CreatePolygon(IList<Vector3> vertices, IList<IList<Vector3>> holeVertices = null)
        {
            if (vertices.Count < 3)
            {
                return new MeshDraft();
            }

            //Vector3 normal = new Plane(vertices[0], vertices[1], vertices[2]).normal;
            List<Vector3> verticesWithHoles = new List<Vector3>(vertices);

            if (holeVertices != null)
            {
                foreach (List<Vector3> hole in holeVertices)
                {
                    verticesWithHoles = GetMergeVertices(verticesWithHoles, hole);
                }
            }

            (List<Vector3> rightVertices, List<int> rightTriangles) = GetMeshInfoForPlane(verticesWithHoles, Vector3.One);

            return new MeshDraft(rightVertices, rightTriangles);
        }

        private (List<Vector3> vertices, List<int> triangles) GetMeshInfoForPlane(List<Vector3> clockwiseVertices, Vector3 normal)
        {
            int triangleCount = clockwiseVertices.Count - 2;
            List<int> triangles = new List<int>();
            List<Vector3> vertices = new List<Vector3>();
            Dictionary<Vector3, int> indexDictionary = new Dictionary<Vector3, int>();

            List<Vector3> copy = new List<Vector3>(clockwiseVertices);
            for (int i = 0; i < triangleCount; i++)
            {
                for (int k = 0; k < copy.Count - 2; k++)
                {
                    bool isClockWise = true;// Geometry3D.IsClockwise(copy[k], copy[k + 1], copy[k + 2], normal);
                    bool isDotsInTriangle = false;// Geometry3D.CheckDotsInTriangle(copy, k);

                    if (isClockWise == true && isDotsInTriangle == false)
                    {
                        for (int t = 0; t < 3; t++)
                        {
                            if (indexDictionary.ContainsKey(copy[k + t])) continue;

                            indexDictionary[copy[k + t]] = vertices.Count;
                            vertices.Add(copy[k + t]);
                        }

                        for (int t = 0; t < 3; t++)
                            triangles.Add(indexDictionary[copy[k + t]]);

                        copy.RemoveAt(k + 1);

                        break;
                    }
                }
            }

            return (vertices, triangles);
        }

        #region Mesh with Hole
        private List<Vector3> GetMergeVertices(List<Vector3> clockwiseVertices, List<Vector3> counterClockWiseHole)
        {
            Dictionary<Vector3, int> verticesCounter = GetValueCounter(clockwiseVertices);
            (Vector3 dot0, Vector3 dot1) = GetShortestDots(clockwiseVertices, counterClockWiseHole, verticesCounter);

            return MergeVerticesWithHole(clockwiseVertices, counterClockWiseHole, new Vector3[] { dot0, dot1 });
        }

        private List<Vector3> MergeVerticesWithHole(List<Vector3> vertices, List<Vector3> holes, Vector3[] selectedDots)
        {
            List<Vector3> sortVertices = SortByStartPoint(vertices, selectedDots[0]);
            List<Vector3> sortHoles = SortByStartPoint(holes, selectedDots[1]);
            List<Vector3> merge = new List<Vector3>();

            foreach (Vector3 v in sortVertices) merge.Add(v);
            merge.Add(sortVertices[0]);

            foreach (Vector3 v in sortHoles) merge.Add(v);
            merge.Add(sortHoles[0]);

            return merge;
        }

        private bool IsDuplicated(Dictionary<Vector3, int> valueCounter, Vector3 value)
        {
            if (valueCounter.ContainsKey(value) == false) return false;
            if (valueCounter[value] == 1) return false;

            return true;
        }

        private List<Vector3> SortByStartPoint(IList<Vector3> list, Vector3 start)
        {
            List<Vector3> sortList = new List<Vector3>();
            List<Vector3> tmpDouble = new List<Vector3>(list);
            int count = list.Count;

            foreach (Vector3 v in list) tmpDouble.Add(v);

            /*
            int index;
            for (index = 0; index < list.Count; index++)
                if (list[index] == start) break;

            for (int i = index; i < index + count; i++) sortList.Add(tmpDouble[i]);
            */
            return sortList;
        }

        private (Vector3 dot0, Vector3 dot1) GetShortestDots(IList<Vector3> vertices, IList<Vector3> holes, Dictionary<Vector3, int> exception = null)
        {
            float minValue = float.MaxValue;

            Vector3 dot0, dot1;

            dot0 = dot1 = Vector3.Zero;
            foreach (Vector3 v0 in vertices)
            {
                foreach (Vector3 v1 in holes)
                {
                    if (exception != null
                        && (IsDuplicated(exception, v0) || IsDuplicated(exception, v1))) continue;

                    float distance = 0;// Vector3.Distance(v0, v1);
                    if (distance < minValue)
                    {
                        minValue = distance;
                        dot0 = v0;
                        dot1 = v1;
                    }
                }
            }

            return (dot0, dot1);
        }

        private Dictionary<Vector3, int> GetValueCounter(List<Vector3> vertices)
        {
            Dictionary<Vector3, int> counter = new Dictionary<Vector3, int>();

            foreach (Vector3 v in vertices)
            {
                if (counter.ContainsKey(v)) counter[v]++;
                else counter[v] = 1;
            }

            return counter;
        }
        #endregion
    }
}
