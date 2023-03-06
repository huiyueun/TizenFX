using Space.BuildingTool.ProceduralMesh;
using System.Collections.Generic;
using ViewModel;

namespace Tizen.NUI.Samples
{
    public class WallView : IMDF3DView
    {
        private ModelRenderer meshRenderer;

        public WallView(RoomWall wallData)
        {
            Name = wallData.Id;

            Size = new Size(300, 300, 300);
            //Position = planeData.Coordinates.GetAverage();
            Position = new Position(0, 0, 0);

            CreateMesh(wallData);
            //CreateOpening(wallData.Openings);
        }

        private void CreateMesh(RoomWall wall)
        {
            var doors = new List<Vector3>();
            var windows = new List<Vector3>();
            foreach (var opening in wall.Openings)
            {
                if (opening.Category == OpeningCategory.DOOR)
                {
                    doors.AddRange(opening.Coordinates);
                }
                else if (opening.Category == OpeningCategory.WINDOW)
                {
                    windows.AddRange(opening.Coordinates);
                }
            }
            foreach (var vertex in wall.Coordinates)
            {
                Tizen.Log.Error("MYLOG", $"1 vertex :{vertex.X} {vertex.Y} {vertex.Z} \n");
            }
            //TODO : Window & Door is nUll
            var meshDraft =  MVMeshCreator.Instance.CreateWallMeshInfo(wall.Coordinates[0], wall.Coordinates[1], doors, windows);

            Tizen.Log.Error("MYLOG", $"vertex Count : {meshDraft.vertices.Count}\n");
            foreach (var vertex in meshDraft.vertices)
            {
                Tizen.Log.Error("MYLOG", $"vertex : {vertex.X}, {vertex.Y}, {vertex.Z}\n");
            }

            meshRenderer = new ModelRenderer();
            AddRenderer(meshRenderer.CreateMeshRenderer(meshDraft, null, new Color(0.6f, 0.6f, 0.6f, 1.0f)));
        }

        private void CreateOpening(List<Opening> openings)
        {
            Tizen.Log.Error("MYLOG", "openings : " + openings.Count + "\n");
            foreach (var opening in openings)
            {
                var openingView = new OpeningView(opening);
                Add(openingView);
            }
        }
    }
}
