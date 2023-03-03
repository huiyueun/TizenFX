using Space.BuildingTool.Utility;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.Samples;

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

            if (md.vertices.Count >= 3 && thickness > 0)
            {
                System.Numerics.Vector3 v1 = new System.Numerics.Vector3(vertices[0].X, vertices[0].Y, vertices[0].Z);
                System.Numerics.Vector3 v2 = new System.Numerics.Vector3(vertices[1].X, vertices[1].Y, vertices[1].Z);
                System.Numerics.Vector3 v3 = new System.Numerics.Vector3(vertices[2].X, vertices[2].Y, vertices[2].Z);

                var plane = System.Numerics.Plane.CreateFromVertices(v1, v2, v3);

                var normalVector = new Vector3(plane.Normal.X, plane.Normal.Y, plane.Normal.Z);
                md.SpreadOut(normalVector, thickness / 2);

                //Plane plane = new Plane(md.vertices[0], md.vertices[1], md.vertices[2]);
                //md.SpreadOut(Utils.Normal3Vector(md.vertices[0], md.vertices[1], md.vertices[2]), thickness*2);
            }
            return md;
        }
    }
}
