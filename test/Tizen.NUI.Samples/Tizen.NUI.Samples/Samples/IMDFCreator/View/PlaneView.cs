using Space.BuildingTool.ProceduralMesh;
using ViewModel;

namespace Tizen.NUI.Samples
{
    public class PlaneView : IMDF3DView
    {
        private ModelRenderer meshRenderer;
        private RoomCategory category;

        public PlaneView(RoomCategory category, RoomPlane planeData)
        {
            this.category = category;
            Size = new Size(300, 300, 300);

            //Position = planeData.Coordinates.GetAverage();
            Position = new Position(0, 0, 0);
            CreateMesh(planeData);
        }

        private void CreateMesh(RoomPlane planeData)
        {
            /*
            foreach (var vertex in planeData.Coordinates)
            {
                Tizen.Log.Error("MYLOG", $"plane vertex :{vertex.X} {vertex.Y} {vertex.Z} \n");
            }
            */
            var meshDraft = MVMeshCreator.Instance.CreatePlaneMeshInfo(planeData.Coordinates);
            meshRenderer = new ModelRenderer();

            Tizen.Log.Error("MYLOG", $"Plane vertex Count : {meshDraft.vertices.Count}\n");
            foreach (var vertex in meshDraft.vertices)
            {
                Tizen.Log.Error("MYLOG", $"Plane vertex : {vertex.X}, {vertex.Y}, {vertex.Z}\n");
            }

            Tizen.Log.Error("MYLOG", "category : " + category + "\n");

            var planeTexture = "/images/IMDFTextures/s_baseColor.jpeg";
            switch (category)
            {
                //case RoomCategory.LIVINGROOM:
                    //planeTexture = null;
                    //break;
                default:
                    break;
            }
            AddRenderer(meshRenderer.CreateMeshRenderer(meshDraft, planeTexture, new Color(0.3f, 0.3f, 0.3f, 1.0f)));
        }
    }
}
