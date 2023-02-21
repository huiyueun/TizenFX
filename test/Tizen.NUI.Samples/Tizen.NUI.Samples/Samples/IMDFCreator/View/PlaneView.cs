using Space.BuildingTool.ProceduralMesh;
using ViewModel;

namespace Tizen.NUI.Samples
{
    public class PlaneView : IMDF3DView
    {
        private ModelRenderer meshRenderer;

        public PlaneView(RoomPlane planeData)
        {
            Size = new Size(300, 300, 300);

            //Position = planeData.Coordinates.GetAverage();
            Position = new Position(0, 0, 0);
            CreateMesh(planeData);
        }

        private void CreateMesh(RoomPlane planeData)
        {
            foreach (var vertex in planeData.Coordinates)
            {
                Tizen.Log.Error("MYLOG", $"plane vertex :{vertex.X} {vertex.Y} {vertex.Z} \n");
            }
            var meshDraft = MVMeshCreator.Instance.CreatePlaneMeshInfo(planeData.Coordinates);
            meshRenderer = new ModelRenderer();

            Tizen.Log.Error("MYLOG", $"Plane vertex Count : {meshDraft.vertices.Count}\n");
            foreach (var vertex in meshDraft.vertices)
            {
                Tizen.Log.Error("MYLOG", $"Plane vertex : {vertex.X}, {vertex.Y}, {vertex.Z}\n");
            }

            AddRenderer(meshRenderer.CreateMeshRenderer(meshDraft, "/images/IMDFTextures/s_baseColor.jpeg", new Color(1.0f, 1.0f, 1.0f, 1.0f)));
        }
    }
}
