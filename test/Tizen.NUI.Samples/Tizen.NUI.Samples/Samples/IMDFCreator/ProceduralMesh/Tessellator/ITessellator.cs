using System.Collections.Generic;
using Tizen.NUI;

namespace Space.BuildingTool.ProceduralMesh
{
    public interface ITessellator
    {
        public MeshDraft CreatePolygon(IList<Vector3> vertices, IList<IList<Vector3>> holeVertices = null);
    }
}
