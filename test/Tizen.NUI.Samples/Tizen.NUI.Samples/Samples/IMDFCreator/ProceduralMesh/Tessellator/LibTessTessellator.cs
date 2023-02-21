using System.Collections.Generic;
using LibTessDotNet;
using Tizen.NUI;

namespace Space.BuildingTool.ProceduralMesh
{
    public class LibTessTessellator : ITessellator
    {
        public MeshDraft CreatePolygon(IList<Vector3> vertices, IList<IList<Vector3>> holeVertices = null)
        {
            if (vertices.Count < 3)
            {
                return new MeshDraft();
            }

            Tess tess = new Tess();
            tess.AddContour(vertices, ContourOrientation.Original);

            if (holeVertices != null)
            {
                foreach (var hole in holeVertices)
                {
                    tess.AddContour(hole, ContourOrientation.Original);
                }
            }
            tess.Tessellate();
            return new MeshDraft(tess.Vertices.ToVector3(), tess.Elements);
        }
    }
}
