using System.Collections.Generic;
using Tizen.NUI;

namespace Space.BuildingTool.ProceduralMesh
{
    public class MeshCreator
    {
        private ITessellator _tessellator;
        public MeshCreator() : this(new LibTessTessellator())
        {
        }

        public MeshCreator(ITessellator tessellator)
        {
            _tessellator = tessellator;
        }

        public MeshDraft CreatePlanarMesh(IList<Vector3> vertices, IList<IList<Vector3>> holeVertices = null, float thickness = 0f)
        {
            MeshDraft md = _tessellator.CreatePolygon(vertices, holeVertices);

            if (md.vertices.Count >= 3)
            {
                //Plane plane = new Plane(md.vertices[0], md.vertices[1], md.vertices[2]);
                //md.SpreadOut(plane.normal, thickness / 2);
            }
            return md;
        }
    }
}
